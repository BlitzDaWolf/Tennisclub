using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TennisClub.Api;
using TennisClub.DTO.Member;
using TennisClub.DTO.Role;
using TennisClub.UI.Api;

namespace TennisClub.UI.Pages
{
    public partial class RolesPage : Page
    {
        public RolesPage()
        {
            InitializeComponent();
        }

        #region Selfmade Methods
        //Members
        public async Task<List<MemberDTO>> LoadMemberData()
        {
            var listMembers = await MemberAPI.GetMembers();
            return listMembers;
        }

        //Roles
        public async Task<List<RoleDTO>> LoadRoleData()
        {
            var listRoles = await RoleAPI.GetRoles();
            RolesList.ItemsSource = from role in listRoles
                                    select new { role.Id, role.Name };
            RolesList.Columns[0].Header = "Id";
            RolesList.Columns[1].Header = "Naam";
            return listRoles;
        }
        public async Task AddRoleData()
        {
            var RoleToCreate = new RoleCreateDTO
            {
                Name = TextBoxRolNaam.Text
            };
            await RoleAPI.CreateRole(RoleToCreate);
        }
        public async Task UpdateData()
        {
            RoleUpdateDTO role = new RoleUpdateDTO
            {
                Id = int.Parse(SelectedRole.Text),
                Name = TextBoxRolNaam.Text
            };
            await RoleAPI.UpdateRole(role);
        }
        private async Task<bool> AlreadyExistsAsyncRoleName(string roleName)
        {
            var roleList = await LoadRoleData();

            return roleList.Any(x => x.Name == roleName);
        }

        //MemberRoles
        public async Task<List<MemberRoleDTO>> LoadMemberRoleData()
        {
            var memberRolesList = await MemberRoleAPI.GetMemberRoles();
            MemberRolesList.ItemsSource = from memberRole in memberRolesList
                                          select new { memberRole.Id, Rol_id = memberRole.Role.Id, Lid_id = memberRole.Member.Id, Start_datum = memberRole.StartDate.ToString("dd/MM/yyyy"), Eind_datum = (memberRole.EndDate.Year == 1) ? "" : memberRole.EndDate.ToString("dd/MM/yyyy") };
            MemberRolesList.Columns[0].Header = "Id";
            MemberRolesList.Columns[1].Header = "Rol Id";
            MemberRolesList.Columns[2].Header = "Lid Id";
            MemberRolesList.Columns[3].Header = "Start datum";
            MemberRolesList.Columns[4].Header = "Eind datum";

            return memberRolesList;
        }
        public async Task AddMemberRoleData()
        {
            DateTime? startDate = DatePickerStartDate.SelectedDate;
            string formattedDate = startDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DateTime? endDate = DatePickerOnCreateEndDate.SelectedDate;
            string formattedDate2 = endDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var memberRoleToCreate = new MemberRoleCreateDTO()
            {
                RoleId = int.Parse(TextBoxRoleId.Text),
                MemberId = int.Parse(TextBoxMemberId.Text),
                StartDate = DateTime.Parse(formattedDate),
                EndDate = (bool)CheckBoxEindButton.IsChecked ? DateTime.Parse(formattedDate2) : (DateTime?)null
            };
            await MemberRoleAPI.CreateMemberRole(memberRoleToCreate);
        }
        public async Task DeleteMemberRoleData()
        {
            var list = await LoadMemberRoleData();
            int roleId = 0, memberId = 0, memberRoleId = 0;
            DateTime startDate = new DateTime();
            foreach (var role in list)
            {
                if (role.Id == int.Parse(TextBoxMemberIdToDelete.Text))
                {
                    memberRoleId = role.Id;
                    memberId = role.Member.Id;
                    roleId = role.Role.Id;
                    startDate = role.StartDate;
                }
            }
            _ = roleId;
            _ = memberId;

            DateTime? endDate = DatePickerOnCreateEndDate.SelectedDate;
            string formattedDate2 = endDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var memberRoleToUpdate = new MemberRoleUpdateDTO()
            {
                Id = memberRoleId,
                RoleId = roleId,
                MemberId = memberId,
                StartDate = startDate,
                EndDate = DateTime.Parse(formattedDate2)
            };
            _ = memberRoleToUpdate;
            await MemberRoleAPI.UpdateMemberRole(memberRoleToUpdate);
        }
        private async Task<bool> AlreadyExistsAsyncMemberId(string memberId)
        {
            bool check = true;
            var memberList = await LoadMemberData();

            if (memberList.Any(x => x.Id == int.Parse(memberId)))
            {
                check = false;
            }
            return check;
        }
        private async Task<bool> AlreadyExistsAsyncRoleId(string roleId)
        {
            bool check = true;
            var roleList = await LoadRoleData();

            if (roleList.Any(x => x.Id == int.Parse(roleId)))
            {
                check = false;
            }
            return check;
        }
        private async Task<bool> CheckIfMemberRoleExistAsync(string memberRoleId)
        {
            bool exists = false;
            var memberRoleList = await LoadMemberData();
            dynamic roles = memberRoleList.SelectMany(x => x.Roles);

            foreach (dynamic role in roles)
            {
                if (role.Id == int.Parse(TextBoxMemberIdToDelete.Text)) exists = true;
            }
            return exists;
        }
        private void ClearTextBoxes()
        {
            TextBoxMemberId.Text = "";
            TextBoxMemberIdToDelete.Text = "";
            TextBoxRoleId.Text = "";
            TextBoxRolNaam.Text = "";
            TextBoxSearchRolesMember.Text = "";
            DatePickerEndDate.SelectedDate = DateTime.Now;
            DatePickerOnCreateEndDate.SelectedDate = DateTime.Now;
            DatePickerStartDate.SelectedDate = DateTime.Now;
        }

        #endregion

        #region WPF Event Handlers
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadRoleData();
            await LoadMemberRoleData();
        }

        //Roles
        private async void ButtonRolToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxRolNaam.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await AddRoleData();
                await LoadRoleData();
                TextBoxRolNaam.Text = "";
            }
        }
        private async void ButtonRolAanpassen_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRole.Text.Length == 0)
            {
                MessageBox.Show($"Selecteer eerst een rol uit de tabel 'Bestaande rollen' door te dubbelklikken op de rol naam.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (TextBoxRolNaam.Text.Length <= 1)
            {
                MessageBox.Show($"De ingegeven rol naam is te kort van lengte.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (await AlreadyExistsAsyncRoleName(TextBoxRolNaam.Text))
            {
                MessageBox.Show($"Rol met de naam: {TextBoxRolNaam.Text} bestaat al!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await UpdateData();
                await LoadRoleData();
                TextBoxRolNaam.Text = "";
                SelectedRole.Text = "";
            }
        }
        private void RollenList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic selectedRole = RolesList.SelectedCells[0].Item;
            int selectedParsedRole = selectedRole.Id;
            SelectedRole.Text = selectedParsedRole.ToString();
            TextBoxRolNaam.Text = selectedRole.Name;
        }
        private void MemberRolesList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic selectedMemberRole = MemberRolesList.SelectedCells[0].Item;
            int selectedParsedRole = selectedMemberRole.Id;
            TextBoxMemberIdToDelete.Text = selectedParsedRole.ToString();
        }

        //MemberRoles
        private async void ButtonMemberRolToevoegen_Click(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = DatePickerStartDate.SelectedDate;
            DateTime? endDate = DatePickerOnCreateEndDate.SelectedDate;
            bool isChecked = (bool)CheckBoxEindButton.IsChecked;

            if (TextBoxRoleId.Text.Length == 0 || TextBoxMemberId.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (isChecked && (startDate > endDate))
            {
                MessageBox.Show($"Ingegeven eind datum mag niet groter zijn dan de start datum!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (await AlreadyExistsAsyncMemberId(TextBoxMemberId.Text) && await AlreadyExistsAsyncRoleId(TextBoxRoleId.Text))
            {
                MessageBox.Show($"Ingegeven lid id: {TextBoxMemberId.Text} & rol id: {TextBoxRoleId.Text} bestaan niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (await AlreadyExistsAsyncMemberId(TextBoxMemberId.Text))
            {
                MessageBox.Show($"Lid met id: {TextBoxMemberId.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (await AlreadyExistsAsyncRoleId(TextBoxRoleId.Text))
            {
                MessageBox.Show($"Rol met id: {TextBoxRoleId.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await AddMemberRoleData();
                await LoadMemberRoleData();
            }
        }
        private async void ButtonMemberRolAfnemen_Click(object sender, RoutedEventArgs e)
        {
            var memberRoleList = await LoadMemberData();
            bool memberNotExist = true;
            DateTime startDate = new DateTime();
            DateTime endDate = new DateTime();

            dynamic roles = memberRoleList.SelectMany(x => x.Roles);
            if (TextBoxMemberIdToDelete.Text.Length != 0)
            {
                foreach (dynamic role in roles)
                {
                    if (role.Id == int.Parse(TextBoxMemberIdToDelete.Text))
                    {
                        startDate = role.StartDate;
                        endDate = (DateTime)DatePickerEndDate.SelectedDate;
                        memberNotExist = false;
                    }
                }
            }

            if (TextBoxMemberIdToDelete.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (memberNotExist)
            {
                MessageBox.Show($"Lid-rol met id: {TextBoxMemberIdToDelete.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (startDate.Date >= endDate.Date)
            {
                MessageBox.Show($"Ingegeven einddatum mag niet groter zijn dan startdatum van ingegeven lid-rol met id: {TextBoxMemberIdToDelete.Text}!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await DeleteMemberRoleData();
                await LoadMemberRoleData();
            }
        }
        private void CheckBoxEindButton_Click(object sender, RoutedEventArgs e)
        {
            _ = ((bool)CheckBoxEindButton.IsChecked) ? DatePickerOnCreateEndDate.Visibility = Visibility.Visible : DatePickerOnCreateEndDate.Visibility = Visibility.Collapsed;
        }
        private async void ButtonSearchRolesMember_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxSearchRolesMember.Text.Length != 0)
            {
                var listRolesOfMember = await LoadMemberRoleData();
                RolesOfMemberList.ItemsSource = from memberRoles in listRolesOfMember
                                                where memberRoles.Member.Id.ToString().Contains(TextBoxSearchRolesMember.Text)
                                                select new { memberRoles.Id, Rol_id = memberRoles.Role.Id, Lid_id = memberRoles.Member.Id, Start_datum = memberRoles.StartDate.ToString("dd/MM/yyyy"), Eind_datum = (memberRoles.EndDate.Year == 1) ? "" : memberRoles.EndDate.ToString("dd/MM/yyyy") };
                RolesOfMemberList.Columns[0].Header = "Id";
                RolesOfMemberList.Columns[1].Header = "Rol Id";
                RolesOfMemberList.Columns[2].Header = "Lid Id";
                RolesOfMemberList.Columns[3].Header = "Start datum";
                RolesOfMemberList.Columns[4].Header = "Eind datum";
                ClearTextBoxes();
            }
            else
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonRolesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            GC.Collect();
        }
        private async void refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadRoleData();
            await LoadMemberRoleData();
            ClearTextBoxes();
        }
        #endregion
    }
}
