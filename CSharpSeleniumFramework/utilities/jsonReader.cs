using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSeleniumFramework.utilities
{
    public class jsonReader
    {
        public string extractData(string tokenName)
        {
            string myJsonString = File.ReadAllText("tests/testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public List<string> extractDataList(string tokenName)
        {
            string myJsonString = File.ReadAllText("tests/testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Values<string>().ToList();
        }
    }
}
