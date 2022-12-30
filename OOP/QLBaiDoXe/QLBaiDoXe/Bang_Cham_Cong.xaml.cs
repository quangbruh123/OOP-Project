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
    /// Interaction logic for Bang_Cham_Cong.xaml
    /// </summary>
    public partial class Bang_Cham_Cong : UserControl
    {
        public Bang_Cham_Cong()
        {
            InitializeComponent();
            this.DataContext = new BangChamCongViewModel();
            TimekeepLV.ItemsSource = Staffing.GetTimekeepForMonth(DateTime.Now.Month);
        }

        private void StaffNameTxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(StartDateDP.Text) || string.IsNullOrEmpty(EndDateDP.Text))
                TimekeepLV.ItemsSource = Staffing.GetTimekeepForStaff(StaffNameTxb.Text);
            else 
                TimekeepLV.ItemsSource = Staffing.GetSpecificTimekeeps(StaffNameTxb.Text,
                                        DateTime.Parse(StartDateDP.Text), DateTime.Parse(EndDateDP.Text));
        }
    }
}
