using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FirelightUI
{
    static class GlobalAppState
    {
        public static List<UIError> Errors { get; } = new List<UIError>();

        public static void AddError(string errId, string detailedErrTitle)
        {
            if (!GlobalAppState.Errors.Any(x => x.Id == errId))
            {
                Debug.WriteLine("Received error " + errId + " - " + detailedErrTitle);
                GlobalAppState.Errors.Add(UIErrorFactory.FromErrorId(errId, detailedErrTitle));
            }
        }
    }
}
