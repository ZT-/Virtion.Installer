﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6AED49C0766FFC57F262ECF2AA327BD2"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18408
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace Virtion.Installer.UI {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 152 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border B_Close;
        
        #line default
        #line hidden
        
        
        #line 177 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border B_FilePath;
        
        #line default
        #line hidden
        
        
        #line 189 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TB_Path;
        
        #line default
        #line hidden
        
        
        #line 201 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border B_Browser;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border B_Install;
        
        #line default
        #line hidden
        
        
        #line 242 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CB_Icon;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CB_Menu;
        
        #line default
        #line hidden
        
        
        #line 256 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CB_Agree;
        
        #line default
        #line hidden
        
        
        #line 263 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CB_License;
        
        #line default
        #line hidden
        
        
        #line 270 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar PB_ProgressBar;
        
        #line default
        #line hidden
        
        
        #line 279 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TB_ProgressText;
        
        #line default
        #line hidden
        
        
        #line 288 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border B_Finsh;
        
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
            System.Uri resourceLocater = new System.Uri("/Virtion.Installer.UI;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 11 "..\..\MainWindow.xaml"
            ((Virtion.Installer.UI.MainWindow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            
            #line 12 "..\..\MainWindow.xaml"
            ((Virtion.Installer.UI.MainWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            
            #line 13 "..\..\MainWindow.xaml"
            ((Virtion.Installer.UI.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.B_Close = ((System.Windows.Controls.Border)(target));
            
            #line 156 "..\..\MainWindow.xaml"
            this.B_Close.MouseEnter += new System.Windows.Input.MouseEventHandler(this.B_Close_OnMouseEnter);
            
            #line default
            #line hidden
            
            #line 157 "..\..\MainWindow.xaml"
            this.B_Close.MouseLeave += new System.Windows.Input.MouseEventHandler(this.B_Close_OnMouseLeave);
            
            #line default
            #line hidden
            
            #line 158 "..\..\MainWindow.xaml"
            this.B_Close.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.B_Close_OnMouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.B_FilePath = ((System.Windows.Controls.Border)(target));
            
            #line 186 "..\..\MainWindow.xaml"
            this.B_FilePath.MouseEnter += new System.Windows.Input.MouseEventHandler(this.B_FilePath_OnMouseEnter);
            
            #line default
            #line hidden
            
            #line 187 "..\..\MainWindow.xaml"
            this.B_FilePath.MouseLeave += new System.Windows.Input.MouseEventHandler(this.B_FilePath_OnMouseLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TB_Path = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.B_Browser = ((System.Windows.Controls.Border)(target));
            
            #line 210 "..\..\MainWindow.xaml"
            this.B_Browser.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.B_Browser_MouseDown);
            
            #line default
            #line hidden
            
            #line 211 "..\..\MainWindow.xaml"
            this.B_Browser.MouseEnter += new System.Windows.Input.MouseEventHandler(this.B_Browser_OnMouseEnter);
            
            #line default
            #line hidden
            
            #line 212 "..\..\MainWindow.xaml"
            this.B_Browser.MouseLeave += new System.Windows.Input.MouseEventHandler(this.B_Browser_OnMouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.B_Install = ((System.Windows.Controls.Border)(target));
            
            #line 229 "..\..\MainWindow.xaml"
            this.B_Install.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.B_Install_MouseDown);
            
            #line default
            #line hidden
            
            #line 230 "..\..\MainWindow.xaml"
            this.B_Install.MouseEnter += new System.Windows.Input.MouseEventHandler(this.B_Install_OnMouseEnter);
            
            #line default
            #line hidden
            
            #line 231 "..\..\MainWindow.xaml"
            this.B_Install.MouseLeave += new System.Windows.Input.MouseEventHandler(this.B_Install_OnMouseLeave);
            
            #line default
            #line hidden
            return;
            case 7:
            this.CB_Icon = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 8:
            this.CB_Menu = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 9:
            this.CB_Agree = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 10:
            this.CB_License = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.PB_ProgressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 12:
            this.TB_ProgressText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.B_Finsh = ((System.Windows.Controls.Border)(target));
            
            #line 293 "..\..\MainWindow.xaml"
            this.B_Finsh.MouseEnter += new System.Windows.Input.MouseEventHandler(this.B_Finsh_OnMouseEnter);
            
            #line default
            #line hidden
            
            #line 294 "..\..\MainWindow.xaml"
            this.B_Finsh.MouseLeave += new System.Windows.Input.MouseEventHandler(this.B_Finsh_OnMouseLeave);
            
            #line default
            #line hidden
            
            #line 295 "..\..\MainWindow.xaml"
            this.B_Finsh.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.B_Finsh_OnMouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

