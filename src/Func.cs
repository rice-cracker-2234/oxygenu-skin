using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using OxygenU.Properties;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;

namespace OxygenU
{
    public class Func
    {
        private string autoCompleteItems =
            "string.byte string.char string.dump string.find string.format string.gsub string.len string.lower string.rep string.sub string.upper table.concat table.insert table.remove table.sort math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.deg math.exp math.floor math.frexp math.ldexp math.log math.max math.min math.pi math.pow math.rad math.random math.randomseed math.sin math.sqrt math.tan" +
            " string.gfind string.gmatch string.match string.reverse string.pack string.packsize string.unpack table.foreach table.foreachi table.getn table.setn table.maxn table.pack table.unpack table.move math.cosh math.fmod math.huge math.log10 math.modf math.mod math.sinh math.tanh math.maxinteger math.mininteger math.tointeger math.type math.ult" +
            " bit32.arshift bit32.band bit32.bnot bit32.bor bit32.btest bit32.bxor bit32.extract bit32.replace bit32.lrotate bit32.lshift bit32.rrotate bit32.rshift" +
            " utf8.char utf8.charpattern utf8.codes utf8.codepoint utf8.len utf8.offset" + " coroutine.create coroutine.resume coroutine.status coroutine.wrap coroutine.yield io.close io.flush io.input io.lines io.open io.output io.read io.tmpfile io.type io.write io.stdin io.stdout io.stderr os.clock os.date os.difftime os.execute os.exit os.getenv os.remove os.rename os.setlocale os.time os.tmpname" + " coroutine.isyieldable coroutine.running io.popen module package.loaders package.seeall package.config package.searchers package.searchpath" + " require package.cpath package.loaded package.loadlib package.path package.preload";

        public static void Try(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void CenterWindowOnScreen(Window window)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = window.Width;
            double windowHeight = window.Height;
            window.Left = (screenWidth / 2) - (windowWidth / 2);
            window.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public static bool CheckForIllegalCharacter(string s)
        {
            char[] illegalCharacters = Path.GetInvalidFileNameChars();
            foreach (char c in illegalCharacters)
                if (s.Contains(Convert.ToString(c)))
                    return true;
            return false;
        }

        public static TextEditor CreateNewEditor()
        {

            TextEditor editor = new TextEditor();
            editor.FontFamily = new FontFamily("JetBrains Mono");
            editor.FontSize = 14;
            editor.Background = null;
            editor.Foreground = Brushes.White;
            editor.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            editor.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            editor.Foreground = new SolidColorBrush(Color.FromRgb(220, 220, 220));

            try
            {
                XmlTextReader xml = new XmlTextReader(new StringReader(Settings.Default.DefaultSyntax));
                editor.SyntaxHighlighting = HighlightingLoader.Load(xml, HighlightingManager.Instance);
            }

            catch (Exception)
            {
                File.WriteAllText("./logs/syntax_backup.bak", Settings.Default.DefaultSyntax);
                Settings.Default.DefaultSyntax = Settings.Default.Properties["DefaultSyntax"].DefaultValue as string;
                MessageBox.Show("Error\'d while loading script editor. Editor default XML has been restored. (A backup of your xml file has been generated at logs folder");
            }

            finally
            {
                XmlTextReader xml = new XmlTextReader(new StringReader(Settings.Default.DefaultSyntax));
                editor.SyntaxHighlighting = HighlightingLoader.Load(xml, HighlightingManager.Instance);
            }

            CompletionWindow completionWindow = new CompletionWindow(editor.TextArea);

            void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
            {
                if (e.Text == ".")
                {
                    // Open code completion after the user has pressed dot:
                    completionWindow = new CompletionWindow(editor.TextArea);
                    IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                    data.Add(new MyCompletionData("Item1"));
                    data.Add(new MyCompletionData("Item2"));
                    data.Add(new MyCompletionData("Item3"));
                    completionWindow.Show();
                    completionWindow.Closed += delegate {
                        completionWindow = null;
                    };
                }
            }

            void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
            {
                if (e.Text.Length > 0 && completionWindow != null)
                {
                    if (!char.IsLetterOrDigit(e.Text[0]))
                    {
                        // Whenever a non-letter is typed while the completion window is open,
                        // insert the currently selected element.
                        completionWindow.CompletionList.RequestInsertion(e);
                    }
                }
                // Do not set e.Handled=true.
                // We still want to insert the character that was typed.
            }

            return editor;
        }

        

        public static void LoadListBoxFile(ListBox listBox, string directory, string fileType)
        {
            listBox.Items.Clear();
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            FileInfo[] files = directoryInfo.GetFiles(fileType);
            foreach (FileInfo fileInfo in files)
            {
                ListBoxItem newItem = new ListBoxItem
                {
                    Foreground = Brushes.White,
                    Content = fileInfo.Name,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch
                };
                listBox.Items.Add(newItem);
            }
        }

        public static void LoadListBoxFile(ListBox listBox, string directory, string[] fileTypes)
        {
            listBox.Items.Clear();

            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            foreach (string fileType in fileTypes)
            {
                FileInfo[] files = directoryInfo.GetFiles(fileType);
                foreach (FileInfo fileInfo in files)
                {
                    ListBoxItem newItem = new ListBoxItem
                    {
                        Foreground = Brushes.White,
                        Content = fileInfo.Name,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Stretch
                    };
                    listBox.Items.Add(newItem);
                }
            }
        }

        public static string GetStringFromFile(string searchPattern = "All files (*.*)|*.*")
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Open file",
                Filter = searchPattern,
                FilterIndex = 0,
            };

            if (dialog.ShowDialog() == true)
                return File.ReadAllText(dialog.FileName);
            return "";
        }

        public static void SaveStringToFile(string content, string searchPattern = "All files (*.*)|*.*")
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Save file",
                Filter = searchPattern,
                FilterIndex = 0,
            };

            if (dialog.ShowDialog() == true)
                File.WriteAllText(dialog.FileName, content);
        }
    }

    public class MyCompletionData : ICompletionData
    {


        public MyCompletionData(string text)
        {
            Text = text;
        }

        public System.Windows.Media.ImageSource Image
        {
            get { return null; }
        }

        public string Text { get; private set; }

        // Use this property if you want to show a fancy UIElement in the list.
        public object Content
        {
            get { return this.Text; }
        }

        public object Description
        {
            get { return "Description for " + this.Text; }
        }

        public double Priority { get; }

        public void Complete(TextArea textArea, ISegment completionSegment,
            EventArgs insertionRequestEventArgs)
        {
            textArea.Document.Replace(completionSegment, this.Text);
        }
    }
}
