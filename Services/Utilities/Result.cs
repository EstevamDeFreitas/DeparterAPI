using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities
{
    public class Result<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }

        public Result(string message, T data)
        {
            Message = message;
            Data = data;
        }

        public Result(string message)
        {
            Message = message;
        }
    }
}
