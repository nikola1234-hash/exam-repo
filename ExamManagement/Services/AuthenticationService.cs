using EasyTestMaker.Constants;
using EasyTestMaker.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Printing;

namespace EasyTestMaker.Services
{
    public class AuthenticationService
    {
        private HttpClient client { get;}
        public AuthenticationService()
        {
            client = new HttpClient();

        }


        public async System.Threading.Tasks.Task<bool> Login(string username, string password)
        {

            User user = new User(username, password);
            var response = await client.PostAsJsonAsync<User>($"/api/Authentication", user);
            var json = await response.Content.ReadAsStringAsync();
            var output = JsonConvert.DeserializeObject<User>(json);

            if (response.IsSuccessStatusCode)
            {
                Const.UserId = output.Id;
                Const.Username = output.Username;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
