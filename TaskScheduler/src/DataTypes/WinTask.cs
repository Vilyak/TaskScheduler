using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskScheduler.src.DataTypes
{
    internal struct WinTask
    {
        public String path { get; }
        public DateTime dateTime { get; }

        public WinTask(String path, DateTime dateTime)
        {
            this.path = path;
            this.dateTime = dateTime;
        }
    }
}
