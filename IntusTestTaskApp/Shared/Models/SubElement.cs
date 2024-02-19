using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntusTestTaskApp.Shared.Models
{
    public class SubElement
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }

        [JsonIgnore]
        public int WindowId { get; set; }

        [JsonIgnore]
        public Window Window { get; set; }
    }
}
