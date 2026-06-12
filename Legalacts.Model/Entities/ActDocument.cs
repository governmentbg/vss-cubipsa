using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class ActDocument
    {
        public ActDocument()
        {
            this.Acts = new List<Act>();
        }

        public int ActDocumentId { get; set; }
        public byte[] Content { get; set; }
        public string MimeType { get; set; }
        public string Extension { get; set; }
        public virtual ICollection<Act> Acts { get; set; }
    }
}
