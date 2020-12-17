using System.Windows;
using System.Windows.Controls;
using TennisClub.Api;

namespace TennisClub.UI.Pages
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            ApiHelper.InitializeClient(new ApiCaller());
        }
        private void ButtonMembersPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MembersPage());
        }
        private void ButtonGamesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GamesPage());
        }
        private void ButtonRolesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RolesPage());
        }
        private void ButtonFinesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FinesPage());
        }
    }
}
