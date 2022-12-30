using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using QLBaiDoXe.ViewModel;
using QLBaiDoXe.Classes;
using AForge.Video;
using AForge.Video.DirectShow;
using QLBaiDoXe.ParkingLotModel;

namespace QLBaiDoXe
{
    /// <summary>
    /// Interaction logic for Homepage1.xaml
    /// </summary>
    public partial class Homepage1 : Window
    {
        DateTime date = DateTime.Now;
        public Homepage1()
        {
            InitializeComponent();
            this.DataContext = new HomePage1ViewModel();
            GetVideoDevices();
            StartCamera();
            this.Closing += Homepage1_Closing;
            VehicleIn_num.Text = DBClasses.ParkingVehicle.GetVehicleInNumber(date).ToString();
            VehicleOut_num.Text = DBClasses.ParkingVehicle.GetVehicleOutNumber(date).ToString();
            VehicleParked_num.Text = DBClasses.ParkingVehicle.GetParkedVehicleNumber().ToString();
        }

        private void Homepage1_Closing(object sender, CancelEventArgs e)
        {
            StopCamera();
        }

        #region Public properties
        public ObservableCollection<FilterInfo> VideoDevicesList { get; set; }

        public FilterInfo CurrentDevice { get; set; }

        public FilterInfo CurrentDevice1 { get; set; }
        #endregion

        #region Private fields

        private IVideoSource _videoSource;

        private IVideoSource _videoSource1;

        #endregion

        #region Code
        public void GetVideoDevices()
        {
            VideoDevicesList = new ObservableCollection<FilterInfo>();
            foreach (FilterInfo filterInfo in new FilterInfoCollection(FilterCategory.VideoInputDevice))
            {
                VideoDevicesList.Add(filterInfo);
            }
            if (VideoDevicesList.Any())
            {
                if (VideoDevicesList.Count == 2)
                {
                    CurrentDevice = VideoDevicesList[0];
                    CurrentDevice1 = VideoDevicesList[1];
                }
                else if (VideoDevicesList.Count == 1)
                {
                    CurrentDevice = VideoDevicesList[0];
                }
            }
            else
            {
                MessageBox.Show("No video sources found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void StartCamera()
        {
            if (CurrentDevice != null && CurrentDevice1 != null)
            {
                _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                _videoSource1 = new VideoCaptureDevice(CurrentDevice1.MonikerString);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource1.NewFrame += video_NewFrame1;
                _videoSource.Start();
                _videoSource1.Start();
            }
            else if (CurrentDevice != null && CurrentDevice1 == null)
            {
                _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
        }
        private void video_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = bitmap.ToBitmapImage();
                }
                bi.Freeze(); // avoid cross thread operations and prevents leaks
                Dispatcher.BeginInvoke(new ThreadStart(delegate { videoPlayer.Source = bi; }));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Phần mềm bị lỗi:" + exc.Message + ". Vui lòng liên hệ nhân viên bảo trì để biết thêm chi tiết", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
        }
        private void video_NewFrame1(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = bitmap.ToBitmapImage();
                }
                bi.Freeze(); // avoid cross thread operations and prevents leaks
                Dispatcher.BeginInvoke(new ThreadStart(delegate { videoPlayer1.Source = bi; }));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Phần mềm bị lỗi:" + exc.Message + ". Vui lòng liên hệ nhân viên bảo trì để biết thêm chi tiết", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
        }

        private void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning && _videoSource1 != null && _videoSource1.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= new NewFrameEventHandler(video_NewFrame);
                _videoSource1.SignalToStop();
                _videoSource1.NewFrame -= new NewFrameEventHandler(video_NewFrame1);
            }

            else if (_videoSource != null && _videoSource.IsRunning && _videoSource1 == null)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= new NewFrameEventHandler(video_NewFrame);
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            image2.Source = null;
            if (textBox1.Text.Length == 10)
            {
                string ID = (textBox1.Text).Trim();
                if (DBClasses.Cards.CheckCardState(long.Parse(ID)) == 1)
                {
                    Dispatcher.BeginInvoke
                        (
                        new Action(() => MessageBox.Show("Thẻ đã được sử dụng", "Lưu ý", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification)),
                        DispatcherPriority.ApplicationIdle
                        );
                    textBox1.Clear();
                }
                else
                {
                    try
                    {
                        textBox1.Clear();

                        image1.Source = videoPlayer.Source;
                        BitmapImage temp = (BitmapImage)image1.Source;
                        Bitmap temp1 = BitmapImageConvert.BitmapImage2Bitmap(temp);

                        dateIn.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                        timeIn.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString(); dateOut_Copy.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                        dateOut_Copy.Text = "--/--/----";
                        timeOut_Copy.Text = "00:00:00";
                        dateIn_Copy.Text = "--/--/----";
                        timeIn_Copy.Text = "00:00:00";
                        vehiclePlate_Copy2.Text = "------";
                        priceTag_Copy.Text = "------" + " đồng";

                        string path = @"C:\Users\Quang\Pictures\test\" + DateTime.Now.ToString().Replace(':', '_').Replace('/', '_') + ".jpg";
                        DBClasses.ParkingVehicle.VehicleIn(vehiclePlate_Copy.Text.ToString().Trim(), long.Parse(ID), path);
                        temp1.Save(path);

                        VehicleIn_num.Text = DBClasses.ParkingVehicle.GetVehicleInNumber(date).ToString();
                        VehicleParked_num.Text = DBClasses.ParkingVehicle.GetParkedVehicleNumber().ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Phần mềm bị lỗi: " + ex.Message + " Vui lòng liên hệ nhân viên bảo trì để biết thêm chi tiết", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            image1.Source = null;
            if (textBox2.Text.Length == 10)
            {
                string ID = (textBox2.Text).Trim();
                if (DBClasses.Cards.CheckCardState(long.Parse(ID)) == 0)
                {
                    Dispatcher.BeginInvoke
                        (
                        new Action(() => MessageBox.Show("Thẻ chưa được sử dụng", "Lưu ý", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification)),
                        DispatcherPriority.ApplicationIdle
                        );
                    textBox2.Clear();
                }
                else
                {
                    try
                    {
                        textBox2.Clear();

                        long ID_temp = long.Parse(ID);
                        var vehicle = DataProvider.Ins.DB.Vehicles.FirstOrDefault(x => x.ParkingCardID == ID_temp && x.VehicleState == 1);
                        string path = vehicle.VehicleImage;
                        Uri imgDir = new Uri(path);
                        image2.Source = new BitmapImage(imgDir);

                        dateOut_Copy.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                        timeOut_Copy.Text = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString();
                        dateIn_Copy.Text = vehicle.TimeStartedParking.Day.ToString() + "/" + vehicle.TimeStartedParking.Month.ToString() + "/" + vehicle.TimeStartedParking.Year.ToString();
                        timeIn_Copy.Text = vehicle.TimeStartedParking.Hour.ToString() + ":" + vehicle.TimeStartedParking.Minute.ToString() + ":" + vehicle.TimeStartedParking.Second.ToString();
                        dateIn.Text = "--/--/----";
                        timeIn.Text = "00:00:00";
                        vehiclePlate_Copy2.Text = vehicle.VehicleType.VehicleTypeName;
                        priceTag_Copy.Text = vehicle.VehicleType.ParkingFee.ToString() + " đồng";

                        DBClasses.ParkingVehicle.VehicleOut(long.Parse(ID), MainWindow.currentUser.StaffID);

                        VehicleOut_num.Text = DBClasses.ParkingVehicle.GetVehicleOutNumber(date).ToString();
                        VehicleParked_num.Text = DBClasses.ParkingVehicle.GetParkedVehicleNumber().ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Phần mềm bị lỗi: " + ex.Message + " Vui lòng liên hệ nhân viên bảo trì để biết thêm chi tiết", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                    }
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                if (vehiclePlate_Copy.Text == "Xe máy")
                {
                    Dispatcher.BeginInvoke(new Action(() => vehiclePlate_Copy.Text = "Xe hơi"));
                }
                else if (vehiclePlate_Copy.Text == "Xe hơi")
                {
                    Dispatcher.BeginInvoke(new Action(() => vehiclePlate_Copy.Text = "Xe đạp"));
                }
                else if (vehiclePlate_Copy.Text == "Xe đạp")
                {
                    Dispatcher.BeginInvoke(new Action(() => vehiclePlate_Copy.Text = "Xe máy"));
                }
            }
        }
        #endregion
    }
}
