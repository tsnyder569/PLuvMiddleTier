using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;

namespace TestWcfService
{
    public class MyServiceAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            // Get the Header Authorization Value
            var reqest = operationContext.RequestContext.RequestMessage.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;
            string authHeader = reqest.Headers[System.Net.HttpRequestHeader.Authorization];
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                authHeader= this.DecodeBase64String(authHeader.Replace("Basic ", "")).Trim();

                // Get Username and Password
                string userName = authHeader.Substring(0, authHeader.IndexOf(":"));
                string password = authHeader.Substring(authHeader.IndexOf(":") + 1, authHeader.Length - userName.Length - 1);

                //validate username and password ...
                if (userName.Equals("test") && password.Equals("testpw"))
                {
                    return true;
                }
                
            }
            
            // If this point is reached, return false to deny access.
            return false;
        }

        private string DecodeBase64String(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
    }
}