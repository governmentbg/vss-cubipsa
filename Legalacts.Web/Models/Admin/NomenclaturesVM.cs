using Legalacts.Model.Entities;
using System.Collections.Generic;

namespace Legalacts.Web.Models
{
    public class NomenclaturesVM
    {
        public List<ActKind> ActKinds { get; set; }
        public List<CaseKind> CaseKinds { get; set; }
    }
}
