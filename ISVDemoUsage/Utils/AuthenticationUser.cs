using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ISVDemoUsage.Utils
{
    public class AuthenticationUser
    {
        public static string GetAccessToken(string organizationId)
        {
            string signedInUserUniqueName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value.Split('#')[ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name).Value.Split('#').Length - 1];

            // Aquire Access Token to call Azure Resource Manager
            ClientCredential credential = new ClientCredential(ConfigurationManager.AppSettings["ida:ClientID"],
                ConfigurationManager.AppSettings["ida:Password"]);
            // initialize AuthenticationContext with the token cache of the currently signed in user, as kept in the app's EF DB
            AuthenticationContext authContext = new AuthenticationContext(
                string.Format(ConfigurationManager.AppSettings["ida:Authority"], organizationId),
                new ADALTokenCache(signedInUserUniqueName));
            AuthenticationResult result =
                authContext.AcquireTokenSilent(
                    ConfigurationManager.AppSettings["ida:AzureResourceManagerIdentifier"], credential,
                    new UserIdentifier(signedInUserUniqueName, UserIdentifierType.RequiredDisplayableId));

            if (result == null)
                return null;

            return result.AccessToken;
        }
    }
}