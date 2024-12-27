using CarRentalSystem.Application.Bases;
using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Domain.Enum;
using CarRentalSystem.Domain.Request;
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
    /// Interaction logic for ApproveAppraiseWindow.xaml
    /// </summary>
    public partial class ApproveAppraiseWindow : Window
    {
        private IBookingService bookingService;
        private IFinesService finesService;
        private IAppraiseService appraiseService;
        private LoginResponse currentUser;
        private BookingHistoryResponse bookingHistory;
        public ApproveAppraiseWindow(LoginResponse user)
        {
            InitializeComponent();
            currentUser = user;
            bookingService = new BookingService();
            finesService = new FinesService();
            appraiseService = new AppraiseService();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBookingHistory();
        }
        private async void LoadBookingHistory()
        {
            var bookingHistory = await appraiseService.GetAllRequestReturn(new BaseFilteration()
            {
                SearchText = ""
            });
            dgData.ItemsSource = bookingHistory.Items;
        }

        private void btnAddFines_Click(object sender, RoutedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                MessageBox.Show("Please select item to approve");
                return;
            }
            if (IsNullOrEmpty(txtAmount.Text) || IsNullOrEmpty(txtDescription.Text) || cboFinesReason.SelectedItem == null)
            {
                MessageBox.Show("Please fill all information");
                return;
            }
            try
            {
                finesService.AddFines(new FinesRequest()
                {
                    BookingId = Guid.Parse(txtBookingID.Text),
                    Amount = decimal.Parse(txtAmount.Text),
                    Description = txtDescription.Text,
                    FinesReason = (EFineReason)cboFinesReason.SelectedIndex
                });
                MessageBox.Show("Add fines for booking is successfully.");
            } catch (Exception ex)
            {
                MessageBox.Show("Data is wrong, please check again.");
            }
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public static bool IsNullOrEmpty(string value)
        {
            if (value != null)
                return value.Length == 0;
            else
                return true;
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem == null)
            {
                return;
            }
            if (dgData.SelectedItem is BookingHistoryResponse _selected)
            {
                txtBookingID.Text = _selected.Id.ToString();
                bookingHistory = _selected;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text != null)
            {
                LoadBookingHistory();
            }
        }
    }
}
