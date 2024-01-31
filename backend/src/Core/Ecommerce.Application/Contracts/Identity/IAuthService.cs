using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;

namespace Ecommerce.Application.Contracts.Identity
{
    public interface IAuthService
    {
        string getSessionUser();
        string createToken(User user, IList<string> roles);
    }
}