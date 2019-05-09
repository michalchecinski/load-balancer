using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Models
{

    public class LoadBalancerSettings
    {
        public static LoadBalancerSettings Current { get; private set; }

        public Instance[] Instances { get; set; }

        public string BalancingAlgorithm { get; set; }

        public int? RoundRobinPerInstance { get; set; }

        public LoadBalancerSettings()
        {
            Current = this;
        }
    }
}
