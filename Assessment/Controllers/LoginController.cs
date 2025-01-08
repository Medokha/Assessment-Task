using Assessment.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Assessment.Helper;
namespace Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {

        HosinOldTestingContext context = new HosinOldTestingContext();

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Dtos.LoginRequest model)
        {
            var maxYearid = await context.DepartmentsYears.MaxAsync(y => y.Id);
            var maxtear = await context.DepartmentsYears.Where(c => c.Id == maxYearid).Select(c => c.Year).FirstOrDefaultAsync();
            globalvar.category = maxtear;

            var flage_checkstd = model.IsStaff;

            if (flage_checkstd)
            {
                try
                {
                    var user = await context.UseresUsers.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user == null)
                    {
                        return NotFound(new { message = "User not found." });
                    }

                    if (string.IsNullOrEmpty(user.Password))  
                    {
                        user.Password = CommonMethods.ConvertToEncrypt(model.Password);
                        user.Role = "std";
                        await context.SaveChangesAsync();

                        if (!Enum.GetNames(typeof(AllowedRoles)).Contains(user.Role))
                        {
                            return BadRequest(new { message = "Invalid role." });
                        }

                        var claims = new List<Claim>
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim("role", user.Role)
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return Ok(new { message = "Login successful", role = user.Role });
                    }
                    else
                    {
                        var oldPassword = CommonMethods.ConvertToDecrypt(user.Password);

                        if (model.Password == null)
                        {
                            return BadRequest(new { message = "Password is required." });
                        }

                        if (!user.IsActive)
                        {
                            return Unauthorized(new { message = "Account is not active." });
                        }

                        if (model.Password == oldPassword)
                        {
                            if (!Enum.GetNames(typeof(AllowedRoles)).Contains(user.Role))
                            {
                                return BadRequest(new { message = "Invalid role." });
                            }

                            var claims = new List<Claim>
                    {
                        new Claim("userId", user.Id.ToString()),
                        new Claim("role", user.Role)
                    };

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            return Ok(new { message = "Login successful", role = user.Role });
                        }
                        else
                        {
                            return Unauthorized(new { message = "Invalid password." });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "An error occurred during login.", error = ex.Message });
                }
            }
            else
            {
                try
                {
                    var user = await context.userpermations.FirstOrDefaultAsync(u => u.Email == model.Email);
                    if (user == null)
                    {
                        return NotFound(new { message = "User not found." });
                    }

                    var oldPassword = CommonMethods.ConvertToDecrypt(user.Password);

                    if (model.Password == null)
                    {
                        return BadRequest(new { message = "Password is required." });
                    }

                    if (!user.IsActive)
                    {
                        return Unauthorized(new { message = "Account is not active." });
                    }

                    if (model.Password == oldPassword)
                    {
                        if (!Enum.GetNames(typeof(AllowedRoles)).Contains(user.Role))
                        {
                            return BadRequest(new { message = "Invalid role." });
                        }

                        var claims = new List<Claim>
                {
                    new Claim("userId", user.Id.ToString()),
                    new Claim("role", user.Role)
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return Ok(new { message = "Login successful", role = user.Role });
                    }
                    else
                    {
                        return Unauthorized(new { message = "Invalid password." });
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "An error occurred during login.", error = ex.Message });
                }
            }
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();
                return Ok(new { message = "Logged out successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during logout.", error = ex.Message });
            }
        }

    }
}
