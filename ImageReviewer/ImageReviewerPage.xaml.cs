using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ImageReviewerPage : Window
    {

        //public bool flag = false;
        public ImagesProcessing processing = new ImagesProcessing();
        public ObservableCollection<SourceImage> images = new ObservableCollection<SourceImage>();
        Instruments instruments = new Instruments();

        public ImageReviewerPage()
        {
            
            InitializeComponent();
            itemsControl.ItemsSource = images;

        }
        public  ObservableCollection<SourceImage> GetImages()
        {
            return images;
        }

      
       

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG image|*.png|JPEG image|*.jpeg|BMP image|*.bmp";
            saveFileDialog.RestoreDirectory = true;

            saveFileDialog.FileName = "PhotoEditTools " + "Date " + DateTime.Now.ToString("dd-MM-yyyy") + " Time " + DateTime.Now.ToString("HH") + "."+ DateTime.Now.ToString("mm") + "." + DateTime.Now.ToString("ss");
            saveFileDialog.DefaultExt = "png";

            if (saveFileDialog.ShowDialog() == true)
                processing.Save(saveFileDialog.FileName);

        }

        private void slider_Click(object sender, RoutedEventArgs e)
        {
            Remask();
        }
        private void buttonUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|Все файлы (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            //MessageBox.Show("Good");
            if (openFileDialog.ShowDialog() == true)
            {
                SourceImage image = new SourceImage(openFileDialog.FileName);
                // MainWindow. = new Bitmap(openFileDialog.FileName);
                /*                
                  Bitmap image = new Bitmap(openFileDialog.FileName);*/
               // Window main = Find_This_Window(typeof(MainWindow));
                images.Add(image);
                Scroll.ScrollToEnd();
                // (main as MainWindow).imageMain.Source = Utilities.GetImageSource(image);
            }

        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("Good");
            
            
            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            /* TextBox winName = new TextBox();
             winName.Text = typeof(Window1).ToString();
             if (e.ChangedButton == MouseButton.Left)
                 this.DragMove();*/
            //this.OnDrop();

            /*else { 
                TextBox winName = new TextBox();
                winName.Text = typeof(Window1).ToString();
                //winName.Text = Cursor.ToString();
                //string winName = Window1.NameProperty.Name;
            
                DragDrop.DoDragDrop(winName,winName.Text, DragDropEffects.Copy);
            }*/
            



        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            //MessageBox.Show("og");
        }

        private void Window_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("g");
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          /*  base.OnMouseLeftButtonDown(e);
            this.DragMove();*/
            
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /*this.Hide();
            
            TextBox winName = new TextBox();
            winName.Text = typeof(Window1).ToString();
            //winName.Text = Cursor.ToString();
            //string winName = Window1.NameProperty.Name;

            DragDrop.DoDragDrop(winName, winName.Text, DragDropEffects.Copy);

            this.Show();*/

        }


        private void TopControlPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void TopControlPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
            Window main = instruments.Find_This_Window(typeof(MainWindow));
            Window umain = instruments.Find_This_Window(typeof(ImageReviewerPage));
            (main as MainWindow).Rectangles_Visibility(true);
            //main.IsEnabled = false;PhotoEditTools.Window1
            //this.IsEnabled = false;PhotoEditTools.Window1
            (umain as ImageReviewerPage).GB.IsEnabled = false;
            base.OnMouseLeftButtonDown(e);
            DragMove();

            umain.Hide();
            //umain.IsEnabled = false;
            //MessageBox.Show(Mouse.DirectlyOver.ToString());
            (main as MainWindow).Activate();

            //MessageBox.Show(Mouse.DirectlyOver.ToString());
            //DragDrop.DoDragDrop(umain, umain, DragDropEffects.Copy);
            //if (flag)
            DragDrop.DoDragDrop(umain, umain.GetType().ToString(), DragDropEffects.Copy);
            //umain.IsEnabled = true;
            (main as MainWindow).Rectangles_Visibility(false);
            umain.Show();
            (umain as ImageReviewerPage).GB.IsEnabled = true;
        }

        private void GroupBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Window1 win = Find_This_Window(typeof(Window1));
            //var win = Parent as Window1;
            //var windows = FinPhotoEditTools.Window1d_This_Window(typeof(Window1));
            //var windows = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive && x.GetType() == typeof(MainWindow));
            //Window window = Find_This_Window(typeof(Window1));
            //Window main = Find_This_Window(typeof(MainWindow));
            //Window mainwindow = Find_This_Window(typeof(MainWindow));

            //(main as MainWindow).But.IsEnabled = true;
            
            //var t = e.GetPosition(main).ToString();
            //var b = Mouse.GetPosition(main).ToString();
            //MessageBox.Show(t + ' ' + b);
            
            //window.Hide();
            
            //MessageBox.Show(e.MouseDevice.Target.GetType().ToString());
            
            //this.Opacity = 0;
            //this.Visibility = 0;
            
            //TextBox winName = new TextBox();
            //winName.Text = typeof(Window1).ToString();
            //winName.Text = Cursor.ToString();
            //string winName = Window1.NameProperty.Name;

            //DragDrop.DoDragDrop(winName, winName.Text, DragDropEffects.Copy);
            //DragDrop.DoDragDrop(this, this.GetType().ToString(), DragDropEffects.Copy);
            //DragDrop.DoDragDrop(this, this.GetType().ToString(), DragDropEffects.Copy);
            //main.IsEnabled = true;
            //(window as Window1).GB.IsEnabled = false;
           //// Mouse.Capture(null);
            //MessageBox.Show(Mouse.DirectlyOver.ToString());


            
            // MessageBox.Show(e.MouseDevice.Target.GetType().ToString());
            /*var t = e.GetPosiPPPhotoEditTools.Window1hotoEditTools.Window1hotoEditTools.Window1tion(main).ToString();
            var b = Mouse.GetPosition(window).ToString();
            MessageBox.Show(t + ' ' + b);*/
            //Mouse.GetPosition(main).Offset
            //MessageBox.Show(Mouse.GetPosition(main).ToString());
            //if (e.GetPosition(main).X == Mouse.GetPosition(main).X && e.GetPosition(main).Y == Mouse.GetPosition(main).Y)

            //MessageBox.Show(Mouse.GetPosition((main as MainWindow).BorderArea).ToString());
            //var height = (main as MainWindow).Height; 
            //var weight = (main as MainWindow).Width;
           
            //MessageBox.Show(height.ToString() + " - " + weight.ToString());
            //MessageBox.Show(height.ToString() + "  ---  " + weight.ToString());
            //MessageBox.Show(Mouse.GetPosition(main).ToString());
            //if (e.GetPosition(main).X > (0 - weight) && e.GetPosition(main).X < 2*weight && e.GetPosition(main).Y > (0 - height) && e.GetPosition(main).Y < 2*height)
            //if (Mouse.GetPosition(main).ToString() == e.GetPosition(main).ToString())
            //{
                
               // MessageBox.Show(Mouse.GetPosition((main as MainWindow).TCP).ToString());
               // MessageBox.Show(e.GetPosition(main).ToString());
               // MessageBox.Show("yes");
              
              //DragDrop.DoDragDrop(window, window.GetType().ToString(), DragDropEffects.Copy);
            //}
           // (main as MainWindow).Rectangles_Visibility(false);
            /* if (this.GetType() != typeof(Window1))
                 win.Show();
             else { this.Show(); }*/
            //this.Show();
            //window.Show();

            

            //InitializeComponent();
            //itemsControl.ItemsSource = images;


            //this.Opacity = 1;

        }


      


        private void comboBoxItem_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = sender as ComboBoxItem;
            ComboBox box = item.Parent as ComboBox;

            int method_index = box.Items.IndexOf(item);

            SourceImage mask = box.DataContext as SourceImage;
            mask.method_view = method_index;

            Remask();
        }

        private void BinarizationComboBoxItem_Click(object sender, RoutedEventArgs e)
        {
            /* ComboBoxItem item = sender as ComboBoxItem;
             ComboBox box = item.Parent as ComboBox;

             var panel = FindContolsByType(itemsControl as DependencyObject, typeof(StackPanel));

             SourceImage source = box.DataContext as SourceImage;

             SetParametrs(source);

             if (box.Items.IndexOf(item) == 0)
             {
                 (panel.ElementAt(1) as StackPanel).Visibility = Visibility.Hidden;
             }*/

            /*ComboBoxItem item = sender as ComboBoxItem;
            ComboBox box = item.Parent as ComboBox;

            SourceImage source = box.DataContext as SourceImage;

            var panel = FindContolsByType(itemsControl as DependencyObject, typeof(StackPanel));

            var text = FindContolsByType(itemsControl as DependencyObject, typeof(TextBox));

            BinarizationType selected = (BinarizationType)box.SelectedIndex;
            if (box.SelectedIndex == 0
                || box.SelectedIndex == 1
                || box.SelectedIndex == 2)
            {
                (panel.ElementAt(1) as StackPanel).Visibility = Visibility.Hidden;
            }
            else
            {
                if (selected != source.binarizationData.type)
                    (text.ElementAt(3) as TextBox).Text = source.binarizationData.defaultParametrs[selected].ToString();
                else
                    (text.ElementAt(3) as TextBox).Text = source.binarizationData.parametrs.ToString();

                (panel.ElementAt(1) as StackPanel).Visibility = Visibility.Visible;
            }*/
        }

        public void Remask()
        {
            var time1 = DateTime.Now;
            Window main = instruments.Find_This_Window(typeof(MainWindow));
            Window cmain = instruments.Find_This_Window(typeof(CurveReviewerPage));
            //List<SourceImage> b = new List<SourceImage>(window.GetImages());

            processing.Blend(images);

            if (images.Count > 0)
            {
                if (processing.ResultImage != null)
                {
                    instruments.DoActionSpatialFiltering(processing);
                    instruments.DoActionDraw(processing);
                }
                
                if (cmain != null)
                    (cmain as CurveReviewerPage).DrawHisto();
            }



            var time2 = DateTime.Now;
            //ImagesProcessing.Text = Math.Round((time2 - time1).TotalMilliseconds).ToString();
            
            (main as MainWindow).imageMain.Source = processing.ImageSource;
            buttonSave.IsEnabled = (main as MainWindow).imageMain.Source == null ? false : true;
            (main as MainWindow).CubicInter.IsEnabled = (main as MainWindow).imageMain.Source == null ? false : true;
            //(main as MainWindow).LinearInter.IsEnabled = (main as MainWindow).imageMain.Source == null ? false : true;
            (main as MainWindow).SpatialFiltering.IsEnabled = (main as MainWindow).imageMain.Source == null ? false : true;
        }

        private void sliderOpacity_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            //Remask();
        }

        private void Upbutton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SourceImage source = button.DataContext as SourceImage;
            int index = images.IndexOf(source);

            if (index > 0 && images.Count() >= 2)
            {
                (images[index], images[index - 1]) = (images[index - 1], images[index]);

                Remask();
            }
        }

        private void Downbutton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SourceImage source = button.DataContext as SourceImage;
            int index = images.IndexOf(source);

            if (index < images.Count() - 1)
            {
                (images[index], images[index + 1]) = (images[index + 1], images[index]);

                Remask();
            }
        }

        private void Removebutton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            SourceImage source = button.DataContext as SourceImage;

            Window main = instruments.Find_This_Window(typeof(MainWindow));
            Window cmain = instruments.Find_This_Window(typeof(CurveReviewerPage));
            Window smain = instruments.Find_This_Window(typeof(SpatialFilteringReviewerPage));

            images.Remove(source);

            if(images.Count == 0)
            {
                if(cmain != null)
                    cmain.Close();
                if(smain != null)
                    smain.Close();
                (main as MainWindow).CubicInter.IsEnabled = false;
                (main as MainWindow).LinearInter.IsEnabled = false;
                (main as MainWindow).SpatialFiltering.IsEnabled = false;

            }

            Remask();

        }

        private void sliderBrightness_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {

            Window main = instruments.Find_This_Window(typeof(MainWindow));
            Slider item = sender as Slider;
            SourceImage mask = item.DataContext as SourceImage;
            //Brightness_control newControl = new Brightness_control();
            var itemSource = images.SingleOrDefault(x => x.bitmap == mask.bitmap);
            if( itemSource != null)
            {
                
                itemSource = Brightness_control(itemSource);
            }
            // mask = Brightness_control(mask);
            (main as MainWindow).imageMain.Source = itemSource.ImageSource;

        }

        private SourceImage Brightness_control(SourceImage image)
        {
            
            ImageAttributes imageAttributes = new ImageAttributes();
            int width = image.width;
            int height = image.height;
            float brightness = image.brightness;

            /*            float[][] colorMatrixElements = {
                                                            new float[] {brightness, 0, 0, 0, 0},
                                                            new float[] {0, brightness, 0, 0, 0},
                                                            new float[] {0, 0, brightness, 0, 0},
                                                            new float[] {0, 0, 0, 1, 0},
                                                            new float[] {0, 0, 0, 0, 1}
                                                        };

                        ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

                        imageAttributes.SetColorMatrix(
                            colorMatrix,
                            ColorMatrixFlag.Default,
                            ColorAdjustType.Bitmap);
                        Graphics graphics = Graphics.FromImage(image.bitmap);
                        graphics.DrawImage(image.bitmap, new System.Drawing.Rectangle(0, 0, width, height), 0, 0, width,
                                               height,
                                               GraphicsUnit.Pixel,
                                               imageAttributes);*/

           //var Pixels_matrix = image.Pixels_matrix;
            byte[] colors = new byte[width * height * 4];

            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int offset = ((y * width) + x) * 4;
                    image.Pixels_matrix[x, y] = new Pixel(colors[(int)image.brightness * offset + 3], colors[(int)image.brightness * offset + 2], colors[(int)image.brightness * offset + 1], colors[(int)image.brightness * offset]);
                    
                }

            }

            return image;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Button buttonOpen = sender as Button;
            SourceImage source = buttonOpen.DataContext as SourceImage;
            //itemsControl.Items[1]
            Grid grid = buttonOpen.Parent as Grid;

            var panel = FindContolsByParentGrid(grid, typeof(StackPanel));

            var button = FindContolsByParentGrid(grid, typeof(Button));

            //var button = FindContolsByType(grid as DependencyObject, typeof(Button));
            //var panel = FindContolsByType(grid as DependencyObject, typeof(StackPanel));

            var box = FindContolsByParentGrid(grid, typeof(ComboBox));


            if (buttonOpen.Content.ToString() == "▼")
            {

                (panel.ElementAt(0) as StackPanel).Height = 100;

                //(panel2 as StackPanel).Height = 100;

                (box.ElementAt(1) as ComboBox).Height = 30;

                (button.ElementAt(1) as Button).Height = 30;

                (button.ElementAt(0) as Button).Content = "▲";


            }

            else
            {
                (button.ElementAt(1) as Button).Height = 0;

                (panel.ElementAt(0) as StackPanel).Height = 0;

                (box.ElementAt(1) as ComboBox).Height = 0;

                (button.ElementAt(0) as Button).Content = "▼";
            }
        }

        private void Set_Button_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            SourceImage source = button.DataContext as SourceImage;

            Grid grid = button.Parent as Grid;

            int index = images.IndexOf(source);


            var box = FindContolsByType(grid, typeof(ComboBox));

            var panel = FindContolsByType(grid, typeof(StackPanel));

            var text = FindContolsByType(panel.ElementAt(0), typeof(TextBox));

            int type_index = (box.ElementAt(1) as ComboBox).SelectedIndex;
            source.binarizationData.typeView = type_index;
            source.binarizationData.windowsSize = Convert.ToInt32((text.ElementAt(0) as TextBox).Text);
            source.binarizationData.parametrs = double.Parse((text.ElementAt(1) as TextBox).Text);


            instruments.DoActionBinarization(source);

            Remask();
            // MessageBox.Show((text.ElementAt(2) as TextBox).Text + "HH" + (text.ElementAt(3) as TextBox).Text + "HH" + (box.ElementAt(1) as ComboBox).SelectedIndex);
        }



        private void SetParametrs( SourceImage source, Grid grid)
        {


           
            var box = FindContolsByParentGrid(grid, typeof(ComboBox)); 

            var panel = FindContolsByParentGrid(grid, typeof(StackPanel)); 

            //var text = FindContolsByType(itemsControl as DependencyObject, typeof(TextBox));

            //var sizeText = (panel.ElementAt(0) as StackPanel).Children[2];
           // var parametrsText = (panel.ElementAt(0) as StackPanel).Children[4];

            var parametrsText = FindContolsByType(panel.ElementAt(0), typeof(TextBox)); 

            BinarizationType selected = (BinarizationType)(box.ElementAt(1) as ComboBox).SelectedIndex;
            if (selected == BinarizationType.None
                || selected == BinarizationType.Gavrilov
                || selected == BinarizationType.Otsu)
            {
                (panel.ElementAt(0) as StackPanel).Visibility = Visibility.Hidden;
            }
            else
            {
                if (selected != source.binarizationData.type)
                    (parametrsText.ElementAt(1) as TextBox).Text = source.binarizationData.defaultParametrs[selected].ToString();
                else
                    (parametrsText.ElementAt(1) as TextBox).Text = source.binarizationData.parametrs.ToString();

                (panel.ElementAt(0) as StackPanel).Visibility = Visibility.Visible;
            }
        }

        private IEnumerable<DependencyObject> FindContolsByType(DependencyObject dObject, Type targetType)
        {
            
            var childCount = VisualTreeHelper.GetChildrenCount(dObject);
            var list = new List<DependencyObject>();
            for (int i = 0; i < childCount; i++)
            {
                var control = VisualTreeHelper.GetChild(dObject, i);
                
                if (control.GetType() == targetType)
                {
                    list.Add(control);
                }
                if (VisualTreeHelper.GetChildrenCount(control) > 0)
                {
                    list.AddRange(FindContolsByType(control, targetType));
                }
            }

            return list;
        }

        private IEnumerable<DependencyObject> FindContolsByParentGrid(Grid grid, Type targetType)
        {

            var childCount = grid.Children.Count;
            var list = new List<DependencyObject>();
            //var result;
            for (int i = 0; i < childCount; i++)
            {
                //grid.Children[i].GetType();

                if (grid.Children[i].GetType() == targetType)
                {
                    list.Add(grid.Children[i]);
                }
                /*if (VisualTreeHelper.GetChildrenCount(control) > 0)
                {
                    list.AddRange(FindContolsByType(control, targetType));
                }*/
            }

            /*foreach(var item in list)
            {
               
              if((item as TextBox).Name == "textBoxSize")
                {
                    MessageBox.Show((item as TextBox).Name);
                }
                
            }*/

            return list;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;

            Grid grid = box.Parent as Grid;

            var panel = FindContolsByType(grid, typeof(StackPanel));

            SourceImage source = box.DataContext as SourceImage;
          

            if (panel.Count() > 0)
            SetParametrs(source, grid);

        }

        private void ComboBoxItem_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
