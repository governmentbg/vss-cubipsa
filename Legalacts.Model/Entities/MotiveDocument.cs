using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class MotiveDocument
    {
        public MotiveDocument()
        {
            this.Acts = new List<Act>();
        }

        public int MotiveDocumentId { get; set; }
        public byte[] Content { get; set; }
        public string MimeType { get; set; }
        public virtual ICollection<Act> Acts { get; set; }
    }
}
