using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typesense;

namespace ConsoleApp5.Model
{
    public class typeSenceSchema
    {
        public bool facet { get; set; }
        public string index { get; set; }
        public string name { get; set; }
        public bool optional { get; set; }
        public FieldType type { get; set; }
    }
}
