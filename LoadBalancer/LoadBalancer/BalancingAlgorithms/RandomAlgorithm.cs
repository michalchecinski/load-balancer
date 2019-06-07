using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadBalancer.Models;

namespace LoadBalancer.BalancingAlgorithms
{
    public class RandomAlgorithm : BalancingAlgorithm
    {
        public override Instance GetInstance()
        {
            Random random = new Random();
            var index = random.Next(0, instances.Length);
            return instances[index];
        }
    }
}
