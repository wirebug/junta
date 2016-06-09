﻿using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameClient
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        IHubProxy proxy;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (s, e) => {
                string url = @"http://localhost:58523/";
                var connection = new HubConnection(url);
                proxy = connection.CreateHubProxy("JuntaHub");

                connection.Start().Wait();
            };
        }
    }
}
