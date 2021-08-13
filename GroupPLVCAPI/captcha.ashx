<%@ WebHandler Language="C#" Class="captcha" %>

using System;
using System.Web;
using System.Drawing;
using System.Web.SessionState;

using System.Web.Http.Cors;

[EnableCors(origins: "*", headers: "*", methods: "*")]
public class captcha : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {        
        context.Response.ContentType = "image/jpeg";
        ADSSAntiBot captcha = new ADSSAntiBot();
        captcha.DrawText(Base64Decode(context.Request.QueryString["SessionID"]));

        //if (context.Session[ ADSSAntiBot.SESSION_CAPTCHA] == null) context.Session.Add(ADSSAntiBot.SESSION_CAPTCHA, str);
        //else
        //{
        //    context.Session[ ADSSAntiBot.SESSION_CAPTCHA] = str;
        //}

        Bitmap bmp = captcha.Result;
        bmp.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        //context.Response.AddHeader("SessionID", Base64Encode(str));
    }

    public static string Base64Encode(string plainText)
    {
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public bool IsReusable
    {
        get
        {
            return true;
        }
    }
}