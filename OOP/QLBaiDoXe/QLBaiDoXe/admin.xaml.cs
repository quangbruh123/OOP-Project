using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using QLBaiDoXe.DBClasses;
using QLBaiDoXe.ViewModel;
namespace QLBaiDoXe
{
    /// <summary>
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        public static DateTime LoginTime = new DateTime();
        public static DateTime LogoutTime = new DateTime();
        public admin()
        {
            InitializeComponent();
            this.DataContext = new AdminViewModel();
            LoginTime = DateTime.Now;
            StaffNameTxt.Text = MainWindow.currentUser.Staff.StaffName;
            StaffAccountTxt.Text = MainWindow.currentUser.AccountName;
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Staffing.LogOut(MainWindow.currentUser.AccountName);
            MainWindow.currentUser = null;
            MainWindow loginWindow = new MainWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                Homepage1 add = new Homepage1();
                add.Show();
                this.Close();
            }
        }
    }
}
