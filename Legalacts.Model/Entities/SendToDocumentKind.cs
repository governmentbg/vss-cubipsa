using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class SendToDocumentKind
    {
        public SendToDocumentKind()
        {
            this.HigherCourts = new List<HigherCourt>();
        }

        public int SendToDocumentKindId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<HigherCourt> HigherCourts { get; set; }
    }
}
