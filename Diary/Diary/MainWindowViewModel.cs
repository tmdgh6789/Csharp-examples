using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public class MainWindowViewModel : BindableBase
    {

        #region Command

        public DelegateCommand WriteCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {

                });
            }
        }


        public DelegateCommand ReadCommand
        {
            get
            {
                return new DelegateCommand(delegate ()
                {

                });
            }
        }

        #endregion
    }
}
