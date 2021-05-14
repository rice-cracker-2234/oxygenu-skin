using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using System.Xml;
using System.Xml.Schema;
using OxygenU.Properties;

namespace OxygenU
{
    /// <summary>
    /// Interaction logic for SyntaxEditor.xaml
    /// </summary>

    public partial class Main
    {
        enum InputBoxResult
        {
            OK,
            Cancel
        }

        class InputBox
        {
            private string Title;
            public InputBox(string title = "") => Title = title;

            public string Text;

            public InputBoxResult Show()
            {
                string text = string.Empty;
                InputBoxResult result = InputBoxResult.Cancel;
                InputBoxGUI gui = new();

                void ResultOK(object a, EventArgs b)
                {
                    if (string.IsNullOrWhiteSpace(gui.Input.Text))
                    {
                        MessageBox.Show("Please input something", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    else if (Func.CheckForIllegalCharacter(gui.Input.Text))
                    {
                        MessageBox.Show("Text contains illegal characters", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    else
                    {
                        result = InputBoxResult.OK;
                        text = gui.Input.Text;
                        gui.Close();
                    }
                }

                void ResultCancel(object a, EventArgs b)
                {
                    result = InputBoxResult.Cancel;
                    gui.Close();
                }

                gui.LabelTitle.Content = Title;
                gui.CloseButton.Click += ResultCancel;
                gui.OK.Click += ResultOK;
                gui.Cancel.Click += ResultCancel;

                gui.ShowDialog();

                Text = text;
                return result;
            }
        }
    }
    
    public partial class InputBoxGUI : Window
    {

        public InputBoxGUI()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
