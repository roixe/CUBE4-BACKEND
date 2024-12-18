﻿using System;
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

namespace JamaisASec.UserControls
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        public event RoutedEventHandler TextChanged;

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged?.Invoke(this, new RoutedEventArgs());
        }

        public string Text
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }
    }
}
