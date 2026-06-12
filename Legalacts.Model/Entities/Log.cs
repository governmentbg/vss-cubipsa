using System;

namespace Legalacts.Model.Entities
{
    public partial class Log
    {
        public int LogId { get; set; }
        public int ActionLogTypeId { get; set; }
        public System.DateTime DatetimeOfAction { get; set; }
        public int CourtId { get; set; }
        public Nullable<int> CaseNumber { get; set; }
        public Nullable<int> ActKindId { get; set; }
        public string UID { get; set; }
        public virtual ActionLogType ActionLogType { get; set; }
        public virtual ActKind ActKind { get; set; }
        public virtual Court Court { get; set; }
    }
}
