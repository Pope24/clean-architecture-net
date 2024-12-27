using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Service;
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
    /// Interaction logic for BookingHistoryWindow.xaml
    /// </summary>
    public partial class BookingHistoryWindow : Window
    {
        private LoginResponse currentUser;
        private IBookingService bookingService;
        private BookingHistoryResponse currentBookingHistory;
        public BookingHistoryWindow(LoginResponse user)
        {
            InitializeComponent();
            currentUser = user;
            bookingService = new BookingService();
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BookingCustomerWindow bookingCustomerWindow = new BookingCustomerWindow(currentUser);
            bookingCustomerWindow.Show();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBookingHistory();
        }
        private async void LoadBookingHistory()
        {
            var bookingHistory = await bookingService.GetBookingsHistoryByUserIdAsync(currentUser.Id, new BaseFilteration()
            {
                SearchText = txtSearch.Text ?? string.Empty,
            });
            dgData.ItemsSource = bookingHistory.Items;
        }

        private async void btnBookingHistory_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                MessageBox.Show("Please select one booking history");
                return;
            }


            if (dgData.SelectedItem is BookingHistoryResponse _selectBookingResponse)
            {
                var res = await bookingService.GetBookingHistoryAsyncById(_selectBookingResponse.Id);
                TransactionDetailWindow transactionDetailWindow = new TransactionDetailWindow(res);
                transactionDetailWindow.Show();
            }
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != null)
            {
                LoadBookingHistory();
            }
        }
        private async void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                return;
            }


            if (dgData.SelectedItem is BookingHistoryResponse _selectBookingResponse)
            {
                var vehicle = _selectBookingResponse.Vehicle;
                txtBookingNumber.Text = _selectBookingResponse.BookingNumber;
                txtVehicleCode.Text = vehicle?.VehicleCode;
                txtUserName.Text = _selectBookingResponse?.User?.UserName;
                txtTotalPrice.Text = (vehicle?.ADayRentalPrice + vehicle?.AdditionalFeeProperty.VATFee + vehicle?.AdditionalFeeProperty.DepositFee + vehicle?.AdditionalFeeProperty.ReservationFee).ToString();
                dpStartDate.SelectedDate = _selectBookingResponse.StartDate;
                dpEndDate.SelectedDate = _selectBookingResponse.ExpectedReturnDate;
            }
        }

        private void btnRegisterReturn_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                MessageBox.Show("Please select one booking history");
                return;
            }
            if (dgData.SelectedItem is BookingHistoryResponse _selected)
            {
                if (_selected.BookingConfirmDate == null)
                {
                    MessageBox.Show("This booking has not been approved");
                    return;
                }
                if (_selected.RegisterReturnDate != null)
                {
                    MessageBox.Show("This booking has been register return before that");
                    return;
                }
                bookingService.ApproveBooking(_selected.Id);
                MessageBox.Show("Register return successfully");
            }
        }

        private void btnFines_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                MessageBox.Show("Please select one booking history");
                return;
            }


            if (dgData.SelectedItem is BookingHistoryResponse _selectBookingResponse)
            {
                FinesDetailWindow finesDetailWindow = new FinesDetailWindow(_selectBookingResponse);
                finesDetailWindow.Show();
            }
        }
    }
}
