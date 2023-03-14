using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Stores
{
    public class ViewStore :BaseViewModel
    {
        public event Action ViewChanged;
        private BaseViewModel _currentViewModel
;

        public BaseViewModel CurrentViewModel

        {
            get { return _currentViewModel; }
            set
            {
                _currentViewModel?.Dispose();
                _currentViewModel = value;
                ViewChanged.Invoke();
            }
        }

    }
}
