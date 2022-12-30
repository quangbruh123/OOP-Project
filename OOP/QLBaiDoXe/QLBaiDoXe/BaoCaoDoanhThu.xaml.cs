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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using QLBaiDoXe.DBClasses;
using QLBaiDoXe.ParkingLotModel;

namespace QLBaiDoXe
{
    /// <summary>
    /// Interaction logic for BaoCaoDoanhThu.xaml
    /// </summary>
    public partial class BaoCaoDoanhThu : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public BaoCaoDoanhThu()
        {
            InitializeComponent();
            SeriesCollection = new SeriesCollection();
            UpdateReport(DateTime.Now.Year);

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            YFormatter = value => value + "";

            //modifying the series collection will animate and update the chart
            /*SeriesCollection.Add(new LineSeries
            {
                Title = "Series 4",
                Values = new ChartValues<double> { 5, 3, 2, 4 },
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines
                PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                PointForeground = Brushes.Gray
            });*/

            //modifying any series values will also animate and update the chart
            //SeriesCollection[3].Values.Add(5d);

            DataContext = this;
        }

        private void YearTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool isNumber = int.TryParse(YearTextbox.Text, out int year);
            if (isNumber)
                UpdateReport(year);
            else
            {               
                return;
            }
        }

        private void GetReportButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateReport(int year)
        {
            FinancialReport financialReport;
            List<VehicleType> vehicleTypes = Regulation.GetAllVehicleTypes();
            List<LineSeries> lineSeries = new List<LineSeries>();
            int total = 0;
            for (int i = 0; i < vehicleTypes.Count; i++)
            {
                lineSeries.Add(new LineSeries()
                {
                    Title = vehicleTypes[i].VehicleTypeName,
                    Values = new ChartValues<int>()
                });
            }
            for (int m = 1; m <= 12; m++)
            {
                financialReport = Finance.GetFinancialReportForDate(m, year);
                if (financialReport == null)
                {
                    for (int j = 0; j < lineSeries.Count; j++)
                    {
                        lineSeries[j].Values.Add(0);
                    }
                }
                else
                {
                    for (int j = 0; j < lineSeries.Count; j++)
                    {
                        int income = 0;
                        List<Receipt> receipts = financialReport.Receipts.ToList();
                        for (int k = 0; k < financialReport.Receipts.Count; k++)
                        {
                            if (lineSeries[j].Title == receipts[k].Vehicle.VehicleType.VehicleTypeName)
                            {
                                income += receipts[k].ParkingFee;
                            }
                            lineSeries[j].Values.Add(income);
                            total += income;
                        }
                    }
                }
            }
            IncomeTextbox.Text = total.ToString() + " đồng";
            SeriesCollection.Clear();
            SeriesCollection.AddRange(lineSeries);
        }
    }
}
