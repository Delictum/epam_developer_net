using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using ManagerCloud.BL.Models;

namespace ManagerCloud.BL
{
    internal static class ParserCsv
    {
        private const int CorrectItemCount = 4;
        private const string FolderNameProcessedFiles = "Processed";

        internal static void ReadFile(string fullPath, string fileName, Dictionary<Type, ReaderWriterLockSlim> lockers)
        {
            var textFile = File.OpenText(fullPath);
            try
            {
                while (!textFile.EndOfStream)
                {
                    var stringLine = textFile.ReadLine();
                    ParseStringLine(fileName, stringLine, lockers);
                }
            }
            finally
            {
                ((IDisposable)textFile).Dispose();
                MoveOrReplaceFile(fullPath);
            }
        }

        private static void MoveOrReplaceFile(string fullPath)
        {
            var fileName = Path.GetFileName(fullPath);
            var currentDirectory = Path.GetDirectoryName(fullPath);
            var pathProcessedFiles = Path.Combine(currentDirectory ?? throw new InvalidOperationException(),
                FolderNameProcessedFiles);
            Directory.CreateDirectory(pathProcessedFiles);
            var newFullPath = Path.Combine(pathProcessedFiles, fileName ?? throw new InvalidOperationException());
            if (File.Exists(newFullPath))
                File.Delete(newFullPath);
            File.Move(fullPath, newFullPath);
        }

        private static void ParseStringLine(string fileName, string stringLine, Dictionary<Type, ReaderWriterLockSlim> lockers)
        {
            if (string.IsNullOrEmpty(stringLine))
                return;
            
            var itemsStringLine = stringLine.Split(',');
            if (!CheckCorrectItemCount(itemsStringLine.Length))
                throw new Exception();

            var dataBaseItemLoader = new DatabaseItemLoader(lockers);
            dataBaseItemLoader.LoadItems(ParseEntitiesToTuple(fileName, itemsStringLine));
        }

        private static Tuple<Client, Item, DataSource, Sale> ParseEntitiesToTuple(string fileName, IReadOnlyList<string> itemStringLine)
        {
            var clientProperties = ParseClientItem(itemStringLine[1]);
            if (clientProperties.Length != 2) throw new Exception();
            var newClient = new Client { FirstName = clientProperties[0], LastName = clientProperties[1] };

            var newItem = new Item { Name = itemStringLine[2]};
            var newDataSource = new DataSource { FileName = fileName };
            var newSale = new Sale
            {
                Client = newClient, DataSource = newDataSource, Date = DateTime.Parse(itemStringLine[0]), Item = newItem, SaleSum = double.Parse(itemStringLine[3])
            };
            return new Tuple<Client, Item, DataSource, Sale>(newClient, newItem, newDataSource, newSale);
        }

        private static bool CheckCorrectItemCount(int count) => CorrectItemCount == count;

        private static string[] ParseClientItem(string clientItem) => clientItem.Split(' ');
    }
}
