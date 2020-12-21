using System;
using System.Collections.Generic;
using System.Globalization;
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
        public async Task<List<MemberDTO>> LoadMemberData()
        {
            var listMembers = await MemberAPI.GetMembers();
            return listMembers;
        }

        //Game
        public async Task<List<GameDTO>> LoadGameData()
        {
            var gamesList = await GameAPI.GetGames();
            GamesList.ItemsSource = from game in gamesList
                                    select new { game.Id, game.GameNumber, LidId = game.Member.Id, game.League.Name, Datum = game.Date.ToString("dd/MM/yyyy") };
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
        public async Task RemoveGameData()
        {
            await GameAPI.DeleteGame(int.Parse(TextBoxDeleteGame.Text));
        }
        public async Task UpdateGameData()
        {
            ComboBoxItem selectedLeague = (ComboBoxItem)ComboBoxLeagueId.SelectedItem;
            DateTime? selectedDate = DatePickerGameDate.SelectedDate;
            string formattedDate = selectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            string leagueName = selectedLeague.Content.ToString();
            int updatedLeague;
            if (leagueName == "Recreatief")
            {
                updatedLeague = 3;
            }
            else if (leagueName == "Competitie")
            {
                updatedLeague = 4;
            }
            else
            {
                updatedLeague = 5;

            }

            GameUpdateDTO game = new GameUpdateDTO
            {
                Id = int.Parse(SelectedGame.Text),
                GameNumber = TextBoxGameNr.Text,
                MemberId = int.Parse(TextBoxMemberId.Text),
                LeagueId = updatedLeague,
                Date = DateTime.Parse(formattedDate)
            };
            await GameAPI.UpdateGame(game);
        }
        public async Task<bool> GivenGameDateExistMemberGameAsync()
        {
            bool hasGameAlready = false;
            DateTime? gameDate = DatePickerGameDate.SelectedDate;
            var games = await LoadGameData();

            if (games.Any(x => x.Member.Id == int.Parse(TextBoxMemberId.Text)))
            {
                var getDateOfGameMember = games.FirstOrDefault(x => x.Member.Id == int.Parse(TextBoxMemberId.Text));
                hasGameAlready = DateTime.Equals(getDateOfGameMember.Date.Date, gameDate.Value.Date);
            }
            return hasGameAlready;
        }
        public async Task<bool> GivenGameDateNotAfterRoleGoneAsync()
        {
            DateTime endDateOfSelectedMember = new DateTime();
            bool hasGameAlready = false;
            var members = await LoadMemberData();
            var member = members.FirstOrDefault(x => x.Id == int.Parse(TextBoxMemberId.Text));


            var memberRole = member.Roles.FirstOrDefault(x => x.Role.Name == "Speler");
            endDateOfSelectedMember = memberRole.EndDate;

            if ((endDateOfSelectedMember.Year != 1) && endDateOfSelectedMember.Date < DatePickerGameDate.SelectedDate.Value.Date)
            {
                hasGameAlready = true;
            }

            return hasGameAlready;
        }

        //GameResult
        public async Task<List<GameResultDTO>> LoadGameResultData()
        {
            var gameResultList = await GameResultAPI.GetGameResult();
            GameResultList.ItemsSource = from gameResult in gameResultList
                                         select new { gameResult.Id, gameId = gameResult.Game.Id, gameResult.SetNr, gameResult.ScoreTeamMember, gameResult.ScoreOpponent };
            GameResultList.Columns[0].Header = "Id";
            GameResultList.Columns[1].Header = "Wedstrijd Id";
            GameResultList.Columns[2].Header = "Set nr.";
            GameResultList.Columns[3].Header = "Score teamlid";
            GameResultList.Columns[4].Header = "Score tegenstander";

            return gameResultList;
        }
        public async Task AddGameResultData()
        {
            var gameResultToCreate = new GameResultCreateDTO()
            {
                GameId = int.Parse(TextBoxGameNrResult.Text),
                SetNr = int.Parse(TextBoxSetNr.Text),
                ScoreTeamMember = int.Parse(TextBoxScoreTeamMember.Text),
                ScoreOpponent = int.Parse(TextBoxScoreOpponent.Text)
            };
            await GameResultAPI.CreateGameResult(gameResultToCreate);
        }
        public async Task UpdateGameResultData()
        {
            GameResultUpdateDTO gameResult = new GameResultUpdateDTO
            {
                Id = int.Parse(SelectedGameResult.Text),
                GameId = int.Parse(TextBoxGameNrResult.Text),
                SetNr = int.Parse(TextBoxSetNr.Text),
                ScoreTeamMember = int.Parse(TextBoxScoreTeamMember.Text),
                ScoreOpponent = int.Parse(TextBoxScoreOpponent.Text)
            };
            await GameResultAPI.UpdateGameResult(gameResult);
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
            TextBoxGameNrResult.Text = "";
            TextBoxSetNr.Text = "";
            TextBoxScoreOpponent.Text = "";
            TextBoxScoreTeamMember.Text = "";
            TextBoxSearchGameResulstsMember.Text = "";
            DatePickerFilterGameResulstsMember.SelectedDate = DateTime.Now;
            DatePickerGameDate.SelectedDate = DateTime.Now;
            DatePickerFilterGameDate.SelectedDate = DateTime.Now;
            ComboBoxLeagueId.SelectedIndex = 0;
        }
        #endregion

        #region WPF Event Handlers
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadGameData();
            await LoadGameResultData();
        }
        private async void refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadGameData();
            ClearTextBoxes();
            SelectedMemberToFilterGameResults.Text = "";
            GameResulstsOfMemberList.ItemsSource = null;
        }
        private void ButtonGamesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            GC.Collect();
        }
        private async void buttonAddGame_Click(object sender, RoutedEventArgs e)
        {
            MemberDTO selectedMember = new MemberDTO();

            var members = await LoadMemberData();
            var memberWithRoles = await LoadMemberRoleData();
            var games = await LoadGameData();

            var checkMemberHasRoleSpeler = members.FirstOrDefault(x => x.Id == int.Parse(TextBoxMemberId.Text));

            if (TextBoxMemberId.Text.Length != 0 && TextBoxGameNr.Text.Length != 0)
            {
                selectedMember = members.FirstOrDefault(x => x.Id == int.Parse(TextBoxMemberId.Text));
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
            else if (!checkMemberHasRoleSpeler.Roles.Any(x => x.Role.Name == "Speler"))
            {
                MessageBox.Show($"Lid met id: {TextBoxMemberId.Text} heeft de rol 'Speler' niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (DateTime.Now.Date > DatePickerGameDate.SelectedDate.Value.Date)
            {
                MessageBox.Show($"Ingegeven wedstrijd datum mag niet kleiner zijn dan datum van vandaag!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private async void ButtonDeleteGame_Click(object sender, RoutedEventArgs e)
        {
            var games = await LoadGameData();

            if (TextBoxDeleteGame.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of veld correct is ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!games.Any(x => x.Id == int.Parse(TextBoxDeleteGame.Text)))
            {
                MessageBox.Show($"Wedstrijd met id: {TextBoxDeleteGame.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await RemoveGameData();
                await LoadGameData();
                ClearTextBoxes();
            }
        }
        private async void ButtonUpdateGame_Click(object sender, RoutedEventArgs e)
        {
            var games = await LoadGameData();
            var members = await LoadMemberData();

            if (SelectedGame.Text.Length == 0)
            {
                MessageBox.Show($"Selecteer eerst een wedstrijd uit de tabel door te dubbelklikken op de wedstrijd die u wenst aan te passen.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!members.Any(x => x.Id == int.Parse(TextBoxMemberId.Text)))
            {
                MessageBox.Show($"Lid met id: {TextBoxMemberId.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await UpdateGameData();
                await LoadGameData();
                ClearTextBoxes();
                SelectedGame.Text = "";
                TextBoxGameNr.IsEnabled = true;
            }
        }
        private void GamesList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic selectedGame = GamesList.SelectedCells[0].Item;

            int selectedGameId = selectedGame.Id;
            SelectedGame.Text = selectedGameId.ToString();
            string gameNumber = selectedGame.GameNumber;
            TextBoxGameNr.Text = gameNumber.ToString();
            int memberId = selectedGame.LidId;
            TextBoxMemberId.Text = memberId.ToString();
            DatePickerGameDate.SelectedDate = DateTime.ParseExact(selectedGame.Datum, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string leagueName = selectedGame.Name;
            if (leagueName == "Recreatief")
            {
                ComboBoxLeagueId.SelectedIndex = 0;
            }
            else if (leagueName == "Competitie")
            {
                ComboBoxLeagueId.SelectedIndex = 1;
            }
            else
            {
                ComboBoxLeagueId.SelectedIndex = 2;
            }
            TextBoxGameNr.IsEnabled = false;
        }
        private async void ButtonFilterGameDate_Click(object sender, RoutedEventArgs e)
        {
            var gamesList = await LoadGameData();
            GamesList.ItemsSource = from game in gamesList
                                    where DateTime.Equals(game.Date.Date, DatePickerFilterGameDate.SelectedDate.Value.Date)
                                    select new { game.Id, game.GameNumber, LidId = game.Member.Id, game.League.Name, Datum = game.Date.ToString("dd/MM/yyyy") };
            GamesList.Columns[0].Header = "Id";
            GamesList.Columns[1].Header = "Wedstrijd nr.";
            GamesList.Columns[2].Header = "Lid Id";
            GamesList.Columns[3].Header = "Competitie";
            GamesList.Columns[4].Header = "Datum";
            ClearTextBoxes();
        }

        //GameResult
        private async void buttonAddGameResult_Click(object sender, RoutedEventArgs e)
        {
            var games = await LoadGameData();
            var gamesWithResults = await LoadGameResultData();

            if (TextBoxGameNrResult.Text.Length == 0 || TextBoxSetNr.Text.Length == 0 || TextBoxScoreTeamMember.Text.Length == 0 || TextBoxScoreOpponent.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!games.Any(x => x.Id == int.Parse(TextBoxGameNrResult.Text)))
            {
                MessageBox.Show($"Wedstrijd met id: {TextBoxGameNrResult.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (gamesWithResults.Any(x => x.Game.Id == int.Parse(TextBoxGameNrResult.Text)))
            {
                MessageBox.Show($"Wedstrijd met id: {TextBoxGameNrResult.Text} heeft al een uitslag gekregen.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (gamesWithResults.Any(x => x.SetNr == int.Parse(TextBoxSetNr.Text)))
            {
                MessageBox.Show($"Set nummer: {TextBoxSetNr.Text} bestaat al!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await AddGameResultData();
                await LoadGameResultData();
                ClearTextBoxes();
            }
        }
        private async void ButtonUpdateGameResult_Click(object sender, RoutedEventArgs e)
        {
            var gameWithResults = await LoadGameResultData();

            if (SelectedGameResult.Text.Length == 0)
            {
                MessageBox.Show($"Selecteer eerst een wedstrijd uit de tabel door te dubbelklikken op de wedstrijd die u wenst aan te passen.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await UpdateGameResultData();
                await LoadGameResultData();
                ClearTextBoxes();
                SelectedGameResult.Text = "";
                TextBoxGameNr.IsEnabled = true;
                TextBoxSetNr.IsEnabled = true;
            }
        }
        private void GameResultList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic selectedGame = GameResultList.SelectedCells[0].Item;
            SelectedGameResult.Text = selectedGame.Id.ToString();
            TextBoxGameNrResult.Text = selectedGame.gameId.ToString();
            TextBoxSetNr.Text = selectedGame.SetNr.ToString();
            TextBoxScoreTeamMember.Text = selectedGame.ScoreTeamMember.ToString();
            TextBoxScoreOpponent.Text = selectedGame.ScoreOpponent.ToString();

            TextBoxGameNrResult.IsEnabled = false;
            TextBoxSetNr.IsEnabled = false;
        }
        private async void ButtonSearchGameResultsMember_Click(object sender, RoutedEventArgs e)
        {
            var members = await LoadMemberData();
            var games = await LoadGameData();
            var gameWithResults = await LoadGameResultData();

            if (TextBoxSearchGameResulstsMember.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!members.Any(x => x.Id == int.Parse(TextBoxSearchGameResulstsMember.Text)))
            {
                MessageBox.Show($"Lid met id: {TextBoxSearchGameResulstsMember.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!gameWithResults.Any(x => x.Game.Member.Id == int.Parse(TextBoxSearchGameResulstsMember.Text)))
            {
                MessageBox.Show($"Lid met id: {TextBoxSearchGameResulstsMember.Text} heeft geen wedstrijden en/of heeft geen wedstrijd uitslagen om op te zoeken!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                GameResulstsOfMemberList.ItemsSource = from gameWithResult in gameWithResults
                                                       where gameWithResult.Game.Member.Id.ToString().Contains(TextBoxSearchGameResulstsMember.Text)
                                                       select new { gameWithResult.Id, gameId = gameWithResult.Game.Id, gameWithResult.SetNr, gameWithResult.ScoreTeamMember, gameWithResult.ScoreOpponent };
                GameResulstsOfMemberList.Columns[0].Header = "Id";
                GameResulstsOfMemberList.Columns[1].Header = "Wedstrijd Id";
                GameResulstsOfMemberList.Columns[2].Header = "Set nr.";
                GameResulstsOfMemberList.Columns[3].Header = "Score teamlid";
                GameResulstsOfMemberList.Columns[4].Header = "Score tegenstander";
                SelectedMemberToFilterGameResults.Text = TextBoxSearchGameResulstsMember.Text;
                ClearTextBoxes();
            }
        }
        private async void ButtonFilterGameResultsMember_Click(object sender, RoutedEventArgs e)
        {
            var gameWithResults = await LoadGameResultData();

            if (!GameResulstsOfMemberList.HasItems)
            {
                MessageBox.Show($"Het tabel waarop u wilt gaan filteren is leeg. Zorg er eerst voor dat u van minstens één wedstrijd van een lid de resultaten ophaalt om daarop te gaan filteren.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var listOfMemberGamesResults = gameWithResults.Where(x => x.Game.Member.Id == int.Parse(SelectedMemberToFilterGameResults.Text)).Select(x => x).ToList();

                if (!listOfMemberGamesResults.Any(x => x.Game.Date.Date == DatePickerFilterGameResulstsMember.SelectedDate.Value.Date))
                {
                    MessageBox.Show($"Er zijn geen resultaten terug gevonden.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    GameResulstsOfMemberList.ItemsSource = from gameWithResult in listOfMemberGamesResults
                                                           where gameWithResult.Game.Date.Date == DatePickerFilterGameResulstsMember.SelectedDate.Value.Date
                                                           select new { gameWithResult.Id, gameId = gameWithResult.Game.Id, gameWithResult.SetNr, gameWithResult.ScoreTeamMember, gameWithResult.ScoreOpponent };
                    GameResulstsOfMemberList.Columns[0].Header = "Id";
                    GameResulstsOfMemberList.Columns[1].Header = "Wedstrijd Id";
                    GameResulstsOfMemberList.Columns[2].Header = "Set nr.";
                    GameResulstsOfMemberList.Columns[3].Header = "Score teamlid";
                    GameResulstsOfMemberList.Columns[4].Header = "Score tegenstander";
                    SelectedMemberToFilterGameResults.Text = TextBoxSearchGameResulstsMember.Text;
                }
            }
        }
        #endregion
    }
}
