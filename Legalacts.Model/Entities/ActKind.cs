using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class ActKind
    {
        public ActKind()
        {
            this.Acts = new List<Act>();
            this.ConnectedCases = new List<ConnectedCase>();
            this.Logs = new List<Log>();
        }

        public int ActKindId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Act> Acts { get; set; }
        public virtual ICollection<ConnectedCase> ConnectedCases { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
        public virtual ICollection<DeletedAct> DeletedActs { get; set; }
    }
}
