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
using System.Web;

namespace SO.Web_Code.Handlers.SEO
{
    class SitemapHttpHandler : IHttpHandler
    {


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

            // 1. check cache

            // 1.1 response cache file


            // not in cache

            // 2. load template
            // 2.1 combine and replace data

            // 2.2 save to cache


            // 2.3 Serve the txt
            response.ContentType = "text/xml";  // Set the content type to the appropriate txt type.

        }

        #endregion


    }
}
