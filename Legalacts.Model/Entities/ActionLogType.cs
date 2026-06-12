using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class ActionLogType
    {
        public ActionLogType()
        {
            this.Logs = new List<Log>();
        }

        public int ActionLogTypeId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Log> Logs { get; set; }
    }
}
