using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ManagerCloud.BL.Models;
using ManagerCloud.Core.CustomExceptions.ParserException;
using ManagerCloud.Core.Helpers;

namespace ManagerCloud.BL
{
    internal static class ParserCsv
    {
        private const int CorrectItemCount = 4;
        private const string FolderNameProcessedFiles = "Processed";
        private const string InvalidFile = "Error";
        private const int ClientPropertiesCount = 2;
        private static int _currentLine;

        internal static void ReadFile(string fullPath, string fileName, Dictionary<Type, ReaderWriterLockSlim> lockers)
        {
            var textFile = File.OpenText(fullPath);
            _currentLine = 0;
            var invalid = false;
            try
            {
                while (!textFile.EndOfStream)
                {
                    _currentLine++;
                    var stringLine = textFile.ReadLine();
                    ParseStringLine(fileName, stringLine, lockers);
                }
            }
            catch (ParserException e)
            {
                LoggerHelper.AddErrorLog(new EventLog(), string.Join(" ", e.Message, "On line", _currentLine));
                invalid = true;
            }
            finally
            {
                ((IDisposable) textFile).Dispose();
            }
            MoveOrReplaceFile(fullPath, invalid);
            LoggerHelper.AddInfoLog(new EventLog(), string.Join(string.Empty, "File \"", fileName, "\" processing completed"));
        }

        private static void MoveOrReplaceFile(string fullPath, bool invalid)
        {
            var fileName = Path.GetFileName(fullPath);
            var currentDirectory = Path.GetDirectoryName(fullPath);
            var pathProcessedFiles = invalid 
                ? Path.Combine(currentDirectory ?? throw new InvalidOperationException(), InvalidFile) 
                : Path.Combine(currentDirectory ?? throw new InvalidOperationException(), FolderNameProcessedFiles);
            Directory.CreateDirectory(pathProcessedFiles);

            var newFullPath = Path.Combine(pathProcessedFiles, fileName ?? throw new InvalidOperationException());

            if (File.Exists(newFullPath))
                File.Delete(newFullPath);
            File.Move(fullPath, newFullPath);
        }

        private static void ParseStringLine(string fileName, string stringLine, IDictionary<Type, ReaderWriterLockSlim> lockers)
        {
            if (string.IsNullOrEmpty(stringLine))
                throw new ParserException(fileName, _currentLine);
            
            var itemsStringLine = stringLine.Split(',');
            if (!CheckCorrectItemCount(itemsStringLine.Length))
                throw new ParserItemCountException(itemsStringLine.Length, CorrectItemCount, fileName, _currentLine);

            var dataBaseItemLoader = new DatabaseItemLoader(lockers);
            dataBaseItemLoader.LoadItems(ParseEntitiesToTuple(fileName, itemsStringLine));
        }

        private static Tuple<Client, Item, DataSource, Sale> ParseEntitiesToTuple(string fileName, IReadOnlyList<string> itemStringLine)
        {
            var clientProperties = ParseClientItem(itemStringLine[1]);
            if (clientProperties.Length != ClientPropertiesCount)
            {
                throw new ParseClientException(clientProperties, ClientPropertiesCount, fileName, _currentLine);
            }
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
