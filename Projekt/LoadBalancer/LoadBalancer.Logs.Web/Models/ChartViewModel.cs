using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Logs.Web.Models
{
    public class ChartViewModel  
    {
        public ChartType ChartType { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public IList<Metrics> ChartDataModels { get; set; }
    }
}
