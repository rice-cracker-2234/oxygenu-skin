using OxygenU.Properties;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using ICSharpCode.AvalonEdit;
using Newtonsoft.Json.Linq;
using Path = System.IO.Path;
using OxygenUI_API;
// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable IdentifierTypo
// ReSharper disable CollectionNeverQueried.Local
// ReSharper disable SuggestVarOrType_SimpleTypes
// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InvertIf
// ReSharper disable UseObjectOrCollectionInitializer

namespace OxygenU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        private FileSystemWatcher watcher = new();
        private ContextMenu menu = new();

        private API api = new();
        private DispatcherTimer timer = new();
        private DispatcherTimer autoAttachTimer = new();
        private bool robloxOpened;
        private bool fadeInOut = true;

        private JToken jsonScript = JObject.Parse(new WebClient().DownloadString("https://raw.githubusercontent.com/PareX2019/OxygenuLinks/master/scripthub.json"))["scriptHub"];
        private List<string> listHub = new();

        private string defaultText = "-- Oxygen U GUI";
        private string version = "1.0.0";

        public Main()
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
            Application.Current.Shutdown();
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

        private async void autoAttachTimer_Tick(object sender, EventArgs args)
        {
            if (!api.isAttached())
            {
                if (Process.GetProcessesByName("RobloxPlayerBeta").Length > 0)
                {
                    if (!robloxOpened)
                    {
                        robloxOpened = true;
                        await Task.Delay(20000);
                        api.Inject();
                    }
                }

                else
                    robloxOpened = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] lines = new WebClient().DownloadString("https://pastebin.com/raw/YRw5jXe4").Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            );

            if (version != lines[0])
                if (MessageBox.Show("New updates available! Would you like to update?", "", MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Process.Start("Bootstrapper.exe");
                    Application.Current.Shutdown();
                }

            autoAttachTimer.Interval = TimeSpan.FromSeconds(2);
            autoAttachTimer.Tick += autoAttachTimer_Tick;

            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += (_, _) =>
            {
                if (!fadeInOut)
                {
                    bool flag = Opacity > 0.5;
                    if (flag)
                        Opacity -= 0.05;
                    else
                        timer.IsEnabled = false;
                }
                else
                {
                    bool flag2 = Opacity < 1.0;
                    if (flag2)
                        Opacity += 0.05;
                    else
                        timer.IsEnabled = false;
                }
            };
            scriptbox.Text = Path.GetFullPath(Settings.Default.DefaultScriptDirectory);
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            if (!Directory.Exists(scriptbox.Text))
                Directory.CreateDirectory(scriptbox.Text);

            if (!Directory.Exists("./logs"))
                Directory.CreateDirectory("./logs");

            if (!Directory.Exists("./tabs"))
            {
                Directory.CreateDirectory("./tabs");
                File.WriteAllText("./tabs/new 1.lua", defaultText);
            }

            foreach (FileInfo file in new DirectoryInfo("./tabs").GetFiles("*.lua"))
            {
                TextEditor editor = Func.CreateNewEditor();

                TabItem newItem = new() {Header = file.Name, Foreground = Brushes.White, Content = editor};
                ((TextEditor) newItem.Content).Text = File.ReadAllText(file.FullName);

                scriptTabs.Items.Add(newItem);
            }

            scriptTabs.SelectedIndex = 0;


            Func.LoadListBoxFile(listBox, scriptbox.Text, new[] { "*.txt", "*.lua" });

            watcher.Path = scriptbox.Text;

            watcher.NotifyFilter = NotifyFilters.Attributes
                                   | NotifyFilters.CreationTime
                                   | NotifyFilters.DirectoryName
                                   | NotifyFilters.FileName
                                   | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.Security
                                   | NotifyFilters.Size;

            void OnChanged(object obj, EventArgs args) => Dispatcher.Invoke(() =>
            {
                Func.LoadListBoxFile(listBox, scriptbox.Text, new[] { "*.txt", "*.lua" });
            });

            watcher.Changed += OnChanged;
            watcher.Created += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Renamed += OnChanged;

            watcher.Filter = "*.*";
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            MenuItem execute = new MenuItem {Header = "Execute Script"};
            execute.Click += (_, _) =>
            {
                string fileName = (listBox.SelectedItem as ListBoxItem).Content as string;
                Func.Try(() => api.Execute(File.ReadAllText(Path.Combine(scriptbox.Text, fileName))));
            };
            menu.Items.Add(execute);

            menu.Items.Add(new Separator());

            MenuItem Copy = new MenuItem {Header = "Copy"};
            Copy.Click += (_, _) =>
            {
                string fileName = (listBox.SelectedItem as ListBoxItem).Content as string;
                Clipboard.SetText(File.ReadAllText(Path.Combine(scriptbox.Text, fileName)));
            };
            menu.Items.Add(Copy);

            MenuItem Delete = new MenuItem {Header = "Delete"};
            Delete.Click += (_, _) =>
            {
                string fileName = (listBox.SelectedItem as ListBoxItem).Content as string;
                File.Delete(Path.Combine(scriptbox.Text, fileName));
            };
            menu.Items.Add(Delete);

            MenuItem Rename = new MenuItem
            {
                Header = "Rename"
            };
            Rename.Click += (_, _) =>
            {
                InputBox input = new InputBox("Rename");
                if (input.Show() == InputBoxResult.OK)
                {
                    string fileName = (listBox.SelectedItem as ListBoxItem).Content as string;
                    string combinedFileName = Path.Combine(scriptbox.Text, fileName);
                    FileInfo info = new FileInfo(combinedFileName);

                    string newFileName = Path.Combine(info.DirectoryName, input.Text + info.Extension);
                    File.Move(combinedFileName, newFileName);
                }
            };
            menu.Items.Add(Rename);

            MenuItem LoadToEditor = new MenuItem();
            LoadToEditor.Header = "Load script to editor";
            LoadToEditor.Click += (_, _) =>
            {
                if (scriptTabs.SelectedIndex < 0)
                    return;
                string fileName = (listBox.SelectedItem as ListBoxItem).Content as string;
                ((scriptTabs.SelectedItem as TabItem).Content as TextEditor).Text = File.ReadAllText(Path.Combine(scriptbox.Text, fileName));
            };
            menu.Items.Add(LoadToEditor);

            Closing += (o, args) =>
            {
                Settings.Default.Save();
                foreach (TabItem item in scriptTabs.Items)
                {
                    Debug.WriteLine(item.Header);
                    TextEditor editor = item.Content as TextEditor;
                    string fileName = Path.Combine("./tabs/", item.Header.ToString());
                    File.WriteAllText(fileName, editor.Text);
                }
            };

            tmBox.IsChecked = Settings.Default.DefaultTopMost;
            fadecheck.IsChecked = Settings.Default.DefaultFadeInOut;
            autoAttach.IsChecked = Settings.Default.DefaultAutoAttach;

            Topmost = (bool)tmBox.IsChecked;
            autoAttachTimer.IsEnabled = (bool)autoAttach.IsChecked;

            foreach (JToken sub_object in jsonScript.Children())
            {
                hublist.Items.Add($"{jsonScript[sub_object.ToObject<JProperty>().Name]["Name"]}");
                listHub.Add(jsonScript[sub_object.ToObject<JProperty>().Name]["Name"].ToString());
            }

            

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ((scriptTabs.SelectedItem as TabItem).Content as TextEditor).Text = "";
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (scriptTabs.SelectedIndex >= 0)
            ((scriptTabs.SelectedItem as TabItem).Content as TextEditor).Text = Func.GetStringFromFile("All files (*.*)|*.*|TXT files (*.txt)|*.txt|LUA files (*.lua)|*.lua");

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Func.SaveStringToFile(((scriptTabs.SelectedItem as TabItem).Content as TextEditor).Text, "All files (*.*)|*.*|TXT files (*.txt)|*.txt|LUA files (*.lua)|*.lua");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Func.LoadListBoxFile(listBox, scriptbox.Text, new[] { "*.txt", "*.lua" });
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex > -1)
            {
                ListBoxItem item = (ListBoxItem)listBox.SelectedItem;
                item.ContextMenu = menu;
            }
        }
        

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            new SyntaxEditor().ShowDialog();
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

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Multiselect = false,
                Title = "Choose folder"
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                Settings.Default.DefaultScriptDirectory = dialog.FileName;

            scriptbox.Text = Path.GetFullPath(Settings.Default.DefaultScriptDirectory);
            Func.LoadListBoxFile(listBox, scriptbox.Text, new[] { "*.txt", "*.lua" });

            watcher.Path = scriptbox.Text;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            var msgbox = MessageBox.Show("Restore default settings?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (msgbox == MessageBoxResult.Yes)
            {
                Settings.Default.Reset();
                var msgbox2 = MessageBox.Show("Restart the program to apply changes?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (msgbox2 == MessageBoxResult.Yes)
                {
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }

            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Func.Try(() => api.Inject());
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            DirectoryInfo info = new DirectoryInfo("./tabs/");
            string fileName = Path.Combine("./tabs/", "new " + new Random().Next(1, 10000) + ".lua");

            while (File.Exists(fileName))
                fileName = Path.Combine("./tabs/", "new " + new Random().Next(1, 10000) + ".lua");

            File.WriteAllText(fileName, defaultText);
            FileInfo file = new FileInfo(fileName);

            TextEditor editor = Func.CreateNewEditor();

            TabItem newItem = new();
            newItem.Header = file.Name;
            newItem.Foreground = Brushes.White;
            newItem.Content = editor;
            ((TextEditor) newItem.Content).Text = File.ReadAllText(file.FullName);

            scriptTabs.Items.Add(newItem);
            scriptTabs.SelectedItem = newItem;
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if (scriptTabs.SelectedIndex < 0)
                return;
            TabItem item = scriptTabs.SelectedItem as TabItem;
            File.Delete(Path.Combine("./tabs/", item.Header as string));

            scriptTabs.Items.Remove(item);
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            if (scriptTabs.SelectedIndex < 0)
                return;

            TabItem item = scriptTabs.SelectedItem as TabItem;
            InputBox input = new InputBox("Rename");

            if (input.Show() == InputBoxResult.OK)
            {
                string newFileName = Path.Combine("./tabs/", input.Text + ".lua");
                string currentFileName = Path.Combine("./tabs/", item.Header as string);

                if (File.Exists(newFileName))
                {
                    MessageBox.Show(Path.GetFileName(newFileName) + " is already existed, use a different name");
                    return;
                }

                File.Move(currentFileName, newFileName);
                item.Header = Path.GetFileName(newFileName);
            }
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            foreach (Process roblox in Process.GetProcessesByName("RobloxPlayerBeta"))
                roblox.Kill();
        }

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            if (scriptTabs.SelectedIndex >= 0)
                Func.Try(() => api.Execute(((scriptTabs.SelectedItem as TabItem).Content as TextEditor).Text));
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            timer.IsEnabled = false;
            bool check = (bool)fadecheck.IsChecked;
            if (check)
            {
                fadeInOut = true;
                timer.IsEnabled = true;
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            timer.IsEnabled = false;
            bool check = (bool)fadecheck.IsChecked;
            if (check)
            {
                fadeInOut = false;
                timer.IsEnabled = true;
            }
        }

        private void fadecheck_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.DefaultFadeInOut = (bool)fadecheck.IsChecked;
        }

        private void hublist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (JToken sub_object in jsonScript.Children())
            {
                try
                {
                    bool flag = hublist.SelectedItem == null;
                    if (flag)
                    {
                        break;
                    }
                    bool flag2 = jsonScript[sub_object.ToObject<JProperty>().Name]["Name"].ToString() == hublist.SelectedItem.ToString();
                    if (flag2)
                    {
                        hubDesc.Text = jsonScript[sub_object.ToObject<JProperty>().Name]["Description"].ToString();
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(jsonScript[sub_object.ToObject<JProperty>().Name]["Picture"].ToString(), UriKind.Absolute);
                        bitmap.EndInit();
                        hubImage.Source = bitmap;
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            bool flag2 = hublist.SelectedItem == null;
            if (!flag2)
            {
                foreach (JToken sub_object in jsonScript.Children())
                {
                    bool flag3 = jsonScript[sub_object.ToObject<JProperty>().Name]["Name"].ToString() == hublist.SelectedItem.ToString();
                    if (flag3)
                    {
                        Task.Delay(150);
                        Func.Try(() => api.Execute(new WebClient().DownloadString(jsonScript[sub_object.ToObject<JProperty>().Name]["source"].ToString())));
                    }
                }
            }
        }

        private void Checkbox_Click(object sender, RoutedEventArgs e)
        {
            Topmost = (bool)tmBox.IsChecked;
            Settings.Default.DefaultTopMost = (bool)tmBox.IsChecked;
        }

        private void autoAttach_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.DefaultAutoAttach = (bool) autoAttach.IsChecked;
            autoAttachTimer.IsEnabled = (bool)autoAttach.IsChecked;
        }
    }
}

