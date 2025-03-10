using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Users.Queries.GetUserAuthenticated;


namespace ESFE.WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string? pReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewBag.Url = pReturnUrl;
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(GetUserAuthenticatedQuery getUserAuthenticatedQuery, string? pReturnUrl = null)
        {
            try
            {
                var userResponse = await _mediator.Send(getUserAuthenticatedQuery);
                if (userResponse != null && userResponse.UserName == getUserAuthenticatedQuery.userName)
                {
                    //    usuarioAut.Rol = await rolBL.ObtenerPorIdAsync(new RolMantDTO { Id = usuarioAut.IdRol });
                    //    usuarioAut.Token = usuarioAut.Token == null ? "" : usuarioAut.Token;
                    //    var claims = new[] {
                    //    new Claim(ClaimTypes.Name, usuarioAut.Email),
                    //    new Claim("Id", usuarioAut.Id.ToString()),
                    //    new Claim(ClaimTypes.Role, usuarioAut.Rol.Nombre) ,
                    //        new Claim(ClaimTypes.GroupSid, usuarioAut.Token)
                    //    };
                    //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true }); ;
                    //    var result = User.Identity.IsAuthenticated;
                    if (!string.IsNullOrWhiteSpace(pReturnUrl))
                        return Redirect(pReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    throw new Exception("Credenciales incorrectas");
            }
            catch (Exception ex)
            {

                ViewBag.Url = pReturnUrl;
                ViewBag.Error = ex.Message;
               return View(getUserAuthenticatedQuery);
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
