using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntusTestTaskApp.Shared.Models
{
    public class Window
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string QuantityOfWindows { get; set; }
        public string TotalSubElements { get; set; }

        //[JsonIgnore]
        public List<SubElement> SubElements { get; set; } = new List<SubElement>();

        //[JsonIgnore]
        public int? OrderId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}
