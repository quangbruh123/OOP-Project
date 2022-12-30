using QLBaiDoXe.DBClasses;
using QLBaiDoXe.ParkingLotModel;
using System;
using System.Collections.Generic;
using System.Windows;


namespace QLBaiDoXe
{
    /// <summary>
    /// Interaction logic for SuaNhanVien.xaml
    /// </summary>
    public partial class SuaNhanVien : Window
    {
        Staff staff;
        public SuaNhanVien()
        {
            InitializeComponent();
            
        }
        private string civilID;
        public string CivilID
        {
            get { return civilID; }
            set 
            { 
                civilID = value; 
                staff = new Staff();
                List<Staff> list = new List<Staff>();  
                list =  Staffing.FindStaffByCivilID(civilID); 
                staff = list[0];
            }
        }

        private void TextBoxName_Loaded(object sender, RoutedEventArgs e)
        {
            txbName.Text = staff.StaffName;
        }

        private void txbPhoneNumb_Loaded(object sender, RoutedEventArgs e)
        {
            txbPhoneNumb.Text = staff.PhoneNumber;
        }

        private void txbAccName_Loaded(object sender, RoutedEventArgs e)
        {
            txbAccName.Text = Staffing.GetAccountNameFromStaff(staff);
        }

        private void txbCivilID_Loaded(object sender, RoutedEventArgs e)
        {
            txbCivlID.Text = staff.CivilID;
        }

        private void datePicker_Loaded(object sender, RoutedEventArgs e)
        {
            datePicker.Text = staff.DateOfBirth.ToString();
        }

        private void cbxRole_Loaded(object sender, RoutedEventArgs e)
        {
            if (staff.RoleID == 1)
                cbxRole.Text = "Nhân viên";
            else
                cbxRole.Text = "Quản trị viên";
        }

        private void txbAddress_Loaded(object sender, RoutedEventArgs e)
        {
            txbAddress.Text = staff.StaffAddress;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdjust_Click(object sender, RoutedEventArgs e)
        {
            if (cbxRole.Text == "Quản trị viên")
                Staffing.ChangeStaffInfo(staff.StaffID, txbName.Text, txbCivlID.Text, "admin", txbPhoneNumb.Text, txbAddress.Text, DateTime.Parse(datePicker.Text), txbAccName.Text, txbPassword.Text);
            else
                Staffing.ChangeStaffInfo(staff.StaffID, txbName.Text, txbCivlID.Text, "staff", txbPhoneNumb.Text, txbAddress.Text, DateTime.Parse(datePicker.Text), txbAccName.Text, txbPassword.Text);
        }
    }
}
