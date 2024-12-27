using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Request;
using CarRentalSystem.Domain.Response;
using CarRentalSystem.Infrustructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for BookingCustomerWindow.xaml
    /// </summary>
    public partial class BookingCustomerWindow : Window
    {
        private IBookingService bookingService;
        private IVehicleService vehicleService;
        private VehicleResponse currentVehicle;
        private LoginResponse currentUser;

        public BookingCustomerWindow(LoginResponse user)
        {
            InitializeComponent();
            bookingService = new BookingService();
            vehicleService = new VehicleService();
            currentUser = user;
        }

        private async void btnCheckAvailable_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                MessageBox.Show("Please select one vehicle to check available");
                return;
            }
            if (IsNullOrEmpty(dpStartDate.Text) || IsNullOrEmpty(dpEndDate.Text))
            {
                MessageBox.Show("Please choose start date and end date both");
                return;
            }
            DateTime startDate = (DateTime)dpStartDate.SelectedDate;
            DateTime endDate = (DateTime)dpEndDate.SelectedDate;
            var isAvailable = await vehicleService.CheckAvailableVehicle(new CheckAvailableRequest()
            {
                VehicleId = ((VehicleResponse)dgData.SelectedItem).Id,
                DateOfBooking = startDate,
                DateOfReturn = endDate,
            });
            if (isAvailable)
            {
                btnBooking.Visibility = Visibility.Visible;
                MessageBox.Show("It's availble, please click Booking button to continue");
            } else
            {
                MessageBox.Show("It's not availble, please select other time");
                btnBooking.Visibility = Visibility.Hidden;
            }
        }

        private void btnBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingDetailCustomerWindow bookingDetailCustomerWindow = new BookingDetailCustomerWindow(currentVehicle, currentUser, (DateTime)dpStartDate.SelectedDate, (DateTime)dpEndDate.SelectedDate);
            bookingDetailCustomerWindow.Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        private async void LoadHistoryBooking()
        {
            try
            {
                var bookings = await vehicleService.GetAsync(new BaseFilteration()
                {
                    SearchText = txtSearch.Text ?? string.Empty
                });
                dgData.ItemsSource = bookings.Items;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadHistoryBooking();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            LoadHistoryBooking();
        }
        private void dateSelectedChange(object sender, SelectionChangedEventArgs e)
        {
            if (dpStartDate.SelectedDate != null && dpEndDate.SelectedDate != null)
            {
                if (dpStartDate.SelectedDate > dpEndDate.SelectedDate)
                {
                    MessageBox.Show("Start Date cannnot be bigger End Date.");
                    dpStartDate.SelectedDate = null;
                    dpEndDate.SelectedDate = null;
                    return;
                }
                if (dpStartDate.SelectedDate < DateTime.Now)
                {
                    MessageBox.Show("Start Date cannnot be less than now.");
                    dpStartDate.SelectedDate = null;
                    dpEndDate.SelectedDate = null;
                    return;
                }
            }
        }
        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                return;
            }


            if (dgData.SelectedItem is VehicleResponse _selectVehicle)
            {
                txtVehicleID.Text = _selectVehicle.Id.ToString();
                txtVehicleNumber.Text = _selectVehicle.VehicleCode;
                txtVehicleName.Text = _selectVehicle.VehicleName;
                txtNumberOfSeat.Text = _selectVehicle.NumberOfSeats.ToString();
                txtPriceRentalADay.Text = _selectVehicle.ADayRentalPrice.ToString();
                txtReservationFee.Text = _selectVehicle.ReservationFee.ToString();
                txtDepositFee.Text = _selectVehicle.DepositFee.ToString();
                txtVATFee.Text = _selectVehicle.VATFee.ToString();
                currentVehicle = _selectVehicle;
            }
        }
        public static bool IsNullOrEmpty(string value)
        {
            if (value != null)
                return value.Length == 0;
            else
                return true;
        }

        private void btnBookingHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BookingHistoryWindow bookingHistoryWindow = new BookingHistoryWindow(currentUser);
            bookingHistoryWindow.Show();
        }

        private void btnRegisterReturn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
