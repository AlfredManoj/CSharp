using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CBMicro.Behaviours
{
    public static class TextBoxSelectionBehaviour
    {
        public static bool GetIsSelectionEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectionEnabledProperty);
        }

        public static void SetIsSelectionEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectionEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsSelectionEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectionEnabledProperty =
            DependencyProperty.Register("IsSelectionEnabled", typeof(bool), typeof(TextBox), new PropertyMetadata(false, OnSelectionEnabled));

        private static void OnSelectionEnabled(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.GotFocus += TextBox_GotFocus;

        }

        private static void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectionStart = 0;
            (sender as TextBox).SelectionLength = (sender as TextBox).Text.Length;
        }
    }
}
