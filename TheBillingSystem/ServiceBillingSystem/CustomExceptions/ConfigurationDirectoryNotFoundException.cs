using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBillingSystem.CustomExceptions
{
    public class ConfigurationDirectoryNotFoundException : DirectoryNotFoundException
    {
        public string Value { get; }
        public override string Message { get; }


        public ConfigurationDirectoryNotFoundException(string value)
        {
            Value = value;
            Message = string.Join(string.Empty, "Configuration file directory not found: ", Value);
        }

        public ConfigurationDirectoryNotFoundException(string message, string value) : base(message)
        {
            Value = value;
            Message = message;
        }
    }
}
