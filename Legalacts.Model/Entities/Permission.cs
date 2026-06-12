using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            this.Roles = new List<Role>();
        }

        public System.Guid Id { get; set; }
        public string ResourceName { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
