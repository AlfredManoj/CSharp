
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace CBMicro.Behaviours
{
    public class DebugAction : Behavior<DependencyObject>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            (AssociatedObject as Button).Loaded += DebugAction_Loaded;
        }

        private void DebugAction_Loaded(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if(button != null)
            {
                Message.SetAttach(button, "");
            }
        }

        private void SampleBehaviour_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            (AssociatedObject as Button).Loaded -= DebugAction_Loaded;
            //(AssociatedObject as Button).PreviewMouseLeftButtonDown -= SampleBehaviour_PreviewMouseLeftButtonDown;
        }
    }
}
