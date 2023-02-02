using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using ToDoList.API.Services;
using System.Net.Http.Headers;
using System.Text;


namespace ToDoList.API.Security
{
    public class AuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public AuthenticationHandler(
            IUserService userService,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock

            ) : base(options, logger, encoder, clock)
        {
            _userService = userService;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var path = Request.Path.ToString();

            if(path == $"/api/User/CreateUser" || path == $"/api/User/Login")
            {
                return AuthenticateResult.NoResult();
            }

            Guid userId;

            try
            {
                userId = Guid.Parse(CurrentRecord.Id["UserId"]);
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Not logged in");
            }

            var claims = new[] { new Claim("UserId", userId.ToString()) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);
        }
    }
}
