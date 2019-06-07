using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoadBalancer.Models;

namespace LoadBalancer.BalancingAlgorithms
{
    public class RoundRobinAlgorithm : BalancingAlgorithm
    {
        private static int lastRoundRobin = 0;
        private static int roundRobinCount = 0;

        public override Instance GetInstance()
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
                if (index > instances.Length - 1)
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
