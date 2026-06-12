using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class CaseKind
    {
        public CaseKind()
        {
            this.Acts = new List<Act>();
        }

        public int CaseKindId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string EcliCode { get; set; }
        public string Abbreviation { get; set; }
        public virtual ICollection<Act> Acts { get; set; }
        public virtual ICollection<DeletedAct> DeletedActs { get; set; }
    }
}
