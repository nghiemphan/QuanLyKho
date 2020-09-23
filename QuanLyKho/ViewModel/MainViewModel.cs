using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyKho.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        public bool isloaded = false;
        public MainViewModel()
        {
            if (!isloaded)
            {
                isloaded = true;
                LoginWindow login = new LoginWindow();
                login.ShowDialog();
            }
        }
    }
}
