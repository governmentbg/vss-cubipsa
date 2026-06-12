using System;

namespace Legalacts.Model.Entities
{
    public partial class ConnectedCase
    {
        public int ConnectedCaseId { get; set; }
        public int ConnectedTypeId { get; set; }
        public int ConnectedKindId { get; set; }
        public int ActId { get; set; }
        public int CourtId { get; set; }
        public int AppealActKindId { get; set; }
        public int CaseNumber { get; set; }
        public int Year { get; set; }
        public Nullable<int> NumberOfAppealAct { get; set; }
        public Nullable<System.DateTime> DateOfAppealAct { get; set; }
        public virtual ActKind ActKind { get; set; }
        public virtual Act Act { get; set; }
        public virtual ConnectedKind ConnectedKind { get; set; }
        public virtual ConnectedType ConnectedType { get; set; }
    }
}
