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
using QLBaiDoXe.ParkingLotModel;

namespace QLBaiDoXe
{
    /// <summary>
    /// Interaction logic for ThemThe.xaml
    /// </summary>
    public partial class ThemThe : Window
    {
        public ThemThe()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (CardID.Text.Length == 10)
            {
                long temp = long.Parse(CardID.Text);
                if (DataProvider.Ins.DB.ParkingCards.Any(x => x.ParkingCardID == temp)) {
                    MessageBox.Show("Mã số thẻ này đã tồn tại", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    DBClasses.Cards.AddCard(long.Parse(CardID.Text));
                    MessageBox.Show("Thêm thẻ thành công");
                    CardID.Clear();
                }
            }
        }
    }
}
