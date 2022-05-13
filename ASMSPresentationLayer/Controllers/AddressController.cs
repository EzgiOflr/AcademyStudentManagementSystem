using ASMSBusinessLayer.ContractsBLL;
using ASMSBusinessLayer.EmailService;
using ASMSEntityLayer.IdentityModels;
using ASMSEntityLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASMSPresentationLayer.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IUsersAddressBusinessEngine _userAddress;
        private readonly ICityBusinessEngine _cityEngine;
        public AddressController(UserManager<AppUser> userManager, IEmailSender emailSender, IUsersAddressBusinessEngine userAddress, ICityBusinessEngine cityEngine)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _userAddress = userAddress;
            _cityEngine = cityEngine;
        }

        public IActionResult Index()
        {
            //indexe betulun adresini getir
            var user = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;

            List<UsersAddressVM> usersAddressList = _userAddress.GetAll(user.Id).Data.ToList();
            usersAddressList.ForEach(x =>
            {
                //ilçe ve il çekilecektir
            });
            return View(usersAddressList);
        }
        [HttpGet]
        public IActionResult AddAddress()
        {
            //illeri sayfaya götürsün.
            ViewBag.Cities = _cityEngine.GetAll().Data;
            return View();
        }
        [HttpPost]
        public IActionResult AddAddress(UsersAddressVM model)
        {
            try
            {
                //illeri sayfaya götürsün
                ViewBag.Cities = _cityEngine.GetAll().Data;
                if (!ModelState.IsValid)
                {
                    return View(model);
                    
                }

                var user = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
                model.UserId = user.Id;

                var result = _userAddress.Add(model).IsSuccess; // usersadress aslında engine

                if (result)
                {
                    TempData["AddAddressSuccessMessage"] = "Adresiniz başarıyla eklendi.";
                    return RedirectToAction("Index", "Address");

                }
                else
                {
                    ModelState.AddModelError("", "Beklenmedik bir hata oluştu!");
                    return View(model);
                }
            }
            

            catch (Exception ex)
            {

                //ex loglanabilir
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!");
                return View(model);
            }
        }
    }


}

    