using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TennisClub.Api;
using TennisClub.DTO.Game;
using TennisClub.DTO.Member;
using TennisClub.DTO.Role;
using TennisClub.UI.Api;

namespace TennisClub.UI.Pages
{
    public partial class GamesPage : Page
    {
        public GamesPage()
        {
            InitializeComponent();
        }

        #region Selfmade Methods
        //Member
        public async Task<List<MemberDTO>> LoadData()
        {
            var listMembers = await MemberAPI.GetMembers();
            return listMembers;
        }

        //Game
        public async Task<List<GameDTO>> LoadGameData()
        {
            var gamesList = await GameAPI.GetGames();
            GamesList.ItemsSource = from game in gamesList
                                    select new { game.Id, game.GameNumber, Lid_id = game.Member.Id, game.League.Name, Datum = game.Date.ToString("dd/MM/yyyy") };
            GamesList.Columns[0].Header = "Id";
            GamesList.Columns[1].Header = "Wedstrijd nr.";
            GamesList.Columns[2].Header = "Lid Id";
            GamesList.Columns[3].Header = "Competitie";
            GamesList.Columns[4].Header = "Datum";

            return gamesList;
        }
        public async Task AddGameData()
        {
            int chosenLeague = 0;
            ComboBoxItem selectedLeague = (ComboBoxItem)ComboBoxLeagueId.SelectedItem;
            string formattedSelectedLeague = selectedLeague.Content.ToString();

            if (formattedSelectedLeague == "Recreatief")
            {
                chosenLeague = 3;
            }
            else if (formattedSelectedLeague == "Competitie")
            {
                chosenLeague = 4;
            }
            else
            {
                chosenLeague = 5;
            }

            DateTime? gameDate = DatePickerGameDate.SelectedDate;
            string formattedDate = gameDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var gameToCreate = new GameCreateDTO()
            {
                GameNumber = TextBoxGameNr.Text,
                MemberId = int.Parse(TextBoxMemberId.Text),
                LeagueId = chosenLeague,
                Date = DateTime.Parse(formattedDate)
            };
            await GameAPI.CreateGame(gameToCreate);
        }
        public async Task<bool> GivenGameDateExistMemberGameAsync()
        {
            bool hasGameAlready = false;
            DateTime? gameDate = DatePickerGameDate.SelectedDate;
            var games = await LoadGameData();

            var getDateOfGameMember = games.FirstOrDefault(x => x.Member.Id == int.Parse(TextBoxMemberId.Text));
            hasGameAlready = DateTime.Equals(getDateOfGameMember.Date.Date, gameDate.Value.Date);
            return hasGameAlready;
        }
        public async Task<bool> GivenGameDateNotAfterRoleGoneAsync()
        {
            DateTime endDateOfSelectedMember = new DateTime();
            bool hasGameAlready = false;
            var members = await LoadData();
            var member = members.FirstOrDefault(x => x.Id == int.Parse(TextBoxMemberId.Text));

            var memberRole = member.Roles.FirstOrDefault(x => x.Role.Name == "Speler");
            endDateOfSelectedMember = memberRole.EndDate;

            if ((endDateOfSelectedMember.Year != 1) && endDateOfSelectedMember.Date < DatePickerGameDate.SelectedDate.Value.Date)
            {
                hasGameAlready = true;
            }
            return hasGameAlready;
        }

        //MemberRole
        public async Task<List<MemberRoleDTO>> LoadMemberRoleData()
        {
            var memberRolesList = await MemberRoleAPI.GetMemberRoles();
            return memberRolesList;
        }

        private void ClearTextBoxes()
        {
            TextBoxGameNr.Text = "";
            TextBoxDeleteGame.Text = "";
            TextBoxMemberId.Text = "";
            DatePickerGameDate.SelectedDate = DateTime.Now;
            ComboBoxLeagueId.SelectedIndex = 0;
        }
        #endregion

        #region WPF Event Handlers
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadGameData();
        }
        private async void refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadGameData();
            ClearTextBoxes();
        }
        private void ButtonRolesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            GC.Collect();
        }
        private async void buttonAddGame_Click(object sender, RoutedEventArgs e)
        {
            bool memberIsNotPlayer = false;
            MemberDTO selectedMember = new MemberDTO();

            var members = await LoadData();
            var memberWithRoles = await LoadMemberRoleData();
            var games = await LoadGameData();

            if (TextBoxMemberId.Text.Length != 0 && TextBoxGameNr.Text.Length != 0)
            {
                var member = members.FirstOrDefault(x => x.Id == int.Parse(TextBoxMemberId.Text));
                selectedMember = member;
            }

            if (TextBoxGameNr.Text.Length == 0 || TextBoxMemberId.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (games.Any(x => x.GameNumber == TextBoxGameNr.Text))
            {
                MessageBox.Show($"Wedstrijd met nummer: {TextBoxGameNr.Text} bestaat al!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (selectedMember == null)
            {
                MessageBox.Show($"Lid met id: {TextBoxMemberId.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (memberIsNotPlayer)
            {
                MessageBox.Show($"Lid met id: {TextBoxMemberId.Text} heeft de rol 'Speler' niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (await GivenGameDateExistMemberGameAsync())
            {
                MessageBox.Show($"Lid met id: {TextBoxMemberId.Text} heeft al een wedstrijd ingeplant op {DatePickerGameDate.SelectedDate.Value.ToString("dd/MM/yyyy")}!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (await GivenGameDateNotAfterRoleGoneAsync())
            {
                MessageBox.Show($"Op de datum van de wedstrijd dat u wilt aanmaken is de speler met id: {selectedMember.Id} niet meer gemachtigd om een wedstrijd te spelen omdat zijn/haar rol als 'speler' dan is verlopen!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await AddGameData();
                await LoadGameData();
                ClearTextBoxes();
            }
        }
        #endregion
    }
}
