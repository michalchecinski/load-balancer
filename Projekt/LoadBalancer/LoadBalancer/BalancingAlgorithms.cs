using LoadBalancer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public class BalancingAlgorithms
    {
        public static Instance Random()
        {
            var instances = LoadBalancerSettings.Current.Instances;
            Random random = new Random();
            var index = random.Next(0, instances.Length);
            return instances[index];
        }
    }
}
