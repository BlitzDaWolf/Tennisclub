#pragma checksum "..\..\..\..\Pages\MemberListPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "834337C0C98427E8AD0049A2D84C1FD2765A1E23"
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
using TennisClub;


namespace TennisClub {
    
    
    /// <summary>
    /// MemberListPage
    /// </summary>
    public partial class MemberListPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\..\..\Pages\MemberListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox filter;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\Pages\MemberListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid MemberList;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Pages\MemberListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button refresh;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TennisClub.UI;V1.0.0.0;component/pages/memberlistpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\MemberListPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\Pages\MemberListPage.xaml"
            ((TennisClub.MemberListPage)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.filter = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\..\Pages\MemberListPage.xaml"
            this.filter.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.filter_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MemberList = ((System.Windows.Controls.DataGrid)(target));
            
            #line 40 "..\..\..\..\Pages\MemberListPage.xaml"
            this.MemberList.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.MemberList_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.refresh = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\..\..\Pages\MemberListPage.xaml"
            this.refresh.Click += new System.Windows.RoutedEventHandler(this.refresh_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

