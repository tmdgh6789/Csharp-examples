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

namespace Csharp_examples
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Point anchorPoint;
        bool isDragging = false;
        private TranslateTransform transform = new TranslateTransform();

        public Window1()
        {
            InitializeComponent();
        }

        private void Canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.Source is Canvas))
            {
                isDragging = true;
                anchorPoint = e.GetPosition(e.Source as FrameworkElement);
                (e.Source as FrameworkElement).CaptureMouse();
            }
        }

        private void Canvas1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(e.Source is Canvas))
            {
                var element = e.Source as FrameworkElement;
                isDragging = false;
                element.ReleaseMouseCapture();

                Point canvas1Position = e.GetPosition(this.Canvas1);

                if (Canvas1.ActualWidth < canvas1Position.X)
                {
                    this.Canvas1.Children.Remove(element);

                    var transform = element.RenderTransform as TranslateTransform;
                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        element.RenderTransform = transform;
                    }

                    Point canvas2Position = e.GetPosition(this.Canvas2);
                    transform.X = canvas2Position.X - (element.ActualWidth / 2);
                    transform.Y = canvas2Position.Y - (element.ActualHeight / 2);

                    this.Canvas2.Children.Add(element);
                }
            }
        }

        private void Canvas1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(e.Source is Canvas))
            {
                var element = e.Source as FrameworkElement;

                if (isDragging && element != null)
                {
                    Point currentPosition = e.GetPosition(this.Canvas1);

                    var transform = element.RenderTransform as TranslateTransform;
                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        element.RenderTransform = transform;
                    }

                    transform.X = currentPosition.X - anchorPoint.X;
                    transform.Y = currentPosition.Y - anchorPoint.Y;
                }
            }
        }

        private void Canvas2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(e.Source is Canvas))
            {
                isDragging = true;
                anchorPoint = e.GetPosition(e.Source as FrameworkElement);
                (e.Source as FrameworkElement).CaptureMouse();
            }
        }

        private void Canvas2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(e.Source is Canvas))
            {
                var element = e.Source as FrameworkElement;
                isDragging = false;
                element.ReleaseMouseCapture();

                Point canvas2Position = e.GetPosition(this.Canvas2);
                if (0 > canvas2Position.X)
                {
                    this.Canvas2.Children.Remove(element);

                    var transform = element.RenderTransform as TranslateTransform;
                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        element.RenderTransform = transform;
                    }

                    Point canvas1position = e.GetPosition(this.Canvas1);
                    transform.X = canvas1position.X - (element.ActualWidth / 2);
                    transform.Y = canvas1position.Y - (element.ActualHeight / 2);

                    this.Canvas1.Children.Add(element);
                }
            }
        }

        private void Canvas2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!(e.Source is Canvas))
            {
                var draggableControl = e.Source as FrameworkElement;

                if (isDragging && draggableControl != null)
                {
                    Point currentPosition = e.GetPosition(this.Canvas2);

                    var transform = draggableControl.RenderTransform as TranslateTransform;
                    if (transform == null)
                    {
                        transform = new TranslateTransform();
                        draggableControl.RenderTransform = transform;
                    }

                    transform.X = currentPosition.X - anchorPoint.X;
                    transform.Y = currentPosition.Y - anchorPoint.Y;
                }
            }
        }

        private void Canvas2_Drop(object sender, DragEventArgs e)
        {
            string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop, true);

            BitmapImage tmpImage = new BitmapImage((new Uri(filenames[0])));
            ImageBrush imgBrush = new ImageBrush() { ImageSource = tmpImage };

            Point currentPosition = e.GetPosition(this.Canvas2);
            var transform = new TranslateTransform();
            Image img = new Image()
            {
                Source = tmpImage,
                Width = 345,
                Height = 150,
                RenderTransform = transform,
            };
            transform.X = currentPosition.X - (img.Width / 2);
            transform.Y = currentPosition.Y - (img.Height / 2);

            Canvas2.Children.Add(img);
        }
    }
}
