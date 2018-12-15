using ManagerCloud.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ManagerCloud.BL
{
    public sealed class Unity
    {
        public string FileFilter { get; }
        private const string FileFormat = ".csv";
        private FileSystemWatcher _watcher;
        private static Dictionary<Type, ReaderWriterLockSlim> _lockers;

        private static void InitializeLockers()
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

        public Unity(string directoryPath, string fileFilter)
        {
            FileFilter = string.Join(string.Empty, fileFilter, FileFormat);
            InitializeLockers();
            StartTaskAllFiles(directoryPath);
            StartFileWatcher(directoryPath);
        }

        private void StartTaskAllFiles(string directoryPath)
        {
            if (!CheckDirectoryContainFiles(directoryPath)) return;
            foreach (var file in GetFiles(directoryPath))
            {
                StartTaskFileParse(Path.GetFileName(file), file);
            }
        }

        private static void StartTaskFileParse(string fileName, string fullPath)
        {
            try
            {
                Task.Factory.StartNew(() => ParserCsv.ReadFile(fullPath, fileName, _lockers));
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

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            StartTaskFileParse(e.Name, e.FullPath);
        }

        private IEnumerable<string> GetFiles(string directoryPath) =>
            Directory.GetFiles(directoryPath, FileFilter, SearchOption.TopDirectoryOnly);

        private bool CheckDirectoryContainFiles(string directoryPath) =>
            Directory.Exists(directoryPath) && GetFiles(directoryPath).Any();
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