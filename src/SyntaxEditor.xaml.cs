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
    public partial class SyntaxEditor : Window
    {
        public SyntaxEditor()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (save.IsEnabled)
                if (MessageBox.Show("You haven\'t saved your work yet. Are you sure you want to exit?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    return;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    WindowState = WindowState.Maximized;
                    break;

                case WindowState.Maximized:
                    WindowState = WindowState.Normal;
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            Editor.Text = Settings.Default.DefaultSyntax;
            Closing += (o, args) =>
            {
                var msgbox = MessageBox.Show("Restart to apply changes?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (msgbox == MessageBoxResult.Yes)
                {
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
            };
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Settings.Default.DefaultSyntax = Editor.Text;
            save.IsEnabled = false;
            save.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(170, 170, 170)
            };
        }

        private void Editor_TextChanged(object sender, EventArgs e)
        {
            if (Editor.Text == Settings.Default.DefaultSyntax)
            {
                save.IsEnabled = false;
                save.Foreground = new SolidColorBrush
                {
                    Color = Color.FromRgb(170, 170, 170)
                };
            }

            else
            {
                save.IsEnabled = true;
                save.Foreground = new SolidColorBrush
                {
                    Color = Color.FromRgb(226, 127, 255)
                };
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var msgBox = MessageBox.Show("Are you sure you want to restore default settings?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msgBox == MessageBoxResult.Yes)
            {
                Settings.Default.DefaultSyntax = Settings.Default.Properties["DefaultSyntax"].DefaultValue as string;
                Editor.Text = Settings.Default.DefaultSyntax;
            }

        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Maximized:
                    minib.Content = "\xE923";
                    windowBord.CornerRadius = new CornerRadius(0);
                    top.CornerRadius = new CornerRadius(0);
                    break;

                case WindowState.Normal:
                    minib.Content = "\xE922";
                    windowBord.CornerRadius = new CornerRadius(10);
                    top.CornerRadius = new CornerRadius(10, 10, 0, 0);
                    break;
            }
        }

        private void top_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                switch (WindowState)
                {
                    case WindowState.Normal:
                        WindowState = WindowState.Maximized;
                        break;

                    case WindowState.Maximized:
                        WindowState = WindowState.Normal;
                        break;
                }
        }
    }
}
