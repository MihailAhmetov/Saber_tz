using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace tz_saber.Exceptions
{
    public class EmptyStreamException : SerializationException
    {
        public EmptyStreamException() : base("Stream is empty")
        {
        }
    }
}
