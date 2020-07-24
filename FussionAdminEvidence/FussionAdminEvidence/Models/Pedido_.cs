using System;
using System.Collections.Generic;
using System.Text;

namespace FussionAdminEvidence.Models
{
    public class Pedido_
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Address2 { get; set; }
        public int Cajas { get; set; }
        public List<Item> Items { get; set; }
        public string FormattedId { get; set; }
        public int Identifier { get; set; }
        public bool IsValid { get; set; }
        public object LastValidationErrorMessages { get; set; }
        public object LastValidationErrors { get; set; }
    }
}
