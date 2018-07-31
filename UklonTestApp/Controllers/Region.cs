namespace UklonTestApp.Controllers
{
    public class Region
    {
        public Region(string regionCode, string regionName)
        {
            RegionCode = regionCode;
            RegionName = regionName;
        }

        /// <summary>
        /// The identifier of <see cref="Region"/>.
        /// </summary>
        public string RegionCode { get; }

        /// <summary>
        /// The name of <see cref="Region"/>.
        /// </summary>
        public string RegionName { get; }
    }
}