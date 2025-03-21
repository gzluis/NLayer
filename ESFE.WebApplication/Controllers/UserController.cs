using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ESFE.BusinessLogic.DTOs;
using ESFE.BusinessLogic.UseCases.Users.Queries.GetUserAuthenticated;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mapster;
using ESFE.BusinessLogic.UseCases.Users.Commands.CreateUser;
using ESFE.BusinessLogic.UseCases.Users.Queries.GetUser;
using ESFE.BusinessLogic.UseCases.Users.Commands.UpdateUser;
using ESFE.BusinessLogic.UseCases.Users.Queries.GetUsers;
using ESFE.BusinessLogic.UseCases.Users.Queries.GetRoles;
using System.Security.Claims;


namespace ESFE.WebApplication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        public async Task<IActionResult> CerrarSesion(string? pReturnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index","Home");
        }
        [AllowAnonymous]
        public IActionResult Login()
        {                    
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(GetUserAuthenticatedQuery getUserAuthenticatedQuery)
        {
            try
            {
                var userResponse = await _mediator.Send(getUserAuthenticatedQuery);
                if (userResponse != null && userResponse.UserNickname == getUserAuthenticatedQuery.userName)
                {                   
                    var claims = new[] {
                        new Claim(ClaimTypes.Name, userResponse.UserName),
                        new Claim("Id", userResponse.UserId.ToString())
                        };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true }); ;                  
                    return RedirectToAction("Index", "Home");
                }
                else
                    throw new Exception("Credenciales incorrectas");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
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
                    throw new Exception("Sucedio un error la intentar guardar la nuevo usuario");
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
                    throw new Exception("Sucedio un error al intentar editar usuario");
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
