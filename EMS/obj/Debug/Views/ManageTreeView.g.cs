﻿#pragma checksum "..\..\..\Views\ManageTreeView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0A6D9A0C3C4BF77007D762692F94BCB5C8AD2CA84DBFE64E0EC013F67D83A0D0"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using EMS.Views;
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


namespace EMS.Views {
    
    
    /// <summary>
    /// ManageTreeView
    /// </summary>
    public partial class ManageTreeView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Views\ManageTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Load;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Views\ManageTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tb_Load;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Views\ManageTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Save;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Views\ManageTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tb_Save;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Views\ManageTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Initialize;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Views\ManageTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Print;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\ManageTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_Next;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Views\ManageTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbOutPut;
        
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
            System.Uri resourceLocater = new System.Uri("/EMS;component/views/managetreeview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\ManageTreeView.xaml"
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
            this.button_Load = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Views\ManageTreeView.xaml"
            this.button_Load.Click += new System.Windows.RoutedEventHandler(this.button_Load_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.tb_Load = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.button_Save = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Views\ManageTreeView.xaml"
            this.button_Save.Click += new System.Windows.RoutedEventHandler(this.button_Save_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tb_Save = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.button_Initialize = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Views\ManageTreeView.xaml"
            this.button_Initialize.Click += new System.Windows.RoutedEventHandler(this.button_Initialize_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.button_Print = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Views\ManageTreeView.xaml"
            this.button_Print.Click += new System.Windows.RoutedEventHandler(this.button_Print_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.button_Next = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Views\ManageTreeView.xaml"
            this.button_Next.Click += new System.Windows.RoutedEventHandler(this.button_Next_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.tbOutPut = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

