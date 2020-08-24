using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FussionAdminEvidence.Models
{
    public class Ruta
    {
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string HoraLlegada { get; set; }
        public string HoraSalida { get; set; }
        public decimal KmSalida { get; set; }
        public decimal KmLlegada { get; set; }
        public string Status { get; set; }
        public string FormattedId { get; set; }
        public int Identifier { get; set; }
        public Chofer Chofer { get; set; }
        public List<Pedido_> DetalleRuta { get; set; }

        //Se agrega para obtener lista de pedidos en servicio para obtener Rutas, en lugar de DetalleRuta viene como Pedidos
        public List<Pedido_> Pedidos { get; set; }
        
        //Se agrega para mostrar en lista de Rutas "Abierta o Cerrada" en lugar de identificadores 1 o 2
        public string StringStatus { get; set; }

    }
}
