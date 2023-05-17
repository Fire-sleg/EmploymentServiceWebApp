// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EmploymentServiceWebApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmploymentServiceWebApp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        public ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(
            ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public string UserRole { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Ім'я користувача")]
            public string Username { get; set; }

            [Phone]
            [Display(Name = "Номер телефону")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Назва компанї")]
            public string CompanyName { get; set; }
            [Display(Name = "Опис компанї")]
            public string Description { get; set; }

            [Display(Name = "Ім'я")]
            public string Name { get; set; }

            [Display(Name = "По-батькові")]
            public string MiddleName { get; set; }

            [Display(Name = "Прізвище")]
            public string SurName { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            //Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            UserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //var userName = await _userManager.GetUserNameAsync(user);

            //if (Input.PhoneNumber != phoneNumber )
            //{





            //    //if (!setPhoneResult.Succeeded || !setUserName.Succeeded || !setUser.Succeeded)
            //    //{
            //    //    StatusMessage = "Неочікувана помилка під час спроби встановити номер телефону.";
            //    //    return RedirectToPage();
            //    //}
            //}

            if (Input.PhoneNumber != null)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            }
            //if (Input.Username != null)
            //{
            //    var setUserName = await _userManager.SetUserNameAsync(user, Input.Username);
            //}

            //IdentityResult setUser = new IdentityResult();
            if (UserRole == "Employer")
            {
                if (Input.CompanyName != null)
                {
                    user.CompanyName = Input.CompanyName;
                }
                if (Input.Description != null)
                {
                    user.Description = Input.Description;
                }
                /*setUser = */
                await _userManager.UpdateAsync(user);
            }
            else if (UserRole == "Employee")
            {
                if (Input.Name != null)
                {
                    user.Name = Input.Name;
                }
                if (Input.MiddleName != null)
                {
                    user.MiddleName = Input.MiddleName;
                }
                if (Input.SurName != null)
                {
                    user.SurName = Input.SurName;
                }
                /*setUser = */
                await _userManager.UpdateAsync(user);
            }
            await _context.SaveChangesAsync();
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Профіль оновлено";
            return RedirectToPage();
        }
    }
}
