using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IntusTestTaskApp.Shared.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }

        //[JsonIgnore]
        public List<Window> Windows { get; set; } = new List<Window>();
    }
}
