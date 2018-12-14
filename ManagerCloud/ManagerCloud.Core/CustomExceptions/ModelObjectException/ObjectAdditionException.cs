using System;

namespace ManagerCloud.Core.CustomExceptions.ModelObjectException
{
    public class ObjectAdditionException : ModelObjectException
    {
        public override string Message { get; }
        
        public ObjectAdditionException(object addObject) : base(addObject)
        {
            Message = string.Join(string.Empty, base.Message, "It happened when adding model object.");
        }

        public ObjectAdditionException(object addObject, string message) : base(addObject, message)
        {
        }
    }
}
