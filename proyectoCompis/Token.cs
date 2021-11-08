using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCompis
{
    class Token
    {
        public static readonly Token PIPE = new Token("|");
        public static readonly Token COLON = new Token(":");
        public static readonly Token SEMICOLON = new Token(";");
        public static readonly Token QUOTE = new Token("'");
        public static readonly Token EOI = new Token("$");

        public string symbol;
        public string param;


        public Token(string symbol, string param = "")
        {
            this.symbol = symbol;
            this.param = param;
        }

        public bool IsAtom() => symbol == "lit" || symbol == "set";

        public override string ToString() => param.Length == 0 ? String.Format("<{0}>", symbol) : String.Format("<{0}: {1}>", symbol, param);


    }
}
