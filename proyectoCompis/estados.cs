using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCompis
{
    class estados
    {
        public Token padre { get; set; }
        public List<Token> produce {get; set;}
    }
}
