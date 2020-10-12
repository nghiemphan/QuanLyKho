using QuanLyKho.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyKho.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool isLogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _PassWord;
        public string PassWord { get => _PassWord; set { _PassWord = value; OnPropertyChanged(); } }
        
        public ICommand LoginCommand { get; set; }
        public ICommand PassWordCommand { get; set; }
        public LoginViewModel()
        {
            isLogin = false;
            UserName = "";
            PassWord = "";
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            PassWordCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { PassWord=p.Password; });
        }
        void Login(Window p)
        {
            if (p == null)
                return;
            string HassPassword = MD5Hash(Base64Encode(PassWord));
            var accCount = DataProvider.Ins.DB.Users.Where(u => u.UserName == UserName && u.Password == HassPassword).Count();
            if (accCount > 0)
            {
                isLogin = true;
                p.Close();
            }
            else
            {
                isLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu");
            }
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
