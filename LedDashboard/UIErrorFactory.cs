using System;
using System.Collections.Generic;
using System.Text;

namespace FirelightUI
{
    static class UIErrorFactory
    {

        static Dictionary<string, UIError> ErrorDictionary = new Dictionary<string, UIError>()
        {
            {
                "hardware-RazerChromaController",
                new UIError()
                {
                    Id = "hardware-RazerChromaController",
                    Title = "Couldn't initialize the following hardware integration: Razer Chroma — Some lighting effects might not work.",
                    CtaText = "Go to settings",
                    CtaUrl = "/settings",
                    CtaElemId = "chroma",
                    Description = @"This can happen if you don't have Razer Synapse 3 or have an outdated version. Make sure you are running
                                    the latest one. If you are still getting this error, <a href='aaaa'><b>restart the lighting</b></a>.

                                    If you don't have Razer Chroma hardware, you can turn off this hardware integration to stop seeing this error."
                }
            }
        };

        public static UIError FromErrorId(string errId, string detailedTitle)
        {
            UIError error;
            if (ErrorDictionary.ContainsKey(errId))
            {
                error = ErrorDictionary[errId];
            }
            else
            {
                error = new UIError()
                {
                    Id = errId,
                    Title = detailedTitle,
                    CtaText = "Dismiss",
                    CtaUrl = "/dismissError"
                };
            }
                
            error.DetailedTitle = detailedTitle;
            return error;
        }

    }
}
