using System;
using System.Web;
using System.Web.Configuration;

namespace ETravel.Web.FakeHttps
{
    /// <summary>
    /// HttpModule to enforce the X-Forwarded-Proto header from SSL Offloaders in ASP.NET
    /// </summary>
    /// <remarks>
    /// It only works on Integrated mode
    /// 
    /// Configuration settings in appSettings:
    /// - FakeHttp.ForceHttps (true/false) If set to true, all connections will be 
    /// faked as secure
    /// </remarks>
    public class HttpModule : IHttpModule
    {
        /// <summary>
        /// Header used to send which protocol is being used: http or https
        /// </summary>
        private const string HeaderName = "X-Forwarded-Proto";

        /// <summary>
        /// Configuration setting to force the module to fake every connection as secure.
        /// </summary>
        private bool _forceHttps = false;

        public void Init(HttpApplication application)
        {
            Boolean.TryParse(WebConfigurationManager.AppSettings["FakeHttps.ForceHttps"],out _forceHttps);

            application.BeginRequest += ApplicationBeginRequest;
        }

        void ApplicationBeginRequest(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;
            var request = app.Request;
            var forcedProtocol = request.Headers[HeaderName];

            // We use this value to enable the module
            if (!_forceHttps // If force https is set, we should not return.
                && ( String.IsNullOrEmpty(forcedProtocol) 
                    || forcedProtocol.Equals("http") 
                    || !forcedProtocol.Equals("https")))
                return;

            // If we're here the protocol is http
            request.ServerVariables.Set("HTTPS", "on");
        }

        /// <summary>
        /// This method does nothing.
        /// </summary>
        public void Dispose()
        {
            // Nothing to declare :)
        }
    }
}
