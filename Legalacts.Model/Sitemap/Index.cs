using System.Collections.Generic;

namespace Legalacts.Model.Sitemap.Entities
{
    public partial class Index
    {
        public Index()
        {
            this.Sitemaps = new List<Sitemap>();
        }

        public int IndexId { get; set; }
        public string Name { get; set; }
        public byte[] XML { get; set; }
        public int RouteId { get; set; }
        public virtual Route Route { get; set; }
        public virtual ICollection<Sitemap> Sitemaps { get; set; }
    }
}
