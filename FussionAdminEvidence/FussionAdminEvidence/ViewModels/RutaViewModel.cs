using FussionAdminEvidence.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FussionAdminEvidence.ViewModels
{
    public class RutaViewModel: Ruta, INotifyPropertyChanged
    {
        #region Properties
        private string rutaNombre;
        private string estado;
        private string nombreChofer;
        #endregion

        #region Attributes

        public string RutaNombre
        {
            set
            {
                if (rutaNombre != value)
                {
                    rutaNombre = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RutaNombre"));
                }
            }
            get
            {
                return rutaNombre;
            }
        }

        public string Estado
        {
            set
            {
                if (estado != value)
                {
                    estado = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Estado"));
                }
            }
            get
            {
                return estado;
            }
        }

        public string NombreChofer
        {
            set
            {
                if (nombreChofer != value)
                {
                    nombreChofer = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NombreChofer"));
                }
            }
            get
            {
                return nombreChofer;
            }
        }
        #endregion

        public RutaViewModel(Ruta ruta)
        {
            this.NombreChofer = ruta.Chofer;
        }

        public RutaViewModel()
        {

        }

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


    }
}
