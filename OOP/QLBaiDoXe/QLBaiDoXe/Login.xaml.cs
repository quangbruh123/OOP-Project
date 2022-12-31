﻿using QLBaiDoXe.DBClasses;
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
        //public static int currentStaffID;
        public static Account currentUser;
        public MainWindow()
        {
            InitializeComponent();
            Debug.WriteLine(DataProvider.Ins.DB.Accounts.FirstOrDefault(x => x.AccountName == "admin").AccountName);
            Debug.WriteLine("last log in date: " + Settings.Default.currentDate.ToString());
            if (DateTime.Now.Month != Settings.Default.currentDate.Month)
            {
                Finance.CreateFinancialReport();
                Settings.Default.todayVehicleIn = Settings.Default.todayVehicleOut = 0;
            }
            Settings.Default.currentDate = DateTime.Now;
            Settings.Default.Save();
            Debug.WriteLine("current date: " + Settings.Default.currentDate.ToString());
            //this.DataContext = new LoginViewModel();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            currentUser = Staffing.LogIn(UsernameTextbox.Text, PasswordTextbox.Password);
            if (currentUser != null)
            {
                MessageBox.Show("Đăng nhập thành công", "Thông báo");
                if (currentUser.RoleID == 2)
                {
                    admin adminWindow = new admin();
                    adminWindow.Show();
                    this.Close();
                }
                else
                {
                    Homepage1 homepage = new Homepage1();
                    homepage.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Sai thông tin đăng nhập", "Thông báo");
            }
        }

        private void LoginWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                currentUser = Staffing.LogIn(UsernameTextbox.Text, PasswordTextbox.Password);
                if (currentUser != null)
                {
                    MessageBox.Show("Đăng nhập thành công", "Thông báo");
                    if (currentUser.RoleID == 2)
                    {
                        admin adminWindow = new admin();
                        adminWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        Homepage1 homepage = new Homepage1();
                        homepage.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Sai thông tin đăng nhập", "Thông báo");
                }
            }
        }
    }
}
