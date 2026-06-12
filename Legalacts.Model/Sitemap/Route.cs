using System;
using System.Collections.Generic;

namespace Legalacts.Model.Sitemap.Entities
{
    public partial class Route
    {
        public Route()
        {
            this.Indexes = new List<Index>();
        }

        public int RouteId { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Index> Indexes { get; set; }
    }
}
