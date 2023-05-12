using CefSharp;
using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using getLocateMap.Logics;

namespace getLocateMap
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var map_url = $@"{AppDomain.CurrentDomain.BaseDirectory}api.html";
            string strHtml = File.ReadAllText(map_url);
            Debug.WriteLine(strHtml);
            browser.LoadHtml(strHtml, "http://www.team-one.com/");
        }
    }
}

