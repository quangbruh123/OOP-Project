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
using QLBaiDoXe.ViewModel;

namespace QLBaiDoXe
{
    /// <summary>
    /// Interaction logic for Bang_Cham_Cong.xaml
    /// </summary>
    public partial class Bang_Cham_Cong : Window
    {
        public Bang_Cham_Cong()
        {
            InitializeComponent();
            this.DataContext = new BangChamCongViewModel();
        }
    }
}
