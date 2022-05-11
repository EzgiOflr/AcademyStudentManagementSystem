using ASMSBusinessLayer.EmailService;
using ASMSEntityLayer.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASMSBusinessLayer.ContractsBLL;
using ASMSPresentationLayer.Models;
using ASMSEntityLayer.Enums;
using ASMSEntityLayer.ViewModels;
using ASMSEntityLayer.ResultModels;
using ASMSBusinessLayer.ViewModels;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;

namespace ASMSPresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IEmailSender _emailSender;
        private readonly IStudentBusinessEngine _studentBusinessEngine;


        //hepsinin ctor'unu olusturdum.
        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IEmailSender emailSender, IStudentBusinessEngine studentBusinessEngine)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            _studentBusinessEngine = studentBusinessEngine;

        }
        //get'i yorum yaptık cunku register sayfam yok popup uzerinden calısıyorum.
        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    TempData["RegisterFailedMessage"] = "Veri girişlerini istenildiği gibi yapmadınız! Tekrar deneyiniz!";
                    return RedirectToAction("Index", "Home");

                    //return View(model);
                }
                //Aynı emailden tekrar kayıt olunmasın 
                var checkUserForEmail = await _userManager.FindByEmailAsync(model.Email);
                if(checkUserForEmail!= null)
                {
                    TempData["RegisterFailedMessage"] = "Bu Email ile zaten sisteme kayıt yapılmıştır!";
                    return RedirectToAction("Index", "Home");
                    //ModelState.AddModelError("", "Bu Email ile zaten sisteme kayıt yapılmıştır!");
                    //return View(model);
                }
                //user'ı oluşturalım 
                AppUser newUser = new AppUser()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    CreatedDate = DateTime.Now,
                    IsDeleted = false,
                    BirthDate = model.BirthDate.HasValue ? model.BirthDate.Value : null,
                    Gender = model.Gender,
                    EmailConfirmed = true,
                    UserName = model.Email,
                    TcNumber=model.TCNumber
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded) //eklendi
                {
                    //rol ataması
                    var roleResult = await _userManager.AddToRoleAsync(newUser, ASMSRoles.Student.ToString());
                    //student eklensin


                    StudentVM newStudent = new StudentVM()
                    {
                        UserId = newUser.Id,
                        TCNumber = model.TCNumber
                    };

                    IResult resultStudent = _studentBusinessEngine.Add(newStudent);
                    if (resultStudent.IsSuccess == false)
                    {
                        //Admime gizliden bir email gönder (öğrenciyi) eklesin
                    }                   
                    //email gönderilsin
                    var emailToStudent = new EmailMessage()
                    {
                        Subject = "ASMS Sistemine HOŞ GELDİNİZ!" + newUser.Name + " " + newUser.Surname,
                        Body = "Merhaba sisteme kaydınız gerçekleşmiştir...",
                        Contacts = new string[] { model.Email }
                    };
                    await _emailSender.SendMessage(emailToStudent);

                    TempData["RegisterSuccessMessage"] = "Sisteme kaydınız başarıyla gerçekleşti!";
                    return RedirectToAction("Index", "Home", new { email = model.Email });
                }

                else
                {
                    TempData["RegisterFailedMessage"] = "Beklenmedik bir sorun oldu.Üye kaydı başarısız.Tekrar deneyiniz!";
                    return RedirectToAction("Index", "Home");
                    //ModelState.AddModelError("", "Beklenmedik bir sorun oldu.Üye kaydı başarısız. Tekrar deneyiniz!");
                    //return View(model);
                }
            }
            catch (Exception ex )
            {

                //loglanacak;
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Login(string email)
        {
            LoginViewModel model = new LoginViewModel()
            {
                Email = email
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = await _userManager.FindByNameAsync(model.Email);
                //var user = _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "Epostanız ya da şifreniz hatalıdır! Tekrar deneyiniz!");
                    return View();
                }

                //TODO : son parametre bool lockOutOnFailure ile ilgili örnek yapalım
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                //TODO : son parametre bool lockOutOnFailure ile ilgili örnek yapalım

                //if (result.IsLockedOut) kullanıcı kilitli ise süre kısıtlaması koyabiliriz.
                //{
                //    DateTimeOffset d = user.LockoutEnd.Value;
                //}

                if (!result.Succeeded)
                {

                    ModelState.AddModelError("", "Epostanız ya da şifreniz hatalıdır! Tekrar deneyiniz!");
                    return View();
                }
                //Artık hoşgeldi

                if (_userManager.IsInRoleAsync(user, ASMSRoles.Student.ToString()).Result)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (_userManager.IsInRoleAsync(user, ASMSRoles.Coordinator.ToString()).Result)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }

                if (_userManager.IsInRoleAsync(user, ASMSRoles.StudentAdministration.ToString()).Result)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu! Tekrar deneyiniz.");
                //ex loglaması yapılacak
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    ViewBag.ResetPasswordSuccessMessage = "Şifre yenileme talebiniz alındı! Epostanızı kontrol ediniz";
                    return View();

                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var codeEncode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callBackUrl = Url.Action("ConfirmResetPassword", "Account", new { userId = user.Id, code = codeEncode },
                    protocol: Request.Scheme);

                var emailMessage = new EmailMessage()
                {
                    Contacts = new string[] { user.Email },
                    Subject="ASMS- Yeni Şifre Talebi",
                    Body=$"Merhaba {user.Name}{user.Surname},"+
                    $"<br/> Yeni parola belirlemek için"+
                    $"<a href='{HtmlEncoder.Default.Encode(callBackUrl)}'> buraya </a> tıklayınız..."
                    
                };
                await _emailSender.SendMessage(emailMessage);
                ViewBag.ResetPasswordSuccessMessage = "Şifre yenileme talebiniz alındı! Epostanızı kontrol ediniz!";

                     return View();

            }
            catch (Exception ex)
            {
                //ex loglansın
                ViewBag.ResetPasswordFailMessage = "Beklenmedik bir hata oluştu! Tekrar deneyiniz!";
                return View();
            }
        }

        [HttpGet]
        public IActionResult ConfirmResetPassword(string userId,string code)
        {
            if(string.IsNullOrEmpty(userId)|| string.IsNullOrEmpty(code))
            {
                ViewBag.ConfirmResetPasswordFailureMessage = "Beklenmedik bir hata oluştu.";

                return View();
            }

            ResetPasswordViewModel model = new ResetPasswordViewModel()
            {
                UserId = userId,
                Code = code
            };
        
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmResetPassword(ResetPasswordViewModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user==null)
                {
                    ModelState.AddModelError("", "Kullanıcı Bulunamadı!");

                    ////log mesajı yerleştir. hangisinde 286 da mı hata var yoksa 275 de mi? 
                    ///ikisi de exceptionda aynı hatayı verir cünkü
                    ////hangiis hatalı anlayalım diye log yerlestiriyoruz.
                    //throw new Exception(); //catche zıplattık.
                }
                var tokenDecoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Code));
                var result = await _userManager.ResetPasswordAsync(user, tokenDecoded, model.NewPassword);
                if (result.Succeeded)
                {
                    TempData["ConfirmResetPasswordSuccess"] = "Şifrenizi başarıyla güncellenmiştir!";
                    return RedirectToAction("Login", "Account", new { email = user.Email });


                }
                else
                {
                    ModelState.AddModelError("", "Şifrenizin değişme işleminde beklenmedik bir hata oluştu. Tekrar Deneyiniz.");
                    return View(model);
                }

            }
            catch (Exception ex)
            {

                //ex loglanacak
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu, Tekrar deneyiniz!");
                return View(model);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

     
    }
}
