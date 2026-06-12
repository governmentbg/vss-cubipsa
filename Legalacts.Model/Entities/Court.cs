using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class Court
    {
        public Court()
        {
            this.Acts = new List<Act>();
            this.HigherCourts = new List<HigherCourt>();
            this.Logs = new List<Log>();
        }

        public int CourtId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string EcliCode { get; set; }
        public virtual ICollection<Act> Acts { get; set; }
        public virtual ICollection<HigherCourt> HigherCourts { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<DeletedAct> DeletedActs { get; set; }
    }
}
