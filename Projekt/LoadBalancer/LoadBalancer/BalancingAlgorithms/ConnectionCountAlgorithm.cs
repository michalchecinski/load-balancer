using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadBalancer.Models;

namespace LoadBalancer.BalancingAlgorithms
{
    public class ConnectionCountAlgorithm : BalancingAlgorithm
    {
        private static Dictionary<Instance, long> instanceUsage = new Dictionary<Instance, long>();

        public ConnectionCountAlgorithm()
        {
            SetInstancesUsageToZero();
        }

        public override Instance GetInstance()
        {
            var instance = instanceUsage.OrderBy(x => x.Value).FirstOrDefault().Key;

            if (instanceUsage[instance] + 1 > long.MaxValue)
            {
                SetInstancesUsageToZero();
            }
            instanceUsage[instance]++;

            return instance;
        }

        private void SetInstancesUsageToZero()
        {
            foreach (var item in instances)
            {
                instanceUsage[item] = 0;
            }
        }
    }
}
