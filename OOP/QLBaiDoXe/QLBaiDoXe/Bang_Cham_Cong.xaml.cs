using QLBaiDoXe.DBClasses;
using QLBaiDoXe.ViewModel;
using System;
using System.Windows.Controls;

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
            {
                string sdDay = StartDateDP.Text.Split('/')[0];
                string sdMonth = StartDateDP.Text.Split('/')[1];
                string sdYear = StartDateDP.Text.Split('/')[2];
                string edDay = EndDateDP.Text.Split('/')[0];
                string edMonth = EndDateDP.Text.Split('/')[1];
                string edYear = EndDateDP.Text.Split('/')[2];
                if (int.TryParse(sdYear, out int sdYearNum) && int.TryParse(sdMonth, out int sdMonthNum) && int.TryParse(sdDay, out int sdDayNum)
                    && int.TryParse(edYear, out int edYearNum) && int.TryParse(edMonth, out int edMonthNum) && int.TryParse(sdDay, out int edDayNum))
                {
                    DateTime startDate = new DateTime(sdYearNum, sdMonthNum, sdDayNum, 0, 0, 0);
                    DateTime endDate = new DateTime(edYearNum, edMonthNum, edDayNum, 0, 0, 0);
                    TimekeepLV.ItemsSource = Staffing.GetSpecificTimekeeps(StaffNameTxb.Text, startDate, endDate);
                }
                
            }
        }
    }
}
