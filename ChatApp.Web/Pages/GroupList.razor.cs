using ChatApp.Web.Models;
using System.Net.Http.Json;

namespace ChatApp.Web.Pages
{
    public partial class GroupList
    {
        private List<User> _groups = new List<User>();
        protected override async Task OnInitializedAsync()
        {
            //Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", yourAccessToken);
            _groups = await Http.GetFromJsonAsync<List<User>>("https://localhost:7183/api/Users");
        }
    }
}
