using System.Runtime.Serialization;

namespace tz_saber.Exceptions
{
    public class UnexpectedEndException : SerializationException
    {
        public UnexpectedEndException() : base("Unexpected stream ending")
        {
        }
    }
}
