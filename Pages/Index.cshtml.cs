using Microsoft.AspNetCore.Components;

namespace Final_Project.Pages
{
    public class IndexModel : ComponentBase
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
