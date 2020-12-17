using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using TennisClub.DTO.Member;

namespace TennisClub.Api
{
    public static class MemberAPI
    {
        public static async Task<List<MemberDTO>> GetMembers()
        {
            string fullUri = $"{ApiHelper.BASEURL}member";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(fullUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<MemberDTO>>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public static async Task CreateMember(MemberCreateDTO member)
        {
            string fullUri = $"{ApiHelper.BASEURL}member/create";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(fullUri, member))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                    else
                    {
                        MessageBox.Show("Member is succesvol toegevoegd!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task DeleteMember(int id)
        {
            string fullUri = $"{ApiHelper.BASEURL}member/{id}";
            MessageBoxResult messageBoxResult = MessageBox.Show($"Bent u zeker dat u de member met id: {id} wilt verwijderen?", "Waarschuwing", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                    using (HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(fullUri))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception(response.ReasonPhrase);
                        }
                        else
                        {
                            MessageBox.Show("Member is succesvol verwijderd!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public static async Task UpdateMember(UpdateMemberDTO member)
        {
            string fullUri = $"{ApiHelper.BASEURL}member/update";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(fullUri, member))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                    else
                    {
                        MessageBox.Show("Member is succesvol geüpdate!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
