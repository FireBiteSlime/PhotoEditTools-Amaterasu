using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace PhotoEditTools
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        Instruments instruments = new Instruments();
        public ImagesProcessing sourceImage { get; set; }

        
        public Window2(ImagesProcessing image)
        {
            this.sourceImage = image;
            this.DataContext = this.sourceImage;
            //if (this.sourceImage.pixels_matrix != null)
            //this.sourceImage.curveData.SetHistoPoints(image.pixels_matrix);
            //instruments.DoActionDraw(image);
            //imageMain.Source = sourceImage.ImageSource;
            InitializeComponent();
        }

        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            
            DrawCurve(withCalculate: true);

            
        }


        private void comboBoxItem_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = sender as ComboBoxItem;
            ComboBox box = item.Parent as ComboBox;

            int channel_index = box.Items.IndexOf(item);

            sourceImage.curveData.channelView = channel_index;


            Window main = instruments.Find_This_Window(typeof(MainWindow));
            Window umain = instruments.Find_This_Window(typeof(Window1));
            Window sfmain = instruments.Find_This_Window(typeof(Window3));

            //(umain as Window1).Remask();
            //instruments.DoActionSpatialFiltering(sourceImage);
            instruments.DoActionDraw(sourceImage);
            instruments.DoActionSpatialFiltering(sourceImage);


            (main as MainWindow).imageMain.Source = sourceImage.ImageSource;
            //Images.Calculate(image);
            DrawHisto();
        }
        /////////////////////////


      /*  private void buttonNewPoint_Click(object sender, RoutedEventArgs e)
        {
            if (!Util.isNatural(textBoxX.Text) || !Util.isNatural(textBoxY.Text)) return;
            var x = Util.Clamp(Convert.ToInt32(textBoxX.Text), 0, 255);
            var y = Util.Clamp(Convert.ToInt32(textBoxY.Text), 0, 255);

            AddPoint(new Point(x, y));
        }*/

       /* private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(paintSurface);

            AddPoint(ToRgbPoint(p, paintSurface));
        }


        private void Point_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var rect = sender as Rectangle;
            var point = (Point)rect.DataContext;
            sourceImage.curveData.points.Remove(point);

            DrawCurve(withCalculate: true);
        }*/

        ///////////////////////

        private void Window_Closed(object sender, EventArgs e)
        {
            Window main = instruments.Find_This_Window(typeof(MainWindow));
            (main as MainWindow).CubicInter.IsEnabled = true;
        }


        private void DrawCurve(bool withCalculate)
        {
            paintSurface.Children.Clear();
            sourceImage.curveData.points.Sort((f, s) => f.X.CompareTo(s.X));

            if (sourceImage.curveData.points.Count >= 2)
            {
                sourceImage.curveData.SetInterpolation();

                //if (withCalculate)  Images.Calculate(image);
                

                Polyline line = new Polyline();
                line.Stroke = Brushes.Pink;
                line.StrokeThickness = 2;
                line.StrokeLineJoin = PenLineJoin.Round;

                Point p_i;
                for (int i = 0; i < 256; i++)
                {
                    p_i = new Point(i, sourceImage.curveData.interpolatedPoints[i]);
                    line.Points.Add(ToCanvasPoint(p_i, paintSurface));
                }

                paintSurface.Children.Add(line);

                Window main = instruments.Find_This_Window(typeof(MainWindow));
                /*Window smain = instruments.Find_This_Window(typeof(Window3));
                
                if (smain != null)
                {
                    instruments.DoActionSpatialFiltering(sourceImage);
                    
                }*/
                instruments.DoActionDraw(sourceImage);
                (main as MainWindow).imageMain.Source = sourceImage.ImageSource;
                DrawHisto();

            }

            foreach (Point point in sourceImage.curveData.points)
            {

                //MessageBox.Show(point.X.ToString() + point.Y.ToString());
                var p = ToCanvasPoint(point, paintSurface);

                Rectangle rect = new Rectangle();
                rect.Width = 8;
                rect.Height = 8;
                Canvas.SetLeft(rect, p.X - 4);
                Canvas.SetTop(rect, p.Y - 4);
                rect.Fill = Brushes.White;
                rect.Stroke = Brushes.Black;
                rect.StrokeThickness = 1;
                rect.DataContext = point;
                rect.MouseRightButtonDown += Point_MouseRightButtonDown;
                paintSurface.Children.Add(rect);
            }

            //var g = instruments.resultCreate(sourceImage);

            //imageMain.Source = g.ImageSource;
            /* int w = sourceImage.width;
             int h = sourceImage.height;
             System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(w, h);

             for (int i = 0; i < w; i++)
             {
                 //temp = txtFile2.ReadLine();
                 //string[] substring = temp.Split(' ');

                 for (int j = 0; j < h; j++)
                 {

                     byte A = sourceImage.pixels_matrix[i, j].A;
                     byte R = sourceImage.pixels_matrix[i, j].R;
                     byte G = sourceImage.pixels_matrix[i, j].G;
                     byte B = sourceImage.pixels_matrix[i, j].B;


                     bmp2.SetPixel(j, i, System.Drawing.Color.FromArgb(A, R, G, B));
                 }

                 //return i;

             }
             sourceImage.ResultImage = bmp2;
             imageMain.Source = sourceImage.ImageSource;*/

            


            
        }

        /*private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
            *//*this.GBForCurve.AllowDrop = true;
            Window main = instruments.Find_This_Window(typeof(MainWindow));
            Window umain = instruments.Find_This_Window(typeof(Window2));
            (main as MainWindow).Rectangles_Visibility(true);
            //main.IsEnabled = false;PhotoEditTools.Window1
            //this.IsEnabled = false;PhotoEditTools.Window1
            (umain as Window2).GBForCurve.IsEnabled = false;
            base.OnMouseLeftButtonDown(e);
            DragMove();

            umain.Hide();
            //umain.IsEnabled = false;
            //MessageBox.Show(Mouse.DirectlyOver.ToString());
            (main as MainWindow).Activate();

            //MessageBox.Show(Mouse.DirectlyOver.ToString());

            DragDrop.DoDragDrop(umain, umain.GetType().ToString(), DragDropEffects.Copy);
            //umain.IsEnabled = true;
            (main as MainWindow).Rectangles_Visibility(false);
            umain.Show();
            (umain as Window2).GBForCurve.IsEnabled = true;*//*
        }*/

        public void DrawHisto()
        {
            gistoSurface.Children.Clear();

            int[] pix_count = sourceImage.curveData.histogramPoints;

            int max = pix_count.Max();
            double rect_width = gistoSurface.ActualWidth / 256.0d;
            double k = gistoSurface.ActualHeight / (max * 1.0d);

            Polygon poly = new Polygon();
            poly.Fill = Brushes.Black;
            Canvas.SetLeft(poly, 0);
            Canvas.SetBottom(poly, 0);

            poly.Points.Add(new Point(0, gistoSurface.ActualHeight));
            for (int i = 0; i < 256; i++)
            {
                double y = Math.Round(gistoSurface.ActualHeight - k * pix_count[i] / gistoSurface.ActualHeight * gistoSurface.ActualHeight);
                double x = i * rect_width;
                poly.Points.Add(new Point(x, y));
                poly.Points.Add(new Point(x + rect_width, y));

            }
            poly.Points.Add(new Point(255 * rect_width + rect_width, gistoSurface.ActualHeight));
            gistoSurface.Children.Add(poly);

            

            
        }

        private void AddPoint(Point point)
        {
            if (sourceImage.curveData.points.Exists(p => p.X == point.X)) return;



            sourceImage.curveData.points.Add(point);


            DrawCurve(withCalculate: true);
        }

        private Point ToCanvasPoint(Point p, Canvas surface)
        {
            var x = Math.Round(p.X / 255 * surface.ActualWidth);
            var y = Math.Round(surface.ActualHeight - p.Y / 255 * surface.ActualHeight);

            return new Point(x, y);
        }

        private Point ToRgbPoint(Point p, Canvas surface)
        {
            var x = Math.Round(p.X / surface.ActualWidth * 255);
            var y = Math.Round(255 - p.Y / surface.ActualHeight * 255);

            return new Point(x, y);
        }

        /*private void comboBoxItem_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = sender as ComboBoxItem;
            ComboBox box = item.Parent as ComboBox;

            int channel_index = box.Items.IndexOf(item);

            image.curving_data.channel_view = channel_index;

            Images.Calculate(image);
            DrawGisto();
        }*/

        /*private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var p = ToRgbPoint(e.GetPosition(paintSurface), paintSurface);

            labelX.Content = p.X.ToString();
            labelY.Content = p.Y.ToString();
        }*/

        /*private void buttonNewPoint_Click(object sender, RoutedEventArgs e)
        {
            if (!Util.isNatural(textBoxX.Text) || !Util.isNatural(textBoxY.Text)) return;
            var x = Util.Clamp(Convert.ToInt32(textBoxX.Text), 0, 255);
            var y = Util.Clamp(Convert.ToInt32(textBoxY.Text), 0, 255);

            AddPoint(new Point(x, y));
        }*/

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var p = e.GetPosition(paintSurface);

            AddPoint(ToRgbPoint(p, paintSurface));

            /*Window umain = instruments.Find_This_Window(typeof(Window1));
            Window main = instruments.Find_This_Window(typeof(MainWindow));

            (main as MainWindow).imageMain.Source = sourceImage.ImageSource;
            (umain as Window1).Remask();*/
        }


        private void Point_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var rect = sender as Rectangle;
            var point = (Point)rect.DataContext;
            sourceImage.curveData.points.Remove(point);



            /*Window main = instruments.Find_This_Window(typeof(MainWindow));

            instruments.DoActionDraw(sourceImage);
            (main as MainWindow).imageMain.Source = sourceImage.ImageSource;*/

            DrawCurve(withCalculate: true);
        }

    }
}
