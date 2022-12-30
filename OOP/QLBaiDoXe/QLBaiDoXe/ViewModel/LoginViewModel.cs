using QLBaiDoXe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace QLBaiDoXe.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public bool IsLoggedIn { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        private string userName;
        public string UserName {
            get { return userName; }
            set
            {
                userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }
        private string password;
        public string Password {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public LoginViewModel()
        {
            IsLoggedIn = false;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { IsLoggedIn = true; LoginButton_Click(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
        }

        private void LoginButton_Click(Window p)
        {
            var temp1 = userName;
            var temp2 = password;
            if (!string.IsNullOrEmpty(temp1) || !string.IsNullOrEmpty(temp2))
            {
                MessageBox.Show("Bạn chưa điền đủ thông tin", "Thông báo");
                return;
            }
            else
            {
                MainWindow.currentUser = DBClasses.Staffing.LogIn(userName, password);
                if (MainWindow.currentUser != null)
                {
                    MessageBox.Show("Đăng nhập thành công", "Thông báo");
                    admin adminScreen = new admin();
                    Application.Current.MainWindow = adminScreen;
                    Application.Current.MainWindow.Show();
                    p.Close();
                }
                else
                    MessageBox.Show("Sai thông tin đăng nhập", "Thông báo");
            }          
        }
    }
}
