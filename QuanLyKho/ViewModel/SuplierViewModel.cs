using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class SuplierViewModel:BaseViewModel
    {
        private ObservableCollection<Suplier> _List;
        public ObservableCollection<Suplier> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Suplier _SellectedItem;
        public Suplier SellectedItem { get => _SellectedItem; set { _SellectedItem = value; OnPropertyChanged();
                if (SellectedItem != null)
                { 
                    DisplayName = SellectedItem.DisplayName;
                    Address = SellectedItem.Address;
                    Phone = SellectedItem.Phone;
                    Email = SellectedItem.Email;
                    MoreInfo = SellectedItem.MoreInfo;
                    ContractDate = SellectedItem.ContractDate;
                }} 
        }

        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }
        private string _Address;
        public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }
        private string _Phone;
        public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private string _MoreInfo;
        public string MoreInfo { get => _MoreInfo; set { _MoreInfo = value; OnPropertyChanged(); } }
        private DateTime? _ContractDate;
        public DateTime? ContractDate { get => _ContractDate; set { _ContractDate = value;OnPropertyChanged(); } }

        public ICommand AddComman { get; set; }
        public ICommand EditComman { get; set; }

        public SuplierViewModel()
        {
            List = new ObservableCollection<Suplier>(DataProvider.Ins.DB.Supliers);

            AddComman = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) => {

                var suplier = new Suplier() { DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo, ContractDate = ContractDate };
                DataProvider.Ins.DB.Supliers.Add(suplier);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(suplier);
            });

            EditComman = new RelayCommand<object>((p) =>
            {
                if (SellectedItem == null)
                {
                    return false;
                }
                var displaynameList = DataProvider.Ins.DB.Supliers.Where(u => u.Id == Id);
                if (displaynameList == null || displaynameList.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) => {

                var suplier = DataProvider.Ins.DB.Supliers.Where(x => x.Id == SellectedItem.Id).SingleOrDefault();
                suplier.DisplayName = DisplayName;
                suplier.Address = Address;
                suplier.Phone = Phone;
                suplier.Email = Email;
                suplier.MoreInfo = MoreInfo;
                suplier.ContractDate = ContractDate;
                DataProvider.Ins.DB.SaveChanges();
            });

        }
    }
}
