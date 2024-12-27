using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Response;
using CarRentalSystem.Infrustructure;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for BookingAdminWindow.xaml
    /// </summary>
    public partial class BookingAdminWindow : Window
    {
        private LoginResponse currentUser;
        private IBookingService bookingService;
        private BookingHistoryResponse currentBookingHistory;
        public BookingAdminWindow(LoginResponse user)
        {
            InitializeComponent();
            currentUser = user;
            bookingService = new BookingService();
        }
        public static bool IsNullOrEmpty(string value)
        {
            if (value != null)
                return value.Length == 0;
            else
                return true;
        }
        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BookingCustomerWindow bookingCustomerWindow = new BookingCustomerWindow(currentUser);
            bookingCustomerWindow.Show();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBookingHistoryNeedToApprove();
        }
        private async void LoadBookingHistoryNeedToApprove()
        {
            var bookingHistory = await bookingService.GetBookingNeedToApprove(new BaseFilteration()
            {
                SearchText = txtSearch.Text ?? string.Empty,
            });
            dgData.ItemsSource = bookingHistory.Items;
        }

        private async void btnBookingHistory_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {

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
                LoadBookingHistoryNeedToApprove();
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

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                MessageBox.Show("Please select one booking");
                return;
            }
            if (dgData.SelectedItem is BookingHistoryResponse _selected)
            {
                bookingService.ApproveBooking(_selected.Id);
                MessageBox.Show("Approved booking: " + _selected.BookingNumber);
                LoadBookingHistoryNeedToApprove();
            }
        }
    }
}
