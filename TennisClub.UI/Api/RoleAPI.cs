using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using TennisClub.DTO.Role;

namespace TennisClub.UI.Api
{
    public class RoleAPI
    {
        public static async Task<List<RoleDTO>> GetRoles()
        {
            string fullUri = $"{ApiHelper.BASEURL}role";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(fullUri))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<List<RoleDTO>>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public static async Task CreateRole(RoleCreateDTO role)
        {
            string fullUri = $"{ApiHelper.BASEURL}role/create";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(fullUri, role))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                    else
                    {
                        MessageBox.Show("Rol is succesvol toegevoegd!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public static async Task UpdateRole(RoleUpdateDTO role)
        {
            string fullUri = $"{ApiHelper.BASEURL}role/update";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(fullUri, role))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                    else
                    {
                        MessageBox.Show("Rol is succesvol geüpdate!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
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
