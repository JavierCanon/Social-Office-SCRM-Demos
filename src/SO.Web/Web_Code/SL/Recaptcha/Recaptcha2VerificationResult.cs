using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO.SL.Recaptcha
{
    class Recaptcha2VerificationResult
    {

        #region Properties

        /// <summary>
        /// Determines if the reCAPTCHA verification was successful.
        /// </summary>
        [JsonProperty( "success" )]
        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// Represents an array of errors if reCAPTCHA verification was not successful.
        /// </summary>
        [JsonProperty( "error-codes" )]
        public string[] ErrorCodes
        {
            get;
            set;
        }

        #endregion Properties
    }


}

