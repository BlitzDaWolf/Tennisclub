using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using TennisClub.DTO.Game;

namespace TennisClub.UI.Api
{
    public class GameResultAPI
    {
        public static async Task<List<GameResultDTO>> GetGameResult()
        {
            string fullUri = $"{ApiHelper.BASEURL}gameresult";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(fullUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<GameResultDTO>>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task CreateGameResult(GameResultCreateDTO gameResult)
        {
            string fullUri = $"{ApiHelper.BASEURL}gameresult/create";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(fullUri, gameResult))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                    else
                    {
                        MessageBox.Show($"Wedstrijdresultaten zijn succesvol ingevoerd!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static async Task UpdateGameResult(GameResultUpdateDTO game)
        {
            string fullUri = $"{ApiHelper.BASEURL}gameresult/update";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(fullUri, game))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                    else
                    {
                        MessageBox.Show("Wedstrijdresultaat is succesvol geüpdate!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
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
