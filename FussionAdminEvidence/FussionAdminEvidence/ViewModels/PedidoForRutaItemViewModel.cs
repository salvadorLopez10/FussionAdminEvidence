using FussionAdminEvidence.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FussionAdminEvidence.ViewModels
{
    public class PedidoForRutaItemViewModel: Pedido_, INotifyPropertyChanged
    {
        #region Attributes
        private bool isChecked;
        public List<Pedido_> listaPedidosRuta;
        #endregion

        #region Properties
        public bool IsChecked
        {
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsChecked"));
                }
            }
            get
            {
                return isChecked;
            }
        }
        #endregion

        public PedidoForRutaItemViewModel()
        {
            listaPedidosRuta = new List<Pedido_>();
        }

        #region Methods
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Commands
        public ICommand SelectPedidoForRutaCommand
        {
            get
            {
                return new RelayCommand(SelectPedidoForRuta);
            }
        }

        
        private void SelectPedidoForRuta()
        {
            //Obtener valor de checkbox
            if (this.IsChecked)
            {
                this.IsChecked = false;
            }
            else
            {
                this.IsChecked = true;
            }
            //PedidosViewModel.GetInstacePedidos().rvm.addListPedidos(listaPedidosRuta);
            //RutaViewModel.GetInstaceRuta().addListPedidos(this);
            var instanceRuta = PedidosViewModel.GetInstaceRutaVM();
            if (instanceRuta.DetalleRuta.Count>0 && this.IsChecked==false){
                //Comprobar que el elemento elegido está en el arreglo y eliminarlo
                //instanceRuta.DetalleRuta.Remove(p=>p.Identifier==this.Identifier);
                var objetoPedido = instanceRuta.DetalleRuta.Find(p=>p.Identifier.Equals(this.Identifier));
                if (objetoPedido!=null)
                {
                    instanceRuta.DetalleRuta.Remove(objetoPedido);
                }
            }
            else
            {
                instanceRuta.DetalleRuta.Add(this);
            }

        }

        #endregion
    }
}
