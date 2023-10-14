using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserSettingsWebApi.Helpers
{
    public class HelperFunctions
    {
        public static string CreateJsonStringForStringValues(Dictionary<string, string> jsonVals)
        {
            var outputJson = "{";
            foreach (var item in jsonVals)
            {
                outputJson += $"\"{item.Key}\":\"{item.Value}\"";

                if (!jsonVals.Last().Equals(item))
                    outputJson += ",";
            }
            outputJson += "}";

            return outputJson;
        }
    }
}
