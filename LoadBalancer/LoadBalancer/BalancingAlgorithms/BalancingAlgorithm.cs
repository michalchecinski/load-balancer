using LoadBalancer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.BalancingAlgorithms
{
    public abstract class BalancingAlgorithm
    {
        protected static Instance[] instances = LoadBalancerSettings.Current.Instances;

        public abstract Instance GetInstance();
    }
}
