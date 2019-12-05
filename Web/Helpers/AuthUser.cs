using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Repository.DAL;
using Web.Controllers;

namespace Web.Helpers
{
    public class AuthUser
    {
        private readonly UserDAO _userDAO;

        public AuthUser(UserDAO userDAO)
        {
            _userDAO = userDAO;
        }

        private void ThrowNotAuthenticated()
        {
            throw new Exception("Usuário não autenticado");
        }

        private ClaimsIdentity GetClaimsIdentity(Controller controller)
        {
            if (controller.User == null || !controller.User.Identity.IsAuthenticated)
            {
                ThrowNotAuthenticated();
            }

            return (ClaimsIdentity)controller.User.Identity;
        }

        private String GetClaimValue(Controller controller, string name, string defaultValue = "")
        {
            var identity = GetClaimsIdentity(controller);

            if (identity == null)
            {
                return defaultValue;
            }

            var claim = identity.FindFirst(name);

            if (claim == null)
            {
                return defaultValue;
            }

            return claim.Value;
        }

        public bool IsAuthenticated(Controller controller)
        {
            return controller.User.Identity.IsAuthenticated;
        }

        public async Task<User> GetUser(Controller controller)
        {
            string id = GetClaimValue(controller, "Id", "0");

            if (id.Equals("0"))
            {
                ThrowNotAuthenticated();
            }

            User user = await _userDAO.FindOrDefault(Convert.ToInt32(id));

            if (user == null)
            {
                ThrowNotAuthenticated();
            }

            return user;
        }

        public bool HasPermission(Controller controller, PermissionType permission)
        {
            string value = GetClaimValue(controller, permission.ToString());

            return !value.Equals("");
        }
    }
}
