using CarRentalSystem.Application.Contracts.Service;
using CarRentalSystem.Application.Request;
using CarRentalSystem.Domain.Entity;
using CarRentalSystem.Infrustructure;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserService userService;
        public MainWindow()
        {
            InitializeComponent();
            userService = new UserService();
        }
        public static bool IsNullOrEmpty(string value)
        {
            if (value != null)
                return value.Length == 0;
            else
                return true;
        }
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsNullOrEmpty(txtUser.Text) || IsNullOrEmpty(txtPass.Password))
            {
                MessageBox.Show("Input user name and password");
                return;
            }
            var res = await userService.Login(new LoginRequest() { Username = txtUser.Text, Password = txtPass.Password });
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var role = res.Data.Role;
                if (role.Equals("CUSTOMER", StringComparison.OrdinalIgnoreCase))
                {
                    BookingCustomerWindow bookingCustomerWindow = new BookingCustomerWindow(res.Data);
                    bookingCustomerWindow.Show();
                }
                if (role.Equals("ADMIN", StringComparison.OrdinalIgnoreCase))
                {
                    BookingAdminWindow bookingAdminWindow = new BookingAdminWindow(res.Data);
                    bookingAdminWindow.Show();
                }
                if (role.Equals("APPRAISER", StringComparison.OrdinalIgnoreCase))
                {
                    ApproveAppraiseWindow approveAppraiseWindow = new ApproveAppraiseWindow(res.Data);
                    approveAppraiseWindow.Show();
                }
            } else
            {
                MessageBox.Show("Can not find your account");
            }

        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Are you sure exit program", "Confirm Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);


            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }
    }
}