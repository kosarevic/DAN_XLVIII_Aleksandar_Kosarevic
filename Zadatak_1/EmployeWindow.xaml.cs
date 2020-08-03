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
using System.Windows.Shapes;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for EmployeWindow.xaml
    /// </summary>
    public partial class EmployeWindow : Window
    {
        EmployeViewModel evm = new EmployeViewModel();

        public EmployeWindow()
        {
            InitializeComponent();
            DataContext = evm;
        }
        //Button click initiates approval of the order.
        private void Approve_Btn(object sender, RoutedEventArgs e)
        {
            evm.Approve();
        }
        //Button click initiates deletation of the order.
        private void Delete_Btn(object sender, RoutedEventArgs e)
        {
            evm.DeleteOrder();
        }
        //Button logs out employe.
        private void Cancel_Btn(object sender, RoutedEventArgs e)
        {
            LoginScreen login = new LoginScreen();
            login.Show();
            this.Close();
        }
    }
}
