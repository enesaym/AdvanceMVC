using AdvanceUI.Models.DTO.BusinessUnit;
using AdvanceUI.Models.DTO.Employee;
using AdvanceUI.Models.DTO.Title;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AdvanceUI.ConnectAPI
{
	public class TokenService
	{
		HttpClient _client;

		public TokenService(HttpClient client)
		{
			_client = client;
		}

		public async Task<string> GetToken(EmployeeLoginDTO dto)
		{
			StringContent stringContent = new StringContent(JsonConvert.SerializeObject(dto));
			stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			var response = await _client.PostAsync("Auth/Login", stringContent);

			if (response.IsSuccessStatusCode)
			{
				var token = await response.Content.ReadAsStringAsync();
				return token;
			}

			return "";
		}

		public async Task<bool> Register(EmployeeRegisterDTO dto)
		{
			StringContent stringContent = new StringContent(JsonConvert.SerializeObject(dto));
			stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			var response = await _client.PostAsync("Auth/Register", stringContent);

			if (response.IsSuccessStatusCode)
			{
				return true;
			}

			return false;
		}
        public async Task<List<BusinessUnitSelectDTO>> GetAllUnits()
        {
            var response = await _client.GetAsync("Unit/GetAllUnits");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BusinessUnitSelectDTO>>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }
        public async Task<List<TitleSelectDTO>> GetAllTitles()
        {
            var response = await _client.GetAsync("Title/GetAllTitles");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TitleSelectDTO>>(await response.Content.ReadAsStringAsync());
            }
            return null;
        }



    }
}
