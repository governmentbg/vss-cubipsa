using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class Status
    {
        public Status()
        {
            this.Acts = new List<Act>();
        }

        public int StatusId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Act> Acts { get; set; }
        public virtual ICollection<DeletedAct> DeletedActs { get; set; }
    }
}
