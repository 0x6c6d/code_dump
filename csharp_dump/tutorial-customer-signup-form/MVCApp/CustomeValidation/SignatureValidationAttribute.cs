﻿using System.ComponentModel.DataAnnotations;

namespace MVCApp.CustomeValidation
{
    public class SignatureValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string signatureDataURI = value.ToString();

            string emptySignatureDateURI = "data:image/png;base64,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";

            return signatureDataURI != emptySignatureDateURI;
        }
    }
}