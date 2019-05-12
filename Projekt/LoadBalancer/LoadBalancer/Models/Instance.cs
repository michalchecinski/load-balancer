using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Models
{
    public class Instance
    {
        public string Ip { get; set; }
        public int Port { get; set; }
    }

}
