using System;
using System.Collections.Generic;
using System.Text;

namespace FussionAdminEvidence.Models
{
    public class Chofer
    {
        public string Nombre { get; set; }
        public string FormattedId { get; set; }
        public int Identifier { get; set; }
        public bool IsValid { get; set; }
        public object LastValidationErrorMessages { get; set; }
        public object LastValidationErrors { get; set; }
    }
}
