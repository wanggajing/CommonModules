using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonModules
{
    public class TimerModule : IHttpModule
    {
        private DateTime startTime;
        public void Init(HttpApplication app)
        {
            app.BeginRequest += HandleEvent;
            app.EndRequest += HandleEvent;
        }
        private void HandleEvent(object src, EventArgs args)
        {
            HttpApplication app = src as HttpApplication;
            switch (app.Context.CurrentNotification)
            {
                case RequestNotification.BeginRequest:
                    startTime = app.Context.Timestamp;
                    break;
                case RequestNotification.EndRequest:
                    double elapsed = DateTime.Now.Subtract(startTime).TotalMilliseconds;
                    System.Diagnostics.Debug.WriteLine(string.Format("Duration: {0} {1}ms", app.Request.RawUrl, elapsed));
                    break;
            }
        }
        public void Dispose()
        {
            // nothing to do
        }
    }
}
