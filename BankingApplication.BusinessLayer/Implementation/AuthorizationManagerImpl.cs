using BankingApplication.BusinessLayer.Contracts;
using BankingApplication.CommonLayer.Models;
using BankingApplication.DataLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingApplication.BusinessLayer.Implementation
{
    public class AuthorizationManagerImpl : IAuthorizationManager
    {
        private readonly IAuthorizationRepository authorizationRepository;

        public AuthorizationManagerImpl(IAuthorizationRepository authorizationRepository)
        {
            this.authorizationRepository = authorizationRepository;
        }

        public Task<TokenResponse> GetUserToken(TokenRequest tokenRequest)
        {
            throw new NotImplementedException();
        }
    }
}
