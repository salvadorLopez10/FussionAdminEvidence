using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace FussionAdminEvidence.ViewModels
{
    public class LoginViewModel
    {
        #region Properties
        public string NombreUsuario { get; set; }

        public string Password { get; set; }

        public bool IsRunning { get; set; }
        #endregion

        #region Constructors
        public LoginViewModel()
        {

        }
        #endregion

        #region Commands
        public ICommand LoginCommand { 
            get; 
            set; 
        }
        #endregion
    }
}
