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

        public Instance()
        {

        }

        public Instance(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        public override string ToString()
        {
            return $"{Ip}:{Port}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }


            Instance instance = obj as Instance;
            if ((System.Object)instance == null)
            {
                return false;
            }

            return (Ip == instance.Ip) && (Port == instance.Port);
        }
    }
}
