using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalacts.Model.Entities
{
    public partial class Act
    {
        public Act()
        {
            this.ConnectedCases = new List<ConnectedCase>();
            this.ConnectedActs = new List<Act>();
            this.Acts = new List<Act>();
        }

        public int ActId { get; set; }
        public Nullable<int> ActNumber { get; set; }
        public int CaseNumber { get; set; }
        public string Judge { get; set; }
        public int ActKindId { get; set; }
        public int CaseKindId { get; set; }
        public int CaseYear { get; set; }
        public Nullable<int> ActYear { get; set; }
        public int CourtId { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<int> ActDocumentId { get; set; }
        public Nullable<int> MotiveDocumentId { get; set; }
        public Nullable<System.DateTime> MotiveDate { get; set; }
        public Nullable<System.DateTime> LegalDate { get; set; }
        public Nullable<int> HigherCourtId { get; set; }
        public int StatusId { get; set; }
        public string ResultOfAppeal { get; set; }
        public string UID { get; set; }
        public string EcliCode { get; set; }
        public string PreviousEcliCode { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public virtual ActDocument ActDocument { get; set; }
        public virtual ActKind ActKind { get; set; }
        public virtual CaseKind CaseKind { get; set; }
        public virtual Court Court { get; set; }
        public virtual HigherCourt HigherCourt { get; set; }
        public virtual MotiveDocument MotiveDocument { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<ConnectedCase> ConnectedCases { get; set; }
        public virtual ICollection<Act> ConnectedActs { get; set; }
        public virtual ICollection<Act> Acts { get; set; }

        public string ResultOfAppealDescription { get; set; }

        [NotMapped]
        public bool IsDeleted {get;set;}
    }
}
