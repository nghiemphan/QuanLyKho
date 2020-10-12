using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        private ObservableCollection<Inventory> _InventoryList;
        public ObservableCollection<Inventory> InventoryList { get => _InventoryList;set { _InventoryList = value;OnPropertyChanged(); } }


        public bool isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand UnitComman { get; set; }
        public ICommand SuplierComman { get; set; }
        public ICommand CustomerComman { get; set; }
        public ICommand ObjectComman { get; set; }
        public ICommand UserComman { get; set; }
        public ICommand InputComman { get; set; }
        public ICommand OutputComman { get; set; }
        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return  true; }, (p) => {
                isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow login = new LoginWindow();
                login.ShowDialog();
                if (login.DataContext == null)
                    return;
                var loginVM = login.DataContext as LoginViewModel;
                if (loginVM.isLogin)
                {
                    p.Show();
                    LoadInventory();
                }
                else
                {
                    p.Close();
                }
            });
            UnitComman = new RelayCommand<object>((p) => { return true; }, (p) => {
                UnitWindow wd = new UnitWindow();
                wd.ShowDialog();
            });
            SuplierComman = new RelayCommand<object>((p) => { return true; }, (p) => {
                SuplierWindow wd = new SuplierWindow();
                wd.ShowDialog();
            });
            CustomerComman = new RelayCommand<object>((p) => { return true; }, (p) => {
                CustomerWindow wd = new CustomerWindow();
                wd.ShowDialog();
            });
            ObjectComman = new RelayCommand<object>((p) => { return true; }, (p) => {
                ObjectWindow wd = new ObjectWindow();
                wd.ShowDialog();
            });
            UserComman = new RelayCommand<object>((p) => { return true; }, (p) => {
                UserWindow wd = new UserWindow();
                wd.ShowDialog();
            });
            InputComman = new RelayCommand<object>((p) => { return true; }, (p) => {
                InputWindow wd = new InputWindow();
                wd.ShowDialog();
            });
            OutputComman = new RelayCommand<object>((p) => { return true; }, (p) => {
                OutputWindow wd = new OutputWindow();
                wd.ShowDialog();
            });
        }

        void LoadInventory()
        {
            InventoryList = new ObservableCollection<Inventory>();
            var objectlist = DataProvider.Ins.DB.Objects;
            int i = 1;
            foreach(var item in objectlist)
            {
                var inputlist = DataProvider.Ins.DB.InputInfoes.Where(p => p.IdObject == item.Id);
                var outputlist = DataProvider.Ins.DB.OutputInfoes.Where(p => p.IdObject == item.Id);
                int suminput = 0;
                int sumoutput = 0;
                if (inputlist != null)
                {
                    suminput = (int)inputlist.Sum(p => p.Count);
                }
                if (outputlist != null)
                {
                    sumoutput = (int)outputlist.Sum(p => p.Count);
                }
                Inventory invertory = new Inventory();
                invertory.STT = i;
                invertory.Count = suminput - sumoutput;
                invertory.Object = item;
                InventoryList.Add(invertory);
                i++;
            }
        }
    }
}
