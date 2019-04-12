// Copyright (c) 2019 Javier Cañon 
// https://www.javiercanon.com 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
using System;
using System.Web;
using System.IO;
using System.Globalization;

namespace SO.Web_Code.Handlers
{
    // http://www.c-sharpcorner.com/uploadfile/37db1d/create-your-first-http-handler-in-Asp-Net-3-5/

    /*
     <add verb="*" path="*.gif" type="SimpleHTTPHanlder.ImageHandler,SimpleHTTPHanlder"/>
     <add verb="*" path="*.jpg" type="SimpleHTTPHanlder.ImageHandler,SimpleHTTPHanlder"/>
     
     */

    /// <summary>
    /// disallow use of images in external hosts that cause more bandwith.
    /// </summary>
    public class ImageHttpHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get
            {
                return true;
            }
        }

        public void ProcessRequest( HttpContext context )
        {

            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            string imageURL = null;
            // Perform a case-insensitive comparison.
            if (request.UrlReferrer != null)
            {
                if (string.Compare( request.Url.Host, request.UrlReferrer.Host, true, CultureInfo.InvariantCulture ) == 0)
                {
                    // The requesting host is correct.
                    // Allow the image (if it exists).
                    imageURL = request.PhysicalPath;
                    if (!File.Exists( imageURL ))
                    {
                        response.Status = "Image Not Found";
                        response.StatusCode = 404;
                    }
                    else
                    {
                    }
                }
            }
            if (imageURL == null)
            {
                // No valid image was allowed.
                // Use the warning image instead.
                // Rather than hard-code this image, you could
                // retrieve it from the web.config file
                // (using the <appSettings> section or a custom
                // section). 
                imageURL = context.Server.MapPath( "~/images/notallowed.gif" );
            }
            // Serve the image
            // Set the content type to the appropriate image type.
            response.ContentType = "image/" + Path.GetExtension( imageURL ).ToLower();
            response.WriteFile( imageURL );


        }

        #endregion
    }
}
