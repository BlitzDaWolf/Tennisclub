using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TennisClub.Api;
using TennisClub.DTO.Fine;
using TennisClub.DTO.Member;
using TennisClub.UI.Api;

namespace TennisClub.UI.Pages
{
    public partial class FinesPage : Page
    {
        public FinesPage()
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

        //Fine
        public async Task<List<FineDTO>> LoadFineData()
        {
            var listFines = await FineAPI.GetFines();
            FinesList.ItemsSource = from fine in listFines
                                    select new { fine.Id, fine.FineNumber, Member_id = fine.Member.Id, fine.Amount, Uitreikingsdatum = fine.handOutDate.ToString("dd/MM/yyyy"), Betalingsdatum = (fine.PaymentDate.Year == 1) ? "" : fine.PaymentDate.ToString("dd/MM/yyyy") };
            FinesList.Columns[0].Header = "Id";
            FinesList.Columns[1].Header = "Boete nr.";
            FinesList.Columns[2].Header = "Lid id";
            FinesList.Columns[3].Header = "Bedrag";
            FinesList.Columns[4].Header = "Uitreikingsdatum";
            FinesList.Columns[5].Header = "Betalingsdatum";
            return listFines;
        }
        public async Task AddFineData()
        {
            DateTime? handOutDate = DatePickerHandOverDate.SelectedDate;
            string formattedDate = handOutDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DateTime? payDate = DatePickerPayoutDate.SelectedDate;
            string formattedDate2 = payDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            var fineToCreate = new FineCreateDTO()
            {
                FineNumber = int.Parse(TextBoxFineNumber.Text),
                MemberId = int.Parse(TextBoxMemberId.Text),
                Amount = Math.Round(decimal.Parse(TextBoxAmount.Text.Replace(".", ",")), 2),
                handOutDate = DateTime.Parse(formattedDate),
                PaymentDate = (bool)CheckBoxBetalingsDatumBekend.IsChecked ? DateTime.Parse(formattedDate2) : (DateTime?)null
            };
            await FineAPI.CreateFine(fineToCreate);
        }
        public async Task UpdateFineData()
        {
            var fines = await LoadFineData();
            var selectedFine = fines.SingleOrDefault(x => x.Id == int.Parse(TextBoxFineId.Text));

            DateTime? payDate = DatePickerAfbetalingsDatumToevoegen.SelectedDate;
            string formattedDate2 = payDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            FineUpdateDTO updatedFine = new FineUpdateDTO
            {
                Id = int.Parse(TextBoxFineId.Text),
                FineNumber = selectedFine.FineNumber,
                MemberId = selectedFine.Member.Id,
                Amount = selectedFine.Amount,
                handOutDate = selectedFine.handOutDate,
                PaymentDate = DateTime.Parse(formattedDate2)
            };
            await FineAPI.UpdateFine(updatedFine);
        }
        private void clearTextBoxes()
        {
            TextBoxFineNumber.Text = "";
            TextBoxMemberId.Text = "";
            TextBoxAmount.Text = "";
            DatePickerHandOverDate.SelectedDate = DateTime.Now;
            DatePickerPayoutDate.SelectedDate = DateTime.Now;
            DatePickerAllFilterHandoutDate.SelectedDate = DateTime.Now;
            DatePickerAllFilterPayoutDate.SelectedDate = DateTime.Now;
            DatePickerFilterFineHandoutDate.SelectedDate = DateTime.Now;
            DatePickerFilterFinePayoutDate.SelectedDate = DateTime.Now;
            CheckBoxBetalingsDatumBekend.IsChecked = false;
            TextBoxFineId.Text = "";
            TextBoxSearchFine.Text = "";
            DatePickerAfbetalingsDatumToevoegen.SelectedDate = DateTime.Now;
        }
        #endregion

        #region WPF Event Handlers
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadFineData();
        }
        private void FinesList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dynamic selectedFine = FinesList.SelectedCells[0].Item;
            int selectedParsedFine = selectedFine.Id;
            TextBoxFineId.Text = selectedParsedFine.ToString();
        }
        private void ButtonFinesPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            GC.Collect();
        }
        private async void ButtonAddFine_Click(object sender, RoutedEventArgs e)
        {
            var listOfFines = await LoadFineData();
            var listOfMembers = await LoadMemberData();

            var fineNumberAlreadyExists = listOfFines.Any(x => x.FineNumber == int.Parse(TextBoxFineNumber.Text));
            var memberNotExisting = listOfMembers.Any(x => x.Id == int.Parse(TextBoxMemberId.Text));
            if (TextBoxMemberId.Text.Length == 0 || TextBoxFineNumber.Text.Length == 0 || TextBoxAmount.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if ((bool)CheckBoxBetalingsDatumBekend.IsChecked && DatePickerHandOverDate.SelectedDate > DatePickerPayoutDate.SelectedDate)
            {
                MessageBox.Show($"Ingegeven overhandig datum van de boete mag niet groter zijn dan de uitbetalings datum!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (fineNumberAlreadyExists)
            {
                MessageBox.Show($"Boete met id: {TextBoxFineNumber.Text} bestaat al!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!memberNotExisting)
            {
                MessageBox.Show($"Lid met id: {TextBoxMemberId.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await AddFineData();
                await LoadFineData();
                clearTextBoxes();
            }

        }
        private async void refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadFineData();
            FinesOfMemberList.ItemsSource = new List<FineDTO>();
            InputMemberIdToFilterFines.Text = "";
            clearTextBoxes();
        }
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            _ = ((bool)CheckBoxBetalingsDatumBekend.IsChecked) ? DatePickerPayoutDate.Visibility = Visibility.Visible : DatePickerPayoutDate.Visibility = Visibility.Collapsed;
        }
        private async void ButtonUpdateFine_Click(object sender, RoutedEventArgs e)
        {
            var fines = await LoadFineData();
            var selectedFine = (TextBoxFineId.Text.Length != 0) ? fines.SingleOrDefault(x => x.Id == int.Parse(TextBoxFineId.Text)) : null;
            bool payoutDateFilled = true;

            if (selectedFine != null)
            {
                if (selectedFine.PaymentDate.Year == 1) payoutDateFilled = false;
            }

            if (TextBoxFineId.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (selectedFine == null)
            {
                MessageBox.Show($"Boete met id: {TextBoxFineId.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (payoutDateFilled)
            {
                MessageBox.Show($"Betalings datum van boete met id {TextBoxFineId.Text} is al ingegeven geweest!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (selectedFine.handOutDate.Date > DatePickerAfbetalingsDatumToevoegen.SelectedDate.Value.Date)
            {
                MessageBox.Show($"Ingegeven betalings datum mag niet groter zijn dan de uitreikings datum!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                await UpdateFineData();
                await LoadFineData();
                clearTextBoxes();
            }
        }
        private async void ButtonSearchFine_Click(object sender, RoutedEventArgs e)
        {
            var fines = await LoadFineData();
            var memberExist = (TextBoxSearchFine.Text.Length != 0) ? fines.Any(x => x.Member.Id == int.Parse(TextBoxSearchFine.Text)) : false;
            if (TextBoxSearchFine.Text.Length == 0)
            {
                MessageBox.Show($"Controleer of alle verplichte velden correct zijn ingevuld!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!memberExist)
            {
                MessageBox.Show($"Lid met id: {TextBoxSearchFine.Text} bestaat niet!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                FinesOfMemberList.ItemsSource = from finesOfMember in fines
                                                where finesOfMember.Member.Id.ToString().Contains(TextBoxSearchFine.Text)
                                                select new { finesOfMember.Id, finesOfMember.FineNumber, Member_id = finesOfMember.Member.Id, finesOfMember.Amount, Uitreikingsdatum = finesOfMember.handOutDate.ToString("dd/MM/yyyy"), Betalingsdatum = (finesOfMember.PaymentDate.Year == 1) ? "" : finesOfMember.PaymentDate.ToString("dd/MM/yyyy") };
                FinesOfMemberList.Columns[0].Header = "Id";
                FinesOfMemberList.Columns[1].Header = "Boete nr.";
                FinesOfMemberList.Columns[2].Header = "Lid id";
                FinesOfMemberList.Columns[3].Header = "Bedrag";
                FinesOfMemberList.Columns[4].Header = "Uitreikingsdatum";
                FinesOfMemberList.Columns[5].Header = "Betalingsdatum";
                InputMemberIdToFilterFines.Text = TextBoxSearchFine.Text;
                clearTextBoxes();
            }
        }
        private async void ButtonAllFilterDates_Click(object sender, RoutedEventArgs e)
        {
            var fines = await LoadFineData();
            if (!FinesList.HasItems)
            {
                MessageBox.Show($"Het tabel waarop u wilt gaan filteren is leeg. Zorg er eerst voor dat u van één lid de boetes ophaalt om daarop te gaan filteren.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (DatePickerAllFilterHandoutDate.SelectedDate.Value.Date > DatePickerAllFilterPayoutDate.SelectedDate.Value.Date)
            {
                MessageBox.Show($"Ingegeven betalings datum mag niet groter zijn dan de uitreikings datum!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                FinesList.ItemsSource = from finesOfMember in fines
                                        where DateTime.Equals(finesOfMember.handOutDate.Date, DatePickerAllFilterHandoutDate.SelectedDate.Value.Date) && DateTime.Equals(finesOfMember.PaymentDate.Date, DatePickerAllFilterPayoutDate.SelectedDate.Value.Date)
                                        select new { finesOfMember.Id, finesOfMember.FineNumber, Member_id = finesOfMember.Member.Id, finesOfMember.Amount, Uitreikingsdatum = finesOfMember.handOutDate.ToString("dd/MM/yyyy"), Betalingsdatum = (finesOfMember.PaymentDate.Year == 1) ? "" : finesOfMember.PaymentDate.ToString("dd/MM/yyyy") };
                if (FinesList.Items.Count > 0)
                {
                    FinesList.Columns[0].Header = "Id";
                    FinesList.Columns[1].Header = "Boete nr.";
                    FinesList.Columns[2].Header = "Lid id";
                    FinesList.Columns[3].Header = "Bedrag";
                    FinesList.Columns[4].Header = "Uitreikingsdatum";
                    FinesList.Columns[5].Header = "Betalingsdatum";
                }
                else
                {
                    FinesList.ItemsSource = from finesOfMember in fines
                                            select new { finesOfMember.Id, finesOfMember.FineNumber, Member_id = finesOfMember.Member.Id, finesOfMember.Amount, Uitreikingsdatum = finesOfMember.handOutDate.ToString("dd/MM/yyyy"), Betalingsdatum = (finesOfMember.PaymentDate.Year == 1) ? "" : finesOfMember.PaymentDate.ToString("dd/MM/yyyy") };
                    FinesList.Columns[0].Header = "Id";
                    FinesList.Columns[1].Header = "Boete nr.";
                    FinesList.Columns[2].Header = "Lid id";
                    FinesList.Columns[3].Header = "Bedrag";
                    FinesList.Columns[4].Header = "Uitreikingsdatum";
                    FinesList.Columns[5].Header = "Betalingsdatum";
                    MessageBox.Show($"Er zijn geen resultaten terug gevonden.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private async void ButtonFilterDates_Click(object sender, RoutedEventArgs e)
        {
            var fines = await LoadFineData();

            List<FineDTO> filteredList = new List<FineDTO>();

            if (InputMemberIdToFilterFines.Text.Length > 0)
            {
                var memberToFilter = fines.Where(x => x.Member.Id == int.Parse(InputMemberIdToFilterFines.Text)).Select(x => x);

                foreach (var item in memberToFilter)
                {
                    filteredList.Add(item);
                }

                if (DatePickerFilterFineHandoutDate.SelectedDate.Value.Date > DatePickerFilterFinePayoutDate.SelectedDate.Value.Date)
                {
                    MessageBox.Show($"Ingegeven betalings datum mag niet groter zijn dan de uitreikings datum!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    FinesOfMemberList.ItemsSource = from finesOfMember in filteredList
                                                    where DateTime.Equals(finesOfMember.handOutDate.Date, DatePickerFilterFineHandoutDate.SelectedDate.Value.Date) && DateTime.Equals(finesOfMember.PaymentDate.Date, DatePickerFilterFinePayoutDate.SelectedDate.Value.Date)
                                                    select new { finesOfMember.Id, finesOfMember.FineNumber, Member_id = finesOfMember.Member.Id, finesOfMember.Amount, Uitreikingsdatum = finesOfMember.handOutDate.ToString("dd/MM/yyyy"), Betalingsdatum = (finesOfMember.PaymentDate.Year == 1) ? "" : finesOfMember.PaymentDate.ToString("dd/MM/yyyy") };
                    if (FinesOfMemberList.Items.Count > 0)
                    {
                        FinesOfMemberList.Columns[0].Header = "Id";
                        FinesOfMemberList.Columns[1].Header = "Boete nr.";
                        FinesOfMemberList.Columns[2].Header = "Lid id";
                        FinesOfMemberList.Columns[3].Header = "Bedrag";
                        FinesOfMemberList.Columns[4].Header = "Uitreikingsdatum";
                        FinesOfMemberList.Columns[5].Header = "Betalingsdatum";
                    }
                    else
                    {
                        FinesOfMemberList.ItemsSource = from finesOfMember in fines
                                                        where finesOfMember.Member.Id.ToString().Contains(InputMemberIdToFilterFines.Text)
                                                        select new { finesOfMember.Id, finesOfMember.FineNumber, Member_id = finesOfMember.Member.Id, finesOfMember.Amount, Uitreikingsdatum = finesOfMember.handOutDate.ToString("dd/MM/yyyy"), Betalingsdatum = (finesOfMember.PaymentDate.Year == 1) ? "" : finesOfMember.PaymentDate.ToString("dd/MM/yyyy") };
                        FinesOfMemberList.Columns[0].Header = "Id";
                        FinesOfMemberList.Columns[1].Header = "Boete nr.";
                        FinesOfMemberList.Columns[2].Header = "Lid id";
                        FinesOfMemberList.Columns[3].Header = "Bedrag";
                        FinesOfMemberList.Columns[4].Header = "Uitreikingsdatum";
                        FinesOfMemberList.Columns[5].Header = "Betalingsdatum";
                        MessageBox.Show($"Er zijn geen resultaten terug gevonden.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show($"Het tabel waarop u wilt gaan filteren is leeg. Zorg er eerst voor dat u van één lid de boetes ophaalt om daarop te gaan filteren.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}

