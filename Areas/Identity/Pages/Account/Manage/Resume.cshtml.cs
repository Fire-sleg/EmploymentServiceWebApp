using EmploymentServiceWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmploymentServiceWebApp.Areas.Identity.Pages.Account.Manage
{
    public class ResumeModel : PageModel
    {
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		public ApplicationDbContext _context;

		public ResumeModel(ApplicationDbContext context,
			UserManager<User> userManager,
			SignInManager<User> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public string UserId { get; set; } = string.Empty;
		public byte[] ResumePdf { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public IFormFile PdfFile { get; set; }
            public int check { get; set; }
        }


        public async Task GetIdAsync()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			UserId = user.Id;
		}
		public async Task<byte[]> GetResumeAsync()
		{
			var user = await _userManager.GetUserAsync(HttpContext.User);
			ResumePdf = user.ResumePdf;
			return ResumePdf;
		}
        public async Task<IActionResult> OnPostAsync()
		{

            if (Input.PdfFile != null && Input.PdfFile.Length > 0)
            {

                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    Input.PdfFile.CopyTo(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                var user = await _userManager.GetUserAsync(HttpContext.User);
                user.ResumePdf = fileBytes;
                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();

                return RedirectToPage();
            }
            return Page();
        }
        private async Task LoadAsync(User user)
        {

            Input = new InputModel
            {
                check = 15
            };
        }

        //public async Task<IActionResult> OnGetAsync()
        //{
        //    //var user = await _userManager.GetUserAsync(User);
        //    //if (user == null)
        //    //{
        //    //    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    //}

        //    //await LoadAsync(user);
        //    //return Page();
        //}
        public void OnGet()
        {
        }
    }
}
