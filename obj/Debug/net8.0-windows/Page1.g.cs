﻿#pragma checksum "..\..\..\Page1.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D2F21D9DC42913DDD6CE5A47738820E76F2D6BE2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using project;


namespace project {
    
    
    /// <summary>
    /// Page1
    /// </summary>
    public partial class Page1 : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Page1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer MessageScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Page1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel MessageStackPanel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Page1.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MessageTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/project;component/page1.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Page1.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MessageScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 2:
            this.MessageStackPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.MessageTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\..\Page1.xaml"
            this.MessageTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.MessageTextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 25 "..\..\..\Page1.xaml"
            this.MessageTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.MessageTextBox_LostFocus);
            
            #line default
            #line hidden
            
            #line 26 "..\..\..\Page1.xaml"
            this.MessageTextBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.MessageTextBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 28 "..\..\..\Page1.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SendButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

