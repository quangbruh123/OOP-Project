using ControlzEx.Standard;
using QLBaiDoXe.DBClasses;
using QLBaiDoXe.ParkingLotModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QLBaiDoXe
{
    /// <summary>
    /// Interaction logic for TraCuuXe.xaml
    /// </summary>
    public partial class TraCuuXe : UserControl
    {
        public TraCuuXe()
        {
            InitializeComponent();
        }
        private void Nullify()
        {
            cbxDay.Text = null;
            cbxMonth.Text = null;
            cbxYear.Text = null;
        }
        private bool CheckInput()
        {

            int testDay;
            int.TryParse(cbxDay.Text, out testDay);


            int testMonth;
            int.TryParse(cbxMonth.Text, out testMonth);


            int testYear;
            int.TryParse(cbxYear.Text, out testYear);


            if (testMonth == 2)
            {
                if (DateTime.IsLeapYear(testYear))
                {
                    if (testDay > 29)
                    {
                        MessageBox.Show("Bạn đã nhập ngày không phù hợp");
                        Nullify();
                        return false;
                    }
                }
                else
                {
                    if (testDay > 28)
                    {
                        MessageBox.Show("Bạn đã nhập ngày không phù hợp");
                        Nullify();
                        return false;
                    }
                }
            }
            if (testMonth == 4 || testMonth == 6 || testMonth == 9 || testMonth == 11)
                if (testDay > 30)
                {
                    MessageBox.Show("Bạn đã nhập ngày không phù hợp");
                    Nullify();
                    return false;
                }
            return true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            List<Vehicle> result = new List<Vehicle>();
            //if (TimePicker.Text != null)
            //{
            //    DateTime datesearch = new DateTime(int.Parse(cbxYear.Text), int.Parse(cbxMonth.Text), int.Parse(cbxDay.Text), int.Parse(TimePicker.Text),0,0);
            //    result = ParkingVehicle.SearchVehicle_TimeIn_DateAndHour(datesearch);
            //}    
            //else
            //{
            //    DateTime datesearch = new DateTime(int.Parse(cbxYear.Text), int.Parse(cbxMonth.Text), int.Parse(cbxDay.Text));
            //    result = ParkingVehicle.SearchVehicle_TimeIn_DateOnly(datesearch);

            //}
            DateTime datesearch = new DateTime(int.Parse(cbxYear.Text), int.Parse(cbxMonth.Text), int.Parse(cbxDay.Text));
            result = ParkingVehicle.SearchVehicle_TimeIn_DateOnly(datesearch);
            if (CheckInput() == false) { return; }
              
            if (result.Count == 0)
            {
                MessageBox.Show("Ngày bạn đã nhập không có xe");
            }
            lvResult.ItemsSource = null;
            lvResult.ItemsSource = result;

        }
    }
}
