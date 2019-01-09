using System;

namespace ManagerCloud.Core.CustomExceptions.ModelObjectException
{
    public class ModelObjectException : Exception
    {
        public object ModelObject { get; }
        public override string Message { get; }

        public ModelObjectException(object addObject)
        {
            ModelObject = addObject;
            Message = string.Join(string.Empty, base.Message, "Exception with object type \"", addObject.GetType(), addObject.ToString(), "\". ");
        }

        public ModelObjectException(object addObject, string message)
        {
            ModelObject = addObject;
            Message = message;
        }
    }
}
