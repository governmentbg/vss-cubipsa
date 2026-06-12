using System.Collections.Generic;
using System.Web.Mvc;

namespace Legalacts.Web.Models
{
    public class ActsVM
    {
        public List<CourtInfo> Courts { get; set; }
        public long TotalCount { get; set; }

        public int? Year { get; set; }
        public IEnumerable<SelectListItem> Years { get; set; }
    }

    public class CourtInfo
    {
        public int CourtId { get; set; }
        public string CourtName { get; set; }
        public int ActsCount { get; set; }
    }
}
