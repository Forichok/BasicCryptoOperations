using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BasicCryptoOperations.Behaviors
{
    class BinaryOnlyBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool),
                typeof(BinaryOnlyBehavior), new UIPropertyMetadata(false, OnValueChanged));

        public static bool GetIsEnabled(Control o)
        {
            return (bool) o.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(Control o, bool value)
        {
            o.SetValue(IsEnabledProperty, value);
        }

        private static void OnValueChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            var uiElement = dependencyObject as Control;
            if (uiElement == null) return;
            if (e.NewValue is bool && (bool) e.NewValue)
            {
                uiElement.PreviewTextInput += OnTextInput;
                uiElement.PreviewKeyUp += OnPreviewKeyUp;
                DataObject.AddPastingHandler(uiElement, OnPaste);
            }

            else
            {
                
                uiElement.PreviewTextInput -= OnTextInput;
                uiElement.PreviewKeyUp -= OnPreviewKeyUp;
                DataObject.RemovePastingHandler(uiElement, OnPaste);
            }
        }

        private static void OnTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckTextBox(sender);
        }

        private static void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            CheckTextBox(sender);
        }


        private static void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            CheckTextBox(sender);
        }

        private static void CheckTextBox(object sender)
        {
            if (sender is TextBox)
            {
                var textBox = (sender as TextBox);
                if (textBox.Text.ToLower().Replace("0", "").Replace("1", "").Length != 0)
                {
                    textBox.Background = Brushes.IndianRed;
                }
                else
                {
                    (sender as TextBox).Background = Brushes.White;
                }
            }
        }
    }
}
