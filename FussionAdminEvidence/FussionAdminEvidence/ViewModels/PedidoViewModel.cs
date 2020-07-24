using FussionAdminEvidence.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace FussionAdminEvidence.ViewModels
{
    public class PedidoViewModel:BaseViewModel
    {
        #region Attributes
        private Pedido_ pedido;
        #endregion

        #region Properties
        public Pedido_ Pedido
        {
            get { return this.pedido; }
            set { SetValue(ref this.pedido, value); }
        }
        #endregion

        #region Constructors

        public PedidoViewModel(Pedido_ pedido)
        {
            this.pedido = pedido;
        }
        #endregion
    }
}
