using System;
using System.Collections.Generic;
using System.Text;

namespace FussionAdminEvidence.Models
{
    public class Ruta
    {
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraLlegada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public double KmSalida { get; set; }
        public double KmLlegada { get; set; }
        public string Status { get; set; }
        public Chofer Chofer { get; set; }
        public List<Pedido_> DetalleRuta { get; set; }
        
    }
}
