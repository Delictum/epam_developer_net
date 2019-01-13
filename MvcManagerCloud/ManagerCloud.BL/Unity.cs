using ManagerCloud.Core.Helpers;
using ManagerCloud.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ManagerCloud.BL
{
    public sealed class Unity
    {
        public string FileFilter { get; }
        private const string FileFormat = ".csv";
        private const int IdSourceCustomerPurchase = 212;

        private FileSystemWatcher _watcher;
        public Dictionary<Type, ReaderWriterLockSlim> _lockers;
        public readonly DbContext _dbContext;

        private void InitializeLockers()
        {
            _lockers = new Dictionary<Type, ReaderWriterLockSlim>
            {
                {
                    typeof(Models.Client), new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)
                },
                {
                    typeof(Models.Item), new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)
                },
                {
                    typeof(Models.DataSource), new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)
                },
                {
                    typeof(Models.Sale), new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion)
                }
            };
        }

        public Unity()
        {
            var contextFactory = new ManagerCloudContextFactory();
            _dbContext = contextFactory.CreateInstance(); 
            FileFilter = string.Join(string.Empty, ConfigurationManager.AppSettings["FileNameFilter"], FileFormat);
            InitializeLockers();
        }

        private void StartTaskAllFiles(string directoryPath)
        {
            if (!CheckDirectoryContainFiles(directoryPath)) return;
            foreach (var file in GetFiles(directoryPath))
            {
                StartLoadTask(Path.GetFileName(file), file);
            }
        }

        private void StartLoadTask(string fileName, string fullPath)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    var fileData = Parser.ReadCsvFile(fullPath, fileName);
                    var dbLoader = new DatabaseItemLoader(_lockers, _dbContext);
                    foreach (var lineFile in fileData)
                    {
                        var tupleModels = Parser.ParseEntitiesToTuple(fileName, lineFile);
                        dbLoader.LoadItems(tupleModels);
                    }
                });
            }
            catch (FileNotFoundException e)
            {
                LoggerHelper.AddErrorLog(new EventLog(), e.Message);
            }
            catch (IOException e)
            {
                LoggerHelper.AddErrorLog(new EventLog(), e.Message);
            }
            catch (NotSupportedException e)
            {
                LoggerHelper.AddErrorLog(new EventLog(), e.Message);
            }
            catch (InvalidOperationException e)
            {
                LoggerHelper.AddErrorLog(new EventLog(), e.Message);
            }
        }

        public void StartFileWatcher(string directoryPath)
        {
            var bufferSize = ConfigurationManager.AppSettings["FileSystemWatcherBufferSize"];

            _watcher = new FileSystemWatcher
            {
                Path = directoryPath,
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName |
                               NotifyFilters.DirectoryName,
                Filter = FileFilter,
                InternalBufferSize = int.Parse(bufferSize)
            };
            _watcher.Created += OnChanged;
            _watcher.EnableRaisingEvents = true;
        }

        public void StopFileWatcher()
        {
            _watcher.EnableRaisingEvents = false;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            StartLoadTask(e.Name, e.FullPath);
        }

        private IEnumerable<string> GetFiles(string directoryPath) =>
            Directory.GetFiles(directoryPath, FileFilter, SearchOption.TopDirectoryOnly);

        private bool CheckDirectoryContainFiles(string directoryPath) =>
            Directory.Exists(directoryPath) && GetFiles(directoryPath).Any();


        public List<Tuple<int, string, string>> GetAllClients()
        {
            var listClientsItems = new List<Tuple<int, string, string>>();
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            Expression<Func<Models.Client, bool>> clientSearchCriteria = x =>
                x.LastName != null;

            var list = unitOfWork.GetAllEntity(clientSearchCriteria, unitOfWork.ClientRepository);

            foreach (var item in list)
            {
                listClientsItems.Add(new Tuple<int, string, string>(
                    item.Id,
                    item.FirstName,
                    item.LastName
                ));
            }

            return listClientsItems;
        }

        public List<Tuple<int, string>> GetAllItems()
        {
            var listItemsItems = new List<Tuple<int, string>>();
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            Expression<Func<Models.Item, bool>> itemSearchCriteria = x =>
                x.Name != null;

            var list = unitOfWork.GetAllEntity(itemSearchCriteria, unitOfWork.ItemRepository);

            foreach (var item in list)
            {
                listItemsItems.Add(new Tuple<int, string>(
                    item.Id,
                    item.Name
                ));
            }

            return listItemsItems;
        }

        public List<Tuple<int, Tuple<int, string, string>, Tuple<int, string>, Tuple<int, string>, DateTime, double>> GetAllSales()
        {
            var listSalesItems = new List<Tuple<int, Tuple<int, string, string>, Tuple<int, string>, Tuple<int, string>, DateTime, double>>();
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            Expression<Func<Models.Sale, bool>> saleSearchCriteria = x =>
                x.Client.LastName != null;

            var list = unitOfWork.GetAllEntity(saleSearchCriteria, unitOfWork.SaleRepository);

            foreach (var item in list)
            {
                listSalesItems.Add(
                    new Tuple<int, Tuple<int, string, string>, Tuple<int, string>, Tuple<int, string>, DateTime, double>(
                        item.Id,
                        new Tuple<int, string, string>(
                            item.Client.Id,
                            item.Client.FirstName,
                            item.Client.LastName
                        ),
                        new Tuple<int, string>(
                            item.DataSource.Id,
                            item.DataSource.FileName
                        ),
                        new Tuple<int, string>(
                            item.Item.Id,
                            item.Item.Name
                        ),
                        item.Date,
                        item.SaleSum
                    ));
            }

            return listSalesItems;
        }

        public void AddSale(Tuple<int, string, DateTime, double> saleTuple)
        {
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);

            var newSale = new Models.Sale
            {
                Client = new Models.Client
                {
                    Id = saleTuple.Item1
                },
                DataSource = new Models.DataSource
                {
                    Id = IdSourceCustomerPurchase
                },
                Item = new Models.Item
                {
                    Name = saleTuple.Item2
                },
                Date = saleTuple.Item3,
                SaleSum = saleTuple.Item4
            };

            unitOfWork.TryAddSaleWithId(newSale);
        }

        public Tuple<int, string, string> GetClient(int id)
        {
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            Expression<Func<Models.Client, bool>> clientSearchCriteria = x =>
                x.Id == id;

            var item = unitOfWork.GetEntity(clientSearchCriteria, unitOfWork.ClientRepository);

            if (item == null)
                return null;

            var clientItems = new Tuple<int, string, string>(
                item.Id,
                item.FirstName,
                item.LastName
            );

            return clientItems;
        }

        public void AddClient(Tuple<string, string> clientTuple)
        {
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            Expression<Func<Models.Client, bool>> clientSearchCriteria = x =>
                x.FirstName == clientTuple.Item1 && x.LastName == clientTuple.Item2;

            unitOfWork.TryAddClient(new Models.Client
                {
                    FirstName = clientTuple.Item1,
                    LastName = clientTuple.Item2
                }, 
                clientSearchCriteria
            );
        }

        public void UpdateClient(Tuple<int, string, string> clientTuple)
        {
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);

            unitOfWork.TryUpdateClient(new Models.Client
                {
                    Id = clientTuple.Item1,
                    FirstName = clientTuple.Item2,
                    LastName = clientTuple.Item3
                }
            );
        }

        public void RemoveClient(int clientId)
        {
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            Expression<Func<Models.Item, bool>> clientSearchCriteria = x =>
                x.Id == clientId;

            unitOfWork.TryRemove(clientSearchCriteria, unitOfWork.ItemRepository);
        }

        public Tuple<int, string> GetItem(int id)
        {
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            Expression<Func<Models.Item, bool>> itemSearchCriteria = x =>
                x.Id == id;

            var item = unitOfWork.GetEntity(itemSearchCriteria, unitOfWork.ItemRepository);

            if (item == null)
                return null;

            var items = new Tuple<int, string>(
                item.Id,
                item.Name
            );

            return items;
        }

        public void AddItem(string itemName)
        {
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            Expression<Func<Models.Item, bool>> itemSearchCriteria = x =>
                x.Name == itemName;

            unitOfWork.TryAddItem(new Models.Item
                {
                    Name = itemName
            },
                itemSearchCriteria
            );
        }

        public void UpdateItem(Tuple<int, string> itemTuple)
        {
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);

            unitOfWork.TryUpdateItem(new Models.Item
                {
                    Id = itemTuple.Item1,
                    Name = itemTuple.Item2,
                }
            );
        }

        public void RemoveItem(int itemId)
        {
            var unitOfWork = new UnitOfWork(_dbContext, _lockers);
            Expression<Func<Models.Item, bool>> itemSearchCriteria = x =>
                x.Id == itemId;

            unitOfWork.TryRemove(itemSearchCriteria, unitOfWork.ItemRepository);
        }

        ~Unity()
        {
            _dbContext.Dispose();
        }
    }
}


//public string GetClientName()
//{
//    Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == "Slava";
//    using (var context = _contextFactory.CreateInstance())
//    {
//        var clientUnitOfWork = new UnitOfWorks.ClientUnitOfWork(context, _repositoryFactory,
//            ResolveLocker(typeof(Models.Client)));

//        var client = clientUnitOfWork.TryGet(clientSearchCriteria, true);
//        return client != null ? client.LastName : string.Empty;
//    }
//}

//public void AddClient()
//{
//    var newClient = new Models.Client { FirstName = "Ivan", LastName = "Novich" };
//    Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == "Ivan" && x.LastName == "Novich";
//    using (var context = _contextFactory.CreateInstance())
//    {
//        var clientUnitOfWork = new UnitOfWorks.ClientUnitOfWork(context, _repositoryFactory,
//            ResolveLocker(typeof(Models.Client)));

//        clientUnitOfWork.TryAdd(newClient, clientSearchCriteria, true);
//    }
//}

//public void UpdateClient()
//{
//    Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == "Ivan" && x.LastName == "Novich";
//    using (var context = _contextFactory.CreateInstance())
//    {
//        var clientUnitOfWork = new UnitOfWorks.ClientUnitOfWork(context, _repositoryFactory,
//            ResolveLocker(typeof(Models.Client)));

//        var client = clientUnitOfWork.TryEntityGet(clientSearchCriteria, true);
//        client.FirstName = "Slava";
//        clientUnitOfWork.TryUpdate(client, clientSearchCriteria, true);
//    }
//}

//public void RemoveClient()
//{
//    Expression<Func<Models.Client, bool>> clientSearchCriteria = x => x.FirstName == "Slava" && x.LastName == "Novich";
//    using (var context = _contextFactory.CreateInstance())
//    {
//        var clientUnitOfWork = new UnitOfWorks.ClientUnitOfWork(context, _repositoryFactory,
//            ResolveLocker(typeof(Models.Client)));

//        var client = clientUnitOfWork.TryEntityGet(clientSearchCriteria, true);
//        clientUnitOfWork.TryRemove(client, clientSearchCriteria, true);
//    }
//}