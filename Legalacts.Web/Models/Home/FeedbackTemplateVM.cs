using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Legalacts.Web.Models
{
    public class FeedbackTemplateVM
    {
        public readonly static string MessageSubjectPrefix = "ПУБЛИКУВАНИ СЪДЕБНИ АКТОВЕ (обратна връзка)";

        public string Subject { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Body { get; set; }
    }
}