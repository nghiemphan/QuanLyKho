using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace QuanLyKho.ViewModel
{
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unit> _List;
        public ObservableCollection<Unit> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private Unit _SellectedItem;
        public Unit SellectedItem { get => _SellectedItem; set { _SellectedItem = value;OnPropertyChanged(); if (SellectedItem != null) { DisplayName = SellectedItem.DisplayName; } } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value;OnPropertyChanged(); } }

        public ICommand AddComman { get; set; }
        public ICommand EditComman { get; set; }

        public UnitViewModel()
        {
            List = new ObservableCollection<Unit>(DataProvider.Ins.DB.Units);

            AddComman = new RelayCommand<object>((p) => 
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                }
                var displaynameList = DataProvider.Ins.DB.Units.Where(u => u.DisplayName == DisplayName);
                if (displaynameList == null || displaynameList.Count()!=0)
                {
                    return false;
                }
                return true;
            }, (p) => {

                var unit = new Unit() { DisplayName = DisplayName };
                DataProvider.Ins.DB.Units.Add(unit);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(unit);
            });

            EditComman = new RelayCommand<object>((p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) ||SellectedItem==null)
                {
                    return false;
                }
                var displaynameList = DataProvider.Ins.DB.Units.Where(u => u.DisplayName == DisplayName);
                if (displaynameList == null || displaynameList.Count() != 0)
                {
                    return false;
                }
                return true;
            }, (p) => {

                var unit = DataProvider.Ins.DB.Units.Where(x => x.Id == SellectedItem.Id).SingleOrDefault();
                unit.DisplayName = DisplayName;
                DataProvider.Ins.DB.SaveChanges();
                SellectedItem.DisplayName = DisplayName;
            });

        }
    }
}
