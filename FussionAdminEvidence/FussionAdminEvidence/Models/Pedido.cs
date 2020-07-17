using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FussionAdminEvidence.Models
{
    public class Pedido
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "numeroPedido")]
        public string NumeroPedido { get; set; }

        [JsonProperty(PropertyName = "fechaPedido")]
        public string FechaPedido { get; set; }

        [JsonProperty(PropertyName = "codigoCliente")]
        public string CodigoCliente { get; set; }

        [JsonProperty(PropertyName = "nombreCliente")]
        public string NombreCliente { get; set; }

        [JsonProperty(PropertyName = "direccionEntrega")]
        public string DireccionEntrega { get; set; }

        [JsonProperty(PropertyName = "numeroCajas")]
        public string NumeroCajas { get; set; }

        [JsonProperty(PropertyName = "folioFactura")]
        public string FolioFactura { get; set; }

        [JsonProperty(PropertyName = "detalle")]
        public string Detalle { get; set; }

        [JsonProperty(PropertyName = "itemCode")]
        public string ItemCode { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public string Quantity { get; set; }

        [JsonProperty(PropertyName = "comentarios")]
        public string Comentarios { get; set; }
    }
}
