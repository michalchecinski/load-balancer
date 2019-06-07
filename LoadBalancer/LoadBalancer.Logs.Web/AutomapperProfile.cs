using AutoMapper;
using LoadBalancer.Logs.Web.Models;

namespace LoadBalancer.Logs.Web
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Metrics, InstanceMetrics>();
        }
    }
}
