﻿#pragma checksum "..\..\SyntaxEditor - Copy.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "62C77A562FEC9E8ACE61E6547C0B5C368E41B0B9B3D14D1CCCE58C5046172225"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Apiloit;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Search;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Apiloit {
    
    
    /// <summary>
    /// SyntaxEditor
    /// </summary>
    public partial class SyntaxEditor : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\SyntaxEditor - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border windowBord;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\SyntaxEditor - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border top;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\SyntaxEditor - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minib;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\SyntaxEditor - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button save;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\SyntaxEditor - Copy.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ICSharpCode.AvalonEdit.TextEditor Editor;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Apiloit;component/syntaxeditor%20-%20copy.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\SyntaxEditor - Copy.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\SyntaxEditor - Copy.xaml"
            ((Apiloit.SyntaxEditor)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 9 "..\..\SyntaxEditor - Copy.xaml"
            ((Apiloit.SyntaxEditor)(target)).StateChanged += new System.EventHandler(this.Window_StateChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.windowBord = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.top = ((System.Windows.Controls.Border)(target));
            
            #line 22 "..\..\SyntaxEditor - Copy.xaml"
            this.top.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseDown);
            
            #line default
            #line hidden
            
            #line 22 "..\..\SyntaxEditor - Copy.xaml"
            this.top.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.top_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 24 "..\..\SyntaxEditor - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.minib = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\SyntaxEditor - Copy.xaml"
            this.minib.Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 26 "..\..\SyntaxEditor - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 7:
            this.save = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\SyntaxEditor - Copy.xaml"
            this.save.Click += new System.Windows.RoutedEventHandler(this.Button_Click_3);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 28 "..\..\SyntaxEditor - Copy.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_4);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Editor = ((ICSharpCode.AvalonEdit.TextEditor)(target));
            
            #line 33 "..\..\SyntaxEditor - Copy.xaml"
            this.Editor.TextChanged += new System.EventHandler(this.Editor_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

