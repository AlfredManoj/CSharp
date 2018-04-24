using System;
using System.Windows;
using System.Windows.Media.Media3D;

namespace MyWpf3D
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ScaleTransform3D myRotateTransform3D = new ScaleTransform3D();
            myRotateTransform3D.ScaleX = Convert.ToDouble(tbScaleX.Text);
            myRotateTransform3D.ScaleY = Convert.ToDouble(tbScaleY.Text);
            myRotateTransform3D.ScaleZ = Convert.ToDouble(tbScaleZ.Text);
            
            //Transform3DGroup myTransform3DGroup = new Transform3DGroup();
            //myTransform3DGroup.Children.Add(myRotateTransform3D);
            mygeo.Transform = myRotateTransform3D;
        }
    }
}
