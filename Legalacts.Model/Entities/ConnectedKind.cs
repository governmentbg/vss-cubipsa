using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class ConnectedKind
    {
        public ConnectedKind()
        {
            this.ConnectedCases = new List<ConnectedCase>();
        }

        public int ConnectedKindId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ConnectedCase> ConnectedCases { get; set; }
    }
}
