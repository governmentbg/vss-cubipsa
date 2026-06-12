using System;
using System.Collections.Generic;

namespace Legalacts.Model.Sitemap.Entities
{
    public partial class Sitemap
    {
        public int SitemapId { get; set; }
        public string Name { get; set; }
        public byte[] XML { get; set; }
        public int IndexID { get; set; }
        public virtual Index Index { get; set; }
    }
}
