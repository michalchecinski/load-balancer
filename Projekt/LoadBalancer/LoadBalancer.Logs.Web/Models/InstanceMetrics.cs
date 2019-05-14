using LoadBalancer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Logs.Web.Models
{
    public class InstanceMetrics : Metrics
    {
        public Instance Instance { get; set; }

        [Display(Name = "Request percentage")]
        public double RequestPercentage { get; set; }
    }
}
