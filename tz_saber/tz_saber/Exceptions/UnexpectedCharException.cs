using System.Runtime.Serialization;

namespace tz_saber.Exceptions
{
    public class UnexpectedCharException : SerializationException
    {
        public UnexpectedCharException(char c) : base($"Unexpected char: \'{c}\'''")
        {
        }
    }
}
