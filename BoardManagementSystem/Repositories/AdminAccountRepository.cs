
using BoardManagementSystem.Dto;

namespace BoardManagementSystem.Repositories
{
    public class AdminAccountRepository
    {


        private static readonly HttpClient client = new HttpClient();



        public async Task<bool> LoginAdmin(object request)
        {
            bool authenticated = false;
            try
            {


                HttpResponseMessage responseOb = await client.PostAsJsonAsync("http://careers.telone.co.zw:1930/ActiveDirectoryLogin/api/v1/ldap/auth", request);

                var responseString = await responseOb.Content.ReadAsStringAsync();

                var responseObject = await responseOb.Content.ReadAsAsync<AdminAuthResponse>();

                authenticated = responseObject.auth;


            }
            catch (Exception ex)
            {
                throw ex;
            }
            return authenticated;
        }




    }
}
