using System.Collections.Generic;
using System.Linq;

using TuRutaUN.Entities.Login;

namespace TuRutaUN.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<LoginUser> WithoutPasswords(this IEnumerable<LoginUser> users)
        {
            if(users == null) return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static LoginUser WithoutPassword(this LoginUser user)
        {
            if (user == null) return null;

            user.PasswordHash = null;
            user.PasswordSalt = null;
            return user;
        }
    }
}