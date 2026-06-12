using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class ConnectedType
    {
        public ConnectedType()
        {
            this.ConnectedCases = new List<ConnectedCase>();
        }

        public int ConnectedTypeId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ConnectedCase> ConnectedCases { get; set; }
    }
}
