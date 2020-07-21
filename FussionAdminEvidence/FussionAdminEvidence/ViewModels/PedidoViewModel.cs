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
        private Pedido pedido;
        #endregion

        #region Properties
        public Pedido Pedido
        {
            get { return this.pedido; }
            set { SetValue(ref this.pedido, value); }
        }
        #endregion

        #region Constructors

        public PedidoViewModel(Pedido pedido)
        {
            this.pedido = pedido;
        }
        #endregion
    }
}
