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

namespace PhotoEditTools
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class SpatialFilteringReviewerPage : Window
    {

        public ImagesProcessing result { get; set; }

        Instruments instruments = new Instruments();
        public SpatialFilteringReviewerPage(ImagesProcessing result)
        {
            this.result = result;
            this.DataContext = this.result;
            InitializeComponent();
        }


        private double try_parse(string s)
        {
            double res = 0.0d;
            if (s.IndexOf("/") != -1)
            {
                string[] digs = s.Split("/");
                double d1, d2;
                if (digs.Length >= 2 && Double.TryParse(digs[0], out d1) && Double.TryParse(digs[1], out d2))
                {
                    res = d1 / d2;
                }
            }
            else
            {
                Double.TryParse(s, out res);
            }
            return res;
        }

        private double[,] stringToMatrix(string s)
        {
            string[] lines = s.Split(Environment.NewLine);
            int h = lines.Length;
            int w = lines[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Length;

            double[,] matrix = new double[h, w];

            for (int i = 0; i < h; i++)
            {
                string[] digits = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < w; j++)
                {
                    matrix[i, j] = try_parse(digits[j]);
                }
            }

            return matrix;
        }

        private void buttonSet_Click(object sender, RoutedEventArgs e)
        {
            Window main = instruments.Find_This_Window(typeof(MainWindow));
            Window cmain = instruments.Find_This_Window(typeof(CurveReviewerPage));

            SpatialFilteringType selected = (SpatialFilteringType)comboBox.SelectedIndex;

            if (selected == SpatialFilteringType.Linear)
            {
                result.spatialFilteringData.kernelView = textBoxKernel.Text;
                result.spatialFilteringData.kernel = stringToMatrix(textBoxKernel.Text);

            }
            else if (selected == SpatialFilteringType.Median)
            {
                result.spatialFilteringData.medianRadius = Int32.Parse(textBoxMedianRadius.Text);
            }

            result.spatialFilteringData.type = selected;
            // Images.Calculate(image);
            instruments.DoActionSpatialFiltering(result);
            if (cmain != null)
            {
                instruments.DoActionDraw(result);
                (cmain as CurveReviewerPage).DrawHisto();

            }
            (main as MainWindow).imageMain.Source = result.ImageSource;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SpatialFilteringType selected = (SpatialFilteringType)comboBox.SelectedIndex;
            if (selected == SpatialFilteringType.Linear)
            {
                gridLinearInput.Visibility = Visibility.Visible;
                stackPanelMedian.Visibility = Visibility.Hidden;
            }
            else if (selected == SpatialFilteringType.Median)
            {
                gridLinearInput.Visibility = Visibility.Hidden;
                stackPanelMedian.Visibility = Visibility.Visible;
            }
            else
            {
                gridLinearInput.Visibility = Visibility.Hidden;
                stackPanelMedian.Visibility = Visibility.Hidden;
            }
        }

        private void textBoxKernel_TextChanged(object sender, TextChangedEventArgs e)
        {
            double sum = textBoxKernel.Text
                .Split(Environment.NewLine)
                .Aggregate(0.0d, (double acc, string line) => acc += line
                                                                        .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                                                        .Aggregate(0.0d, (double lacc, string dig) => lacc += try_parse(dig)));

            labelKernelSum.Content = Math.Round(sum, 5).ToString();

        }

        private void buttonGenerateGauss_Click(object sender, RoutedEventArgs e)
        {
            int r = Int32.Parse(textBoxRadiusGauss.Text);
            double sigma = Double.Parse(textBoxSigmaGauss.Text);

            string result = "";
            for (int i = -r; i <= r; i++)
            {
                for (int j = -r; j <= r; j++)
                {
                    double g = 1.0d / (2.0d * Math.PI * sigma * sigma) * Math.Exp(-1.0d * (i * i + j * j) / (2.0d * sigma * sigma));
                    string s = string.Format("{0:0.00000}", Math.Round(g, 5));

                    result += s + " ";
                }

                if (i != r) result += Environment.NewLine;
            }

            textBoxKernel.Text = result;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Window main = instruments.Find_This_Window(typeof(MainWindow));

            (main as MainWindow).SpatialFiltering.IsEnabled = true;
        }
    }
}
