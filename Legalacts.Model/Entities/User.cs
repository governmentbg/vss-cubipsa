using System;
using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class User
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public System.Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Nullable<int> AdmUnitId { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
