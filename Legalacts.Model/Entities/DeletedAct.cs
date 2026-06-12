using System;
using System.Collections.Generic;

namespace Legalacts.Model.Entities
{
    public partial class DeletedAct
    {
        public int DeletedActId { get; set; }
        public Nullable<int> ActNumber { get; set; }
        public int CaseNumber { get; set; }
        public string Judge { get; set; }
        public int ActKindId { get; set; }
        public int CaseKindId { get; set; }
        public int CaseYear { get; set; }
        public Nullable<int> ActYear { get; set; }
        public int CourtId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public int StatusId { get; set; }
        public string ResultOfAppeal { get; set; }
        public string UID { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public string EcliCode { get; set; }
        public string PreviousEcliCode { get; set; }
        public bool IsSynced { get; set; }
        public virtual ActKind ActKind { get; set; }
        public virtual CaseKind CaseKind { get; set; }
        public virtual Court Court { get; set; }
        public virtual Status Status { get; set; }
    }
}
