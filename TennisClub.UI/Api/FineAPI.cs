using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using TennisClub.DTO.Fine;

namespace TennisClub.UI.Api
{
    public class FineAPI
    {
        public static async Task<List<FineDTO>> GetFines()
        {
            string fullUri = $"{ApiHelper.BASEURL}fine";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(fullUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<FineDTO>>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task CreateFine(FineCreateDTO fine)
        {
            string fullUri = $"{ApiHelper.BASEURL}fine/create";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(fullUri, fine))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                    else
                    {
                        MessageBox.Show("Boete is succesvol toegevoegd!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static async Task UpdateFine(FineUpdateDTO fine)
        {
            string fullUri = $"{ApiHelper.BASEURL}fine/update";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(fullUri, fine))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                    else
                    {
                        MessageBox.Show("Boete is succesvol geüpdate!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
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
