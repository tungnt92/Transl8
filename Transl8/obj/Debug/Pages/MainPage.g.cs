﻿#pragma checksum "C:\Users\tungnt92\Desktop\Transl8\Transl8\Pages\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8301D7EA09C6F027F7ED6EC810916534"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Transl8 {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBox txtToTrans;
        
        internal System.Windows.Controls.Button btnTraslate;
        
        internal System.Windows.Controls.TextBlock lblTranslatedText;
        
        internal System.Windows.Controls.Button btnFrom;
        
        internal System.Windows.Controls.Button btnTo;
        
        internal System.Windows.Controls.Image imgSpeak;
        
        internal System.Windows.Controls.ListBox ListMT;
        
        internal System.Windows.Controls.Primitives.Popup popup;
        
        internal System.Windows.Controls.Button eng;
        
        internal System.Windows.Controls.Button viet;
        
        internal System.Windows.Controls.Button span;
        
        internal System.Windows.Controls.Button china;
        
        internal System.Windows.Controls.Button hindi;
        
        internal System.Windows.Controls.Button russian;
        
        internal System.Windows.Controls.Button french;
        
        internal System.Windows.Controls.Button korean;
        
        internal System.Windows.Controls.Button japanese;
        
        internal System.Windows.Controls.Button german;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton SpeakAppBr;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Transl8;component/Pages/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.txtToTrans = ((System.Windows.Controls.TextBox)(this.FindName("txtToTrans")));
            this.btnTraslate = ((System.Windows.Controls.Button)(this.FindName("btnTraslate")));
            this.lblTranslatedText = ((System.Windows.Controls.TextBlock)(this.FindName("lblTranslatedText")));
            this.btnFrom = ((System.Windows.Controls.Button)(this.FindName("btnFrom")));
            this.btnTo = ((System.Windows.Controls.Button)(this.FindName("btnTo")));
            this.imgSpeak = ((System.Windows.Controls.Image)(this.FindName("imgSpeak")));
            this.ListMT = ((System.Windows.Controls.ListBox)(this.FindName("ListMT")));
            this.popup = ((System.Windows.Controls.Primitives.Popup)(this.FindName("popup")));
            this.eng = ((System.Windows.Controls.Button)(this.FindName("eng")));
            this.viet = ((System.Windows.Controls.Button)(this.FindName("viet")));
            this.span = ((System.Windows.Controls.Button)(this.FindName("span")));
            this.china = ((System.Windows.Controls.Button)(this.FindName("china")));
            this.hindi = ((System.Windows.Controls.Button)(this.FindName("hindi")));
            this.russian = ((System.Windows.Controls.Button)(this.FindName("russian")));
            this.french = ((System.Windows.Controls.Button)(this.FindName("french")));
            this.korean = ((System.Windows.Controls.Button)(this.FindName("korean")));
            this.japanese = ((System.Windows.Controls.Button)(this.FindName("japanese")));
            this.german = ((System.Windows.Controls.Button)(this.FindName("german")));
            this.SpeakAppBr = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("SpeakAppBr")));
        }
    }
}

