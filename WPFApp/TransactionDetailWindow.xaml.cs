using CarRentalSystem.Application.Bases;
using CarRentalSystem.Domain.Response;
using CarRentalSystem.Infrustructure;
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
    /// Interaction logic for TransactionDetailWindow.xaml
    /// </summary>
    public partial class TransactionDetailWindow : Window
    {
        private BookingHistoryResponse currentBookingHistory;
        public TransactionDetailWindow(BookingHistoryResponse bookingHistoryResponse)
        {
            InitializeComponent();
            currentBookingHistory = bookingHistoryResponse;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgData.ItemsSource = currentBookingHistory.TransactionHistory;
        }

        private void btnBookingHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
