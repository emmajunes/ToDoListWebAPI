using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using ToDoList.API.Services;

namespace ToDoList.API.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserService _userService;

        public BasicAuthenticationHandler(
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

            if(path == $"/api/User/CreateUser")
            {
                return AuthenticateResult.NoResult();
            }
                

            string userName;
            Guid userId;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');

                userName = decoded[0];
                var password = decoded[1];

                var user = await _userService.Authenticate(userName, password);

                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                userId = user.Id;
            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Invalid credentials!!!!!");
            }


            var claims = new[] { new Claim(ClaimTypes.Name, userName), new Claim("UserId", userId.ToString()) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
