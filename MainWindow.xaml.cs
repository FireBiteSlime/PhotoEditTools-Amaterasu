using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PhotoEditTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Instruments instruments = new Instruments();
        public MainWindow()
        {
            Window1 helpWin = new Window1();
           // Window2 interWin = new Window2(helpWin.processing);
            InitializeComponent();
            //this.Left = (SystemParameters.PrimaryScreenWidth - helpWin.Width + interWin.Width - this.Width) / 2;
            this.Left = (SystemParameters.PrimaryScreenWidth - helpWin.Width + 458 - this.Width) / 2;
            this.Top = (SystemParameters.PrimaryScreenHeight - this.Height) / 2 - 50;
            helpWin.Close();
            //interWin.Close();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            
        }



        private void MainImage_CubicInterpolation(object sender, RoutedEventArgs e)
        {
            Window umain = instruments.Find_This_Window(typeof(Window1));
            Window smain = instruments.Find_This_Window(typeof(Window3));

            var Height = 440;

            if (smain == null)
            {
                Height = 0;
            }
            //MessageBox.Show("OK");
            Window2 cubicWin = new Window2((umain as Window1).processing);
            cubicWin.Owner = this;
            cubicWin.Left = this.Left  - cubicWin.Width + 16.5;
            cubicWin.Top = this.Top + Height;
            cubicWin.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            this.CubicInter.IsEnabled = false;
            cubicWin.Show();
        }

        private void MainImage_SpatialFiltering(object sender, RoutedEventArgs e)
        {
            Window umain = instruments.Find_This_Window(typeof(Window1));
            Window cmain = instruments.Find_This_Window(typeof(Window2));

            var Height = 407;

            if(cmain == null)
            {
                Height = 0;
            }

            //MessageBox.Show("OK");
            Window3 spatialWin = new Window3((umain as Window1).processing);
            spatialWin.Owner = this;
            spatialWin.Left = this.Left - spatialWin.Width + 14.5;
            spatialWin.Top = this.Top + Height;
            spatialWin.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            this.SpatialFiltering.IsEnabled = false;
            spatialWin.Show();
        }

        private void MainImage_LinearInterpolation(object sender, RoutedEventArgs e)
        {
            
        }
        public void Maximazed_Window_Position_Fix( Window window, string PanelName)
        {
            window.Height = SystemParameters.PrimaryScreenHeight / 5;
            window.Width = SystemParameters.PrimaryScreenWidth / 5;
            switch (PanelName)
            {
                case "TFP":
                        window.Left = 0;
                        window.Top = 0 + 37;
                        window.Width = SystemParameters.PrimaryScreenWidth;
                    break;
                case "TRP":
                        window.Left = 4 * window.Width;
                        window.Top = 0 + 37;
                    break;
                case "TLP":
                        window.Left = 0;
                        window.Top = 0 + 37;
                    break;
                case "TCP":
                        window.Left = 2 * window.Width;
                        window.Top = 0 + 37;
                    break;
                case "RFP":
                        window.Left = 4 * window.Width;
                        window.Top = 0 + 40;
                        window.Height = SystemParameters.PrimaryScreenHeight - 75;
                    break;
                case "RTP":
                    goto case "TRP";
                case "RBP":
                        window.Left = 4 * window.Width;
                        window.Top = 4 * window.Height - 35;
                    break;
                case "RCP":     
                        window.Left = 4 * window.Width;
                        window.Top = 2 * window.Height;

                    break;
                case "BFP": 
                        window.Left = 0;
                        window.Top = 4 * window.Height - 35;
                        window.Width = SystemParameters.PrimaryScreenWidth ;
                    break;
                case "BRP":
                    goto case "RBP";
                case "BLP":
                        window.Left = 0;
                        window.Top = 4 * window.Height - 35;
                    break;
                case "BCP":
                        window.Left = 2 * window.Width;
                        window.Top = 4 * window.Height - 35;
                    break;

                case "LFP":
                        window.Left = 0;
                        window.Top = 0 + 40;
                        window.Height = 5 * window.Height - 75;
                    break;
                case "LTP":
                    goto case "TLP";
                case "LBP":
                    goto case "BLP";
                case "LCP":
                        window.Left = 0;
                        window.Top = 2 * window.Height;
                    break;
            }
            window.Top += 30;
            window.Height -= 30;
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
        }
        public double[] Location_Window_Fix(string panelname)
        {
            double[] position = new double[4];
            position[2] = this.Width / 3;
            position[3] = this.Height / 3;
            switch (panelname)
            {
                case "TFP" :
                    position[0] = this.Left;
                    position[1] = this.Top + 35;
                    position[2] = this.Width;
                    break;
                case "TRP":
                    position[0] = this.Left + 2 * position[2];
                    position[1] = this.Top + 35;
                    break;
                case "TCP":
                    position[0] = this.Left + position[2];
                    position[1] = this.Top + 35;
                    break;
                case "TLP":
                    position[0] = this.Left;
                    position[1] = this.Top + 35;
                    break;
                case "RFP":
                    position[0] = this.Left + 2 * position[2];
                    position[1] = this.Top + 35;
                    position[3] = this.Height - 35;
                    break;
                case "RTP":
                    goto case "TRP";
                case "RCP":
                    position[0] = this.Left + 2 * position[2];
                    position[1] = this.Top + position[3];
                    break;
                case "RBP":
                    position[0] = this.Left + 2 * position[2];
                    position[1] = this.Top + 2 * position[3];
                    break;
                case "BFP":
                    position[0] = this.Left;
                    position[1] = this.Top + 2 * position[3];
                    position[2] = this.Width;
                    break;
                case "BRP":
                    goto case "RBP";
                case "BCP":
                    position[0] = this.Left + position[2];
                    position[1] = this.Top + 2 * position[3];
                    break;
                case "BLP":
                    position[0] = this.Left;
                    position[1] = this.Top + 2 * position[3];
                    break;
                case "LFP":
                    position[0] = this.Left;
                    position[1] = this.Top + 35;
                    position[3] = this.Height - 35;
                    break;
                case "LTP":
                    goto case "TLP";
                case "LCP":
                    position[0] = this.Left;
                    position[1] = this.Top + position[3];
                    break;
                case "LBP":
                    goto case "BLP";
            }
            position[1] += 30;
            position[3] -= 30;
            return position;
        }
        public void Window_Position_Fix(string WindowName, string PanelName)
        {
            double[] position;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType().ToString() == WindowName)
                {
                    if (this.WindowState == WindowState.Maximized)
                    {
                        Maximazed_Window_Position_Fix(window, PanelName);
                    }
                    else
                    {
                        position = Location_Window_Fix(PanelName);
                        window.Left = position[0];
                        window.Top = position[1];
                        window.Width = position[2];
                        window.Height = position[3];
                        window.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
                    }
                }
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            
        }

        public void Rectangles_Visibility(bool state)
        {
            if (state)
            {
                TFP.Visibility = Visibility.Visible;
                TLP.Visibility = Visibility.Visible;
                TCP.Visibility = Visibility.Visible;
                TRP.Visibility = Visibility.Visible;

                RFP.Visibility = Visibility.Visible;
                RTP.Visibility = Visibility.Visible;
                RCP.Visibility = Visibility.Visible;
                RBP.Visibility = Visibility.Visible;

                BFP.Visibility = Visibility.Visible;
                BRP.Visibility = Visibility.Visible;
                BCP.Visibility = Visibility.Visible;
                BLP.Visibility = Visibility.Visible;

                LFP.Visibility = Visibility.Visible;
                LBP.Visibility = Visibility.Visible;
                LCP.Visibility = Visibility.Visible;
                LTP.Visibility = Visibility.Visible;
            }
            else
            {
                TFP.Visibility = Visibility.Hidden;
                TLP.Visibility = Visibility.Hidden;
                TCP.Visibility = Visibility.Hidden;
                TRP.Visibility = Visibility.Hidden;
                                            
                RFP.Visibility = Visibility.Hidden;
                RTP.Visibility = Visibility.Hidden;
                RCP.Visibility = Visibility.Hidden;
                RBP.Visibility = Visibility.Hidden;
                                            
                BFP.Visibility = Visibility.Hidden;
                BRP.Visibility = Visibility.Hidden;
                BCP.Visibility = Visibility.Hidden;
                BLP.Visibility = Visibility.Hidden;
                                           
                LFP.Visibility = Visibility.Hidden;
                LBP.Visibility = Visibility.Hidden;
                LCP.Visibility = Visibility.Hidden;
                LTP.Visibility = Visibility.Hidden;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Rectangles_Visibility(false);
            Window1 helpWin = new Window1();
            helpWin.Owner = this;
            helpWin.Left = this.Left + this.Width - 14;
            helpWin.Top = this.Top ;
            helpWin.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            helpWin.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }


        private void TFP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();
            Window_Position_Fix(winName, "TFP");
        }

        private void TLP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();
            Window_Position_Fix(winName, "TLP");
        }

        private void TRP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();
            Window_Position_Fix(winName, "TRP");
        }

        private void TCP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();
            Window_Position_Fix(winName, "TCP");
        }
        private void RFP_Drop(object sender, DragEventArgs e)
        {
            //this.CaptureMouse();
            //MessageBox.Show("ff");
            //string winName = e.GetType().ToString();
            
            string winName = e.Data.GetData(DataFormats.Text).ToString();
            // Window1 win = e.Data.GetData(typeof(Window1));
            //string winName = e.Data.GetData();

            Window_Position_Fix(winName, "RFP");
        }

        private void RTP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();

            Window_Position_Fix(winName, "RTP");
        }

        private void RCP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();

            Window_Position_Fix(winName, "RCP");
        }

        private void RBP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();

            Window_Position_Fix(winName, "RBP");
        }

        private void BFP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();

            Window_Position_Fix(winName, "BFP");
        }

        private void BRP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();

            Window_Position_Fix(winName, "BRP");
        }

        private void BCP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();

            Window_Position_Fix(winName, "BCP");
        }

        private void BLP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();

            Window_Position_Fix(winName, "BLP");
        }

        private void LFP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();
            Window_Position_Fix(winName, "LFP");

        }

        private void LBP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();
            Window_Position_Fix(winName, "LBP");
        }

        private void LCP_Drop(object sender, DragEventArgs e)
        {
            string winName = e.Data.GetData(DataFormats.Text).ToString();
            Window_Position_Fix(winName, "LCP");
        }

        private void LTP_Drop(object sender, DragEventArgs e)
        {
            
            string winName = e.Data.GetData(DataFormats.Text).ToString();
            Window_Position_Fix(winName, "LTP");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(Mouse.DirectlyOver.ToString());
        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }






        /*private void RFP_PreviewDrop(object sender, DragEventArgs e)
        {
            Window umain = instruments.Find_This_Window(typeof(Window1));
            (umain as Window1).flag = true;
        }*/
    }
}
