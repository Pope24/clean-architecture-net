using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Enum;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using CarRentalSystem.Infrustructure;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for BookingDetailCustomerWindow.xaml
    /// </summary>
    public partial class BookingDetailCustomerWindow : Window
    {
        private IBookingService bookingService;
        private LoginResponse currentUser;
        private VehicleResponse currentVehicle;
        private DateTime startDate;
        private DateTime endDate;
        public BookingDetailCustomerWindow(VehicleResponse _currentVehicle, LoginResponse user, DateTime _startDate, DateTime _endDate)
        {
            InitializeComponent();
            bookingService = new BookingService();
            currentVehicle = _currentVehicle;
            currentUser = user;
            startDate = _startDate;
            endDate = _endDate;
        }
        public static bool IsNullOrEmpty(string value)
        {
            if (value != null)
                return value.Length == 0;
            else
                return true;
        }

        private async void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (cboReturnMethod.SelectedItem == null)
            {
                MessageBox.Show("You need to select return method");
                return;
            }
            if (((ComboBoxItem)cboReturnMethod.SelectedItem).Tag.ToString() == "1" && IsNullOrEmpty(txtAddressReturn.Text))
            {
                MessageBox.Show("You need to fill address to return in your place");
                return;
            }
            decimal totalAmount;
            if (decimal.TryParse(txtPrice.Text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out totalAmount))
            {
                var res = await bookingService.AddBookingAsync(new BookingRequest()
                {
                    UserId = currentUser.Id,
                    VehicleId = currentVehicle.Id,
                    BookingType = (EBookingType)(cboReturnMethod.SelectedIndex + 1),
                    PaymentMethod = EPaymentMethod.CASH,
                    StartDate = startDate,
                    ExpectedReturnDate = endDate,
                    ReturnAddress = txtAddressReturn.Text,
                    TotalAmount = totalAmount,
                });
                MessageBox.Show("Booking "+ res.Data.BookingEntity.BookingNumber+" successfully");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid price format. Please enter a valid decimal number.");
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dpStartDate.SelectedDate = startDate;
            dpEndDate.SelectedDate = endDate;
            txtVehicleNumber.Text = currentVehicle.VehicleCode;
            txtVehicleName.Text = currentVehicle.VehicleName;
            txtPrice.Text = (currentVehicle.ADayRentalPrice + currentVehicle.VATFee + currentVehicle.DepositFee + currentVehicle.ReservationFee).ToString();
            txtNumberOfSeat.Text = currentVehicle.NumberOfSeats.ToString();
        }

        private void cboReturnMethod_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if ((cboReturnMethod.SelectedIndex + 1) == 1)
            {
                txtAddressReturn.Visibility = Visibility.Visible;
                labelAddressReturn.Visibility = Visibility.Visible;
            } else
            {
                txtAddressReturn.Visibility = Visibility.Hidden;
                labelAddressReturn.Visibility = Visibility.Hidden;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
