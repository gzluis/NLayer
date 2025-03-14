using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Users.Queries.GetUserAuthenticated;
using ESFE.BusinessLogic.UseCases.Brands.Queries.GetBrands;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mapster;
using ESFE.BusinessLogic.UseCases.Users.Commands.CreateUser;
using ESFE.BusinessLogic.UseCases.Users.Queries.GetUser;
using ESFE.BusinessLogic.UseCases.Users.Commands.UpdateUser;
using ESFE.BusinessLogic.UseCases.Users.Queries.GetUsers;
using ESFE.BusinessLogic.UseCases.Users.Queries.GetRoles;
using ESFE.Entities;


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

        public async Task<IActionResult> Index()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return View(users);
        }

        // GET: BrandController/Create
        public async Task<IActionResult> Create()
        {
            var rols = await _mediator.Send(new GetRolesQuery());
            ViewData["RolId"] = new SelectList(rols, "RolId", "RolName");
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserRequest createUserRequest)
        {
            try
            {
                var result = await _mediator.Send(new CreateUserCommand(createUserRequest));
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error la intentar guardar la nuevo Usero");
            }
            catch (Exception ex)
            {
                var rols = await _mediator.Send(new GetRolesQuery());
                ViewData["RolId"] = new SelectList(rols, "RolId", "RolName");
                ModelState.AddModelError("", ex.Message);
                return View(createUserRequest);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await _mediator.Send(new GetUserQuery(id));
            var rols = await _mediator.Send(new GetRolesQuery());
            ViewData["RolId"] = new SelectList(rols, "RolId", "RolName", user.RolId);
            return View(user.Adapt(new UpdateUserRequest()));
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserRequest updateUserRequest)
        {
            try
            {
                var result = await _mediator.Send(new UpdateUserCommand(updateUserRequest));
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    throw new Exception("Sucedio un error la intentar editar Usero");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var rols = await _mediator.Send(new GetRolesQuery());
                ViewData["RolId"] = new SelectList(rols, "RolId", "RolName", updateUserRequest.RolId);
                return View(updateUserRequest);
            }
        }
    }
}
