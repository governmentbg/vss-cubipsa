using System;
using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class HigherCourt
    {
        public HigherCourt()
        {
            this.Acts = new List<Act>();
        }

        public int HigherCourtId { get; set; }
        public int CourtId { get; set; }
        public int OutputNumber { get; set; }
        public int Year { get; set; }
        public int SendToDocumentKindId { get; set; }
        public Nullable<System.DateTime> DateOfDispatch { get; set; }
        public virtual ICollection<Act> Acts { get; set; }
        public virtual Court Court { get; set; }
        public virtual SendToDocumentKind SendToDocumentKind { get; set; }
    }
}
