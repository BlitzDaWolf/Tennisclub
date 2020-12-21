using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TennisClub.Api;
using TennisClub.DTO.Member;

namespace TennisClub
{

    public partial class MembersPage : Page
    {

        public MembersPage()
        {
            InitializeComponent();
        }

        #region Selfmade methods
        public async Task<List<MemberDTO>> LoadData()
        {
            var listMembers = await MemberAPI.GetMembers();
            MemberList.ItemsSource = from member in listMembers
                                     where member.FirstName.ToLower().Contains(filter.Text.ToLower()) || member.LastName.ToLower().Contains(filter.Text.ToLower()) || member.FederationNr.Contains(filter.Text) || member.Id.ToString().Contains(filter.Text.ToString()) || member.Zipcode.ToString().Contains(filter.Text.ToString()) || member.City.ToString().Contains(filter.Text.ToString())
                                     select new { member.Id, member.FederationNr, member.FirstName, member.LastName, birthDate = member.BirthDate.ToString("dd/MM/yyyy"), Gender = member.Gender.Name, member.PhoneNr, member.City, member.Address, member.Number, member.Addition, member.Zipcode };
            MemberList.Columns[0].Header = "Id";
            MemberList.Columns[1].Header = "Federatienr.";
            MemberList.Columns[2].Header = "Voornaam";
            MemberList.Columns[3].Header = "Achternaam";
            MemberList.Columns[4].Header = "Geboortedatum";
            MemberList.Columns[5].Header = "Geslacht";
            MemberList.Columns[6].Header = "Telefoonnr.";
            MemberList.Columns[7].Header = "Stad";
            MemberList.Columns[8].Header = "Straat";
            MemberList.Columns[9].Header = "Huisnr.";
            MemberList.Columns[10].Header = "Extra toevoeging";
            MemberList.Columns[11].Header = "Postcode";
            return listMembers;
        }
        public async Task AddData()
        {
            ComboBoxItem typeItem = (ComboBoxItem)ComboBoxGeslacht.SelectedItem;
            string selectedItem = typeItem.Content.ToString();

            DateTime? selectedDate = DatePickerGeboortedatum.SelectedDate;
            string formattedDate = selectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var memberToCreate = new MemberCreateDTO
            {
                FederationNr = TextBoxFederatieNr.Text,
                FirstName = TextBoxVoornaam.Text,
                LastName = TextBoxAchternaam.Text,
                GenderId = (selectedItem == "Man") ? 1 : 2,
                BirthDate = DateTime.Parse(formattedDate),
                Address = TextBoxAdres.Text,
                Number = TextBoxNummer.Text,
                Addition = TextBoxToevoeging.Text,
                Zipcode = TextBoxPostcode.Text,
                City = TextBoxStad.Text,
                PhoneNr = TextBoxTelefoonnr.Text
            };
            await MemberAPI.CreateMember(memberToCreate);
        }
        public async Task RemoveData()
        {
            await MemberAPI.DeleteMember(int.Parse(TextBoxDeleteMember.Text));
        }
        public async Task UpdateData(int memberId)
        {
            ComboBoxItem typeItem = (ComboBoxItem)ComboBoxGeslacht.SelectedItem;
            string selectedItem = typeItem.Content.ToString();

            DateTime? selectedDate = DatePickerGeboortedatum.SelectedDate;
            string formattedDate = selectedDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            UpdateMemberDTO member = new UpdateMemberDTO
            {
                Id = memberId,
                FederationNr = TextBoxFederatieNr.Text,
                FirstName = TextBoxVoornaam.Text,
                LastName = TextBoxAchternaam.Text,
                GenderId = (selectedItem == "Man") ? 1 : 2,
                BirthDate = DateTime.Parse(formattedDate),
                Address = TextBoxAdres.Text,
                Number = TextBoxNummer.Text,
                Addition = TextBoxToevoeging.Text,
                Zipcode = TextBoxPostcode.Text,
                City = TextBoxStad.Text,
                PhoneNr = TextBoxTelefoonnr.Text
            };
            await MemberAPI.UpdateMember(member);
        }
        private void ClearTextBoxes()
        {
            TextBoxFederatieNr.Text = "";
            TextBoxVoornaam.Text = "";
            TextBoxAchternaam.Text = "";
            ComboBoxGeslacht.SelectedIndex = 0;
            DatePickerGeboortedatum.SelectedDate = DateTime.Now;
            TextBoxAdres.Text = "";
            TextBoxNummer.Text = "";
            TextBoxToevoeging.Text = "";
            TextBoxPostcode.Text = "";
            TextBoxStad.Text = "";
            TextBoxTelefoonnr.Text = "";
            TextBoxDeleteMember.Text = "";
        }
        private bool EmptyBoxes()
        {
            bool foundEmpty = false;

            if (TextBoxFederatieNr.Text.Length == 0 || TextBoxVoornaam.Text.Length == 0 || TextBoxAchternaam.Text.Length == 0 || TextBoxAdres.Text.Length == 0 || TextBoxNummer.Text.Length == 0 || TextBoxPostcode.Text.Length == 0 || TextBoxStad.Text.Length == 0)
            {
                foundEmpty = true;
            }
            return foundEmpty;
        }
        private async Task<bool> AlreadyExistsAsyncFederatieNr(string federatieNr)
        {
            var memberList = await LoadData();

            return memberList.Any(x => x.FederationNr == federatieNr);
        }
        private async Task<bool> IdExists(int id)
        {
            var memberList = await LoadData();

            return memberList.Any(x => x.Id == id);
        }
        #endregion

        #region WPF Event Handlers
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }
        private async void refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadData();
            ClearTextBoxes();
        }
        private async void filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            await LoadData();
        }
        private void MemberList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            dynamic selectedMember = MemberList.SelectedCells[0].Item;

            SelectedMemberId.Content = selectedMember.Id;
            TextBoxFederatieNr.Text = selectedMember.FederationNr;
            TextBoxVoornaam.Text = selectedMember.FirstName;
            TextBoxAchternaam.Text = selectedMember.LastName;
            TextBoxAchternaam.Text = selectedMember.LastName;
            ComboBoxGeslacht.SelectedIndex = (selectedMember.Gender == "Male") ? 0 : 1;
            DatePickerGeboortedatum.SelectedDate = DateTime.ParseExact(selectedMember.birthDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TextBoxAdres.Text = selectedMember.Address;
            TextBoxNummer.Text = selectedMember.Number;
            TextBoxToevoeging.Text = selectedMember.Addition;
            TextBoxPostcode.Text = selectedMember.Zipcode;
            TextBoxStad.Text = selectedMember.City;
            TextBoxTelefoonnr.Text = selectedMember.PhoneNr;
        }
        private async void ButtonAddMember_Click(object sender, RoutedEventArgs e)
        {
            if (EmptyBoxes())
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!await AlreadyExistsAsyncFederatieNr(TextBoxFederatieNr.Text))
            {
                await AddData();
                await LoadData();
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show($"Lid met ingegeven federatienr: {TextBoxFederatieNr.Text} bestaat al.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void ButtonDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxDeleteMember.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of veld correct is ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!await IdExists(int.Parse(TextBoxDeleteMember.Text)))
            {
                MessageBox.Show($"Lid met id: {TextBoxDeleteMember.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await RemoveData();
                await LoadData();
                ClearTextBoxes();
            }
        }
        private async void ButtonUpdateMember_Click(object sender, RoutedEventArgs e)
        {
            var memberList = await LoadData();
            bool memberNotExist = true;
            int memberId = 0;

            foreach (var member in memberList)
            {
                if (member.FederationNr == TextBoxFederatieNr.Text)
                {
                    memberNotExist = false;
                    memberId = member.Id;
                }
            }

            if (EmptyBoxes())
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (memberNotExist)
            {
                MessageBox.Show($"Lid met id: {TextBoxFederatieNr.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await UpdateData(memberId);
                await LoadData();
                ClearTextBoxes();
            }
        }
        private void ButtonRolesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            GC.Collect();
        }
        #endregion
    }
}
