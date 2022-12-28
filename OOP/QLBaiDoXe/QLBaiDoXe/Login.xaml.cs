using QLBaiDoXe.DBClasses;
using QLBaiDoXe.ParkingLotModel;
using QLBaiDoXe.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QLBaiDoXe.ViewModel;

namespace QLBaiDoXe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Debug.WriteLine(DataProvider.Ins.DB.Accounts.FirstOrDefault(x => x.AccountName == "admin").AccountName);
            Debug.WriteLine("last log in date: " + Settings.Default.currentDate.ToString());
            if (DateTime.Now.Date != Settings.Default.currentDate.Date)
            {
                Finance.UpdateFinancialReport();
                Settings.Default.todayVehicleIn = Settings.Default.todayVehicleOut = 0;
            }
            Settings.Default.currentDate = DateTime.Now;
            Settings.Default.Save();
            Debug.WriteLine("current date: " + Settings.Default.currentDate.ToString());
            //this.DataContext = new LoginViewModel();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (DBClasses.Account.LogIn(UsernameTextbox.Text, PasswordTextbox.Password))
            {
                MessageBox.Show("Đăng nhập thành công", "Thông báo");
                Homepage1 homepage = new Homepage1();
                homepage.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai thông tin đăng nhập", "Thông báo");
            }
        }
    }
}
