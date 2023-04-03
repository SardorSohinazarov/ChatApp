using System.Net.Http.Json;

namespace ChatApp.Web.Pages
{
    public partial class GroupList
    {
        private List<string> _groups = new List<string>();
        protected override async Task OnInitializedAsync()
        {
            _groups = await Http.GetFromJsonAsync<List<string>>("https://localhost:7183/api/Account/groups");
        }
    }
}
