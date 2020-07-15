using FussionAdminEvidence.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FussionAdminEvidence.Infrastructure
{
    public class InstanceLocator
    {
        #region Properties
        public MainViewModel Main { get; set; } 
        #endregion

        #region Contructors
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        } 
        #endregion
    }
}
