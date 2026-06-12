using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class Role
    {
        public Role()
        {
            this.Permissions = new List<Permission>();
            this.Users = new List<User>();
        }

        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
