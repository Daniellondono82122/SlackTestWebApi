using Microsoft.AspNetCore.Mvc;

namespace SlackTestWebApi.Controllers
{
    public class SlackController : Controller
    {
        
        public Task<IActionResult> Test()
        {

            //var url = $"{RecognizerApiUri}api/v1/Invoice/{id}/{action}";
            //var headers = new Dictionary<string, string>();
            //headers.Add("Authorization", token);
            //var res = await HttpClientService.PostStringAsync(url, "", "application/json", headers);
            //if (res.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            //{
            //    var validateToken = await ManagementRepository.GetValidToken();
            //    headers["Authorization"] = $"Bearer {validateToken}";
            //    res = await HttpClientService.PostStringAsync(url, "", "application/json", headers);
            //}
            //var content = await res.Content.ReadAsStringAsync();
            //var invoice = await GetInvoiceDetails(id, token, clientId);
            //return (res.IsSuccessStatusCode, content ?? "Bad request - no content", invoice);

        }
    }
}
