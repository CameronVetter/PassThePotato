using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PassThePotato.Monitor
{
    /// <summary>
    /// http://jobijoy.blogspot.com/2008/04/simple-radial-panel-for-wpf-and.html
    /// </summary>
    public class RadialPanel : Panel
    {
        //public RadialPanel()
        //{
        //    Contents= new ObservableCollection<PlayerControl>();
                
        //}

        //public static readonly DependencyProperty ContentsProperty = DependencyProperty.RegisterAttached(nameof(Contents), typeof(ObservableCollection<PlayerControl>), typeof(RadialPanel), new PropertyMetadata());

        //public ObservableCollection<PlayerControl> Contents { get; set; }

        // Measure each children and give as much room as they want 
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement elem in Children)
            {
                //Give Infinite size as the avaiable size for all the children
                elem.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            }

            return base.MeasureOverride(availableSize);
        }

        //Arrange all children based on the geometric equations for the circle.
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (Children.Count == 0)
                return finalSize;

            double angle = 0;

            //Degrees converted to Radian by multiplying with PI/180
            double incrementalAngularSpace = 360.0 / Children.Count * (Math.PI / 180);

            //An approximate radii based on the avialable size , obviusly a better approach is needed here.
            double radiusX = finalSize.Width / 2.4;
            double radiusY = finalSize.Height / 2.4;

            foreach (UIElement elem in Children)
            {
                //Calculate the point on the circle for the element
                Point childPoint = new Point(Math.Cos(angle) * radiusX, -Math.Sin(angle) * radiusY);

                //Offsetting the point to the Avalable rectangular area which is FinalSize.
                Point actualChildPoint = new Point(finalSize.Width / 2 + childPoint.X - elem.DesiredSize.Width / 2, finalSize.Height / 2 + childPoint.Y - elem.DesiredSize.Height / 2);

                //Call Arrange method on the child element by giving the calculated point as the placementPoint.
                elem.Arrange(new Rect(actualChildPoint.X, actualChildPoint.Y, elem.DesiredSize.Width, elem.DesiredSize.Height));

                //Calculate the new _angle for the next element
                angle += incrementalAngularSpace;
            }

            return finalSize;
        }

    }
}
