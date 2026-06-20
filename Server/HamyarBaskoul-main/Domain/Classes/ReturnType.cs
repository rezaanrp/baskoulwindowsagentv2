using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Classes
{
    public enum Type
    {
        Success,
        Error,
    }
    public class ReturnType<T>
    {
        public Type type { get; set; }
        public T message { get; set; }
    }
}
