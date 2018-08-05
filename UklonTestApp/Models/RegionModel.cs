using System;
using System.Collections.Generic;

namespace UklonTestApp.Models
{
    public class RegionModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// The identifier of <see cref="RegionModel"/>.
        /// </summary>
        public virtual string RegionCode { get; set; }

        /// <summary>
        /// The name of <see cref="RegionModel"/>.
        /// </summary>
        public virtual string RegionName { get; set; }

        /// <summary>
        /// RegionTrafficStatuses
        /// </summary>
        public virtual List<RegionTrafficStatusModel> RegionTrafficStatuses { get; set; }
    }
}