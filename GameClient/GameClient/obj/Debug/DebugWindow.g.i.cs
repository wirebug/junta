﻿#pragma checksum "..\..\DebugWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "51E3378BACCB26BE864007C08B8483AD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GameClient;
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


namespace GameClient {
    
    
    /// <summary>
    /// DebugWindow
    /// </summary>
    public partial class DebugWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button startButton;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PräsidentÄndern;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button KarteHinzu;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button KarteWeg;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button spielerHinzu;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button messageButton;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button KampfWählen;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\DebugWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button versprechenWählen;
        
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
            System.Uri resourceLocater = new System.Uri("/GameClient;component/debugwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DebugWindow.xaml"
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
            this.startButton = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.PräsidentÄndern = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\DebugWindow.xaml"
            this.PräsidentÄndern.Click += new System.Windows.RoutedEventHandler(this.PräsidentÄndern_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.KarteHinzu = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\DebugWindow.xaml"
            this.KarteHinzu.Click += new System.Windows.RoutedEventHandler(this.KarteHinzu_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.KarteWeg = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\DebugWindow.xaml"
            this.KarteWeg.Click += new System.Windows.RoutedEventHandler(this.KarteWeg_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.spielerHinzu = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\DebugWindow.xaml"
            this.spielerHinzu.Click += new System.Windows.RoutedEventHandler(this.spielerHinzu_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.messageButton = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\DebugWindow.xaml"
            this.messageButton.Click += new System.Windows.RoutedEventHandler(this.button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.KampfWählen = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\DebugWindow.xaml"
            this.KampfWählen.Click += new System.Windows.RoutedEventHandler(this.KampfWählen_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.versprechenWählen = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\DebugWindow.xaml"
            this.versprechenWählen.Click += new System.Windows.RoutedEventHandler(this.versprechenWählen_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

