using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BL.ManagerCloud
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
            if (CheckDirectoryContainFiles(directoryPath))
            {
                foreach (var file in GetFiles(directoryPath))
                {
                    StartTaskFileParse(Path.GetFileName(file), file);
                }
            }
            StartFileWatcher(directoryPath);
        }

        private static void StartTaskFileParse(string fileName, string fullPath) => 
            Task.Factory.StartNew(() => ParserCsv.ReadFile(fullPath, fileName, _lockers));

        private void StartFileWatcher(string directoryPath)
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

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            StartTaskFileParse(e.Name, e.FullPath);
        }

        private IEnumerable<string> GetFiles(string directoryPath) =>
            Directory.GetFiles(directoryPath, FileFilter, SearchOption.TopDirectoryOnly);

        private bool CheckDirectoryContainFiles(string directoryPath) =>
            Directory.Exists(directoryPath) && GetFiles(directoryPath).Any();

        ~Unity()
        {
            _watcher?.Dispose();
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