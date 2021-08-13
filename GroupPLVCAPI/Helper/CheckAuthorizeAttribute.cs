using GroupPLVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace GroupPLVCAPI.Helper
{
    public class CheckAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {            
            if (AuthorizeRequest(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
        }
       
        private bool AuthorizeRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                BAL objBAL = new BAL();

                var UserAgent = actionContext.Request.Headers.UserAgent.ToString();
                var TokenKey = actionContext.Request.Headers.Authorization.Parameter.ToString();
                if (TokenKey.ToString() == "")
                {
                    TokenKey = actionContext.Request.Headers.GetValues("X-Token").First();
                }
                //Boolean Obj = SecurityManager.IsTokenValid(TokenKey, UserAgent);

                Boolean Obj = objBAL.ValidateAgentToken(TokenKey);

                return Obj;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //Code to handle unauthorized request
            actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);            
        }
    }
}