using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLED
{
    public class resultJson
    {
        [JsonProperty("data")]
        public string[][] data;
        [JsonProperty("entity")]
        public string[] entity;

    }
}
