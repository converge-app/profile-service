using System;
using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class InvalidBidding : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvalidBidding()
        {
        }

        public InvalidBidding(string message) : base(message)
        {
        }

        public InvalidBidding(string message, Exception inner) : base(message, inner)
        {
        }

        protected InvalidBidding(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    } 
}