using LoadBalancer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    public static class BalancingAlgorithms
    {
        private static Dictionary<Instance, long> instanceUsage = new Dictionary<Instance, long>();
        private static Instance[] instances = LoadBalancerSettings.Current.Instances;
        private static int lastRoundRobin = 0;
        private static int roundRobinCount = 0;

        static BalancingAlgorithms()
        {
            SetInstancesUsageToZero();
        }

        private static void SetInstancesUsageToZero()
        {
            foreach (var item in instances)
            {
                instanceUsage[item] = 0;
            }
        }

        public static Instance Random()
        {
            Random random = new Random();
            var index = random.Next(0, instances.Length);
            return instances[index];
        }

        public static Instance ConnectionCount()
        {
            var instance = instanceUsage.OrderBy(x => x.Value).FirstOrDefault().Key;
            
            if (instanceUsage[instance] + 1 > long.MaxValue)
            {
                SetInstancesUsageToZero();
            }
            instanceUsage[instance]++;

            return instance;
        }

        public static Instance RoundRobin()
        {
            var maxRoundRobin = LoadBalancerSettings.Current.RoundRobinPerInstance;
            if (maxRoundRobin == null || maxRoundRobin == 0)
            {
                maxRoundRobin = 10;
            }
            if (roundRobinCount == maxRoundRobin)
            {
                roundRobinCount = 0;
                var index = lastRoundRobin + 1;
                if (index > instances.Length-1)
                {
                    index = 0;
                }
                lastRoundRobin = index;
                return instances[index];
            }
            roundRobinCount++;
            return instances[lastRoundRobin];
        }
    }
}
