using CarRentalSystem.Domain.Response;
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
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for FinesDetailWindow.xaml
    /// </summary>
    public partial class FinesDetailWindow : Window
    {
        private BookingHistoryResponse bookingHistory;
        public FinesDetailWindow(BookingHistoryResponse bookingHistoryResponse)
        {
            InitializeComponent();
            bookingHistory = bookingHistoryResponse;
        }

        private void btnBookingHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgData.ItemsSource = bookingHistory.Fines;
        }
    }
}
