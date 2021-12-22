using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Service;

namespace WebApplication3
{
    public class SimpleTimeService : ITimeService
    {
        public string Time { get; set; }
        public SimpleTimeService()
        {
            Time = DateTime.Now.ToString("hh:mm:ss");
        }
    }
}
