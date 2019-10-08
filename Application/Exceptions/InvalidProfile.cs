using System;
using System.Runtime.Serialization;

namespace Application.Exceptions
{
    [Serializable]
    public class InvalidProfile : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public InvalidProfile()
        { }

        public InvalidProfile(string message) : base(message)
        { }

        public InvalidProfile(string message, Exception inner) : base(message, inner)
        { }

        protected InvalidProfile(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        { }
    }
}