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
using System.Globalization;
using QLBaiDoXe.DBClasses;
using HandyControl.Tools.Extension;

namespace QLBaiDoXe
{
    /// <summary>
    /// Interaction logic for ThemNhanVien.xaml
    /// </summary>
    public partial class ThemNhanVien : Window
    {
        public ThemNhanVien()
        {
            InitializeComponent();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            string mintime = "1/1/1800";
            if (cbxRole.Text == "Quản trị viên")
            {
                if (DatePicker.Text.IsNullOrEmpty())
                    Staffing.AddAdminInfo(txbName.Text, txbCivilID.Text, txbPhoneNumb.Text, txbAddress.Text, DateTime.Parse(mintime), txbAccName.Text, txbPassword.Text);
                else
                    Staffing.AddAdminInfo(txbName.Text, txbCivilID.Text, txbPhoneNumb.Text, txbAddress.Text, DateTime.Parse(DatePicker.Text), txbAccName.Text, txbPassword.Text);
                MessageBox.Show("Thêm quản trị viên thành công!");
            }
            else
            {
                if (DatePicker.Text.IsNullOrEmpty())
                    Staffing.AddStaffInfo(txbName.Text, txbCivilID.Text, txbPhoneNumb.Text, txbAddress.Text, DateTime.Parse(mintime), txbAccName.Text, txbPassword.Text);
                else
                    Staffing.AddStaffInfo(txbName.Text, txbCivilID.Text, txbPhoneNumb.Text, txbAddress.Text, DateTime.Parse(DatePicker.Text), txbAccName.Text, txbPassword.Text);
                MessageBox.Show("Thêm nhân viên thành công!");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
