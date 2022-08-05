using System.Runtime.Serialization;

namespace tz_saber.Exceptions
{
    public class UnconnectedNodeException : SerializationException
    {
        public UnconnectedNodeException() : base("Unconnected node found")
        {
        }
    }
}
