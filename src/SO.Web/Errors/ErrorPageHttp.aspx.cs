using System;
using System.Web;
using Errors;

namespace Errors
{
    public partial class ErrorPageHttp : System.Web.UI.Page
    {
        protected HttpException ex = null;
        protected void Page_Load( object sender, EventArgs e )
        {

            // if System.InvalidOperationException show error:
            // use class AS class for trycast that return null if trycast fail...
            // http://stackoverflow.com/questions/3350770/how-to-convert-trycast-in-c 
            HttpException ex = Server.GetLastError() as HttpException;

            if (ex == null)
                return;

            int httpCode = ex.GetHttpCode();

            // log original error
            // logged in SO.Global.Application_Error():
            // ExceptionUtility.LogException( ex, "HttpErrorPage" );
            // ExceptionUtility.NotifySystemOps( ex );

            exTrace.Text = ex.StackTrace;


            if (Request.IsLocal || SO.Global.Configuration.Development.GetIsEnabledDeveloperMode())
            {

                if (ex.InnerException != null)
                {
                    innerTrace.Text = ex.InnerException.StackTrace;
                    InnerErrorPanel.Visible = Request.IsLocal;
                    innerMessage.Text = string.Format( "HTTP {0}: {1}",
                        httpCode, ex.InnerException.Message );
                }
                exTrace.Visible = Request.IsLocal;

            }


            /*
            if (httpCode >= 400 && httpCode < 500)
            {
                ex = new HttpException
                        ( httpCode, "Error al solicitar el recurso al servidor (" + httpCode + "). El error ha sigo registrado y notificado a los administradores.", ex );
            }
            else
            {
                if (httpCode > 499)
                {
                    ex = new HttpException
                        ( ex.ErrorCode, "Error en el aplicativo web del servidor (" + httpCode + "). El error ha sigo registrado y notificado a los administradores.", ex );
                }
                else
                {
                    ex = new HttpException
                        ( httpCode, "Safe message for unexpected HTTP codes.", ex );
                }
            }

                        exMessage.Text = ex.Message;

            */
            if (httpCode >= 400 && httpCode < 500)
            {
                exMessage.Text = 
                        "Error al solicitar el recurso al servidor (" + httpCode + "). El error ha sigo registrado y notificado a los administradores.";
            }
            else
            {
                if (httpCode > 499)
                {
                    exMessage.Text =
                        "Error en el aplicativo web del servidor (" + httpCode + "). El error ha sigo registrado y notificado a los administradores.";
                }
                else
                {
                    exMessage.Text = "Error no controlado en el aplicativo web del servidor (" + httpCode + "). El error ha sigo registrado y notificado a los administradores.";
                }
            }




            Server.ClearError();
        }
    }
}
