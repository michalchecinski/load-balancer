using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer.Logs.Web.Models
{
    public class Metrics
    {
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }

        [Display(Name = "Server error code (5XX) percentage")]
        public int FiveHundredPercent { get; set; }

        [Display(Name = "Server error code (5XX) count")]
        public int FiveHundredCount { get; set; }

        [Display(Name = "Request count")]
        public int RequestCount { get; set; }

        [Display(Name = "Success status code (2XX) count")]
        public int SuccessCount { get; set; }

        [Display(Name = "Success status code (2XX) percentage")]
        public int SuccessPercent { get; set; }

        [Display(Name = "Not found code (404) count")]
        public int NotFoundCount { get; set; }

        [Display(Name = "Not found code (404) percent")]
        public int NotFoundPercent { get; set; }
    }
}
