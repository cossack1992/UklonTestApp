using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Models
{
    public class RegionTrafficStatusModel
    {
        public virtual Guid Id { get; set; }
        public virtual Guid? RegionId { get; set; }
        public virtual RegionModel Region { get; set; }
        public virtual DateTimeOffset DateTimeNow { get; set; }
        public virtual string TrafficLevel { get; set; }
        public virtual string TrafficIcon { get; set; }
        public virtual string TrafficMessage { get; set; }
    }
}
