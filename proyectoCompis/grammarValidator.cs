using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCompis
{
    class grammarValidator
    {
        string _token = "";
        bool resultado = true;
        int _index = 0;
        public static List<Token> tokens = new List<Token>();

        public grammarValidator(List<Token> Tokens)
        {
            tokens = Tokens;
        }
        public void Principales()
        {
            switch (_token)
            {
                case "NoTerminal":
                    Principal();
                   // Principales();
                    break;
                case " ":
                    Match("");
                    break;
                
                    
            }
        }
        public void Principal()
        {
            switch (_token)
            {
                case "NoTerminal":
                    Match("NoTerminal");
                    Match(":");
                    Agrupado();
                    if (tokens[_index].symbol == ":")
                    {
                        resultado = false;
                    }
                    else if (tokens[_index].symbol != ";")
                    {
                        Agrupado();
                        if (_token == "EOF")
                        {
                            resultado = false;
                        }
                    }
                    else if(tokens[_index].symbol == ";")
                    {
                        Match(";");
                        Principal();
                        if (_token == "EOF")
                        {
                            resultado = false;
                        }
                    }
                    else
                    {
                        resultado = false;
                    }
                    break;
                case " ":
                    Match(" ");
                    break;
            }
        }

        public void Agrupado()
        {
            switch (_token)
            {
                case "NoTerminal":
                    NoTerminal();
                    if (tokens[_index].symbol == "|")
                    {
                        Match("|");
                        Agrupado();
                    }
                    else
                    {
                        TerminalNoTerminal();
                    }
                    break;
                case "Terminal":
                    Terminal();
                    if (tokens[_index].symbol == "|")
                    {
                        Match("|");
                        Agrupado();
                    }
                    else
                    {
                        TerminalNoTerminal();
                    }
                    break;
                case " ":
                    Match(" ");
                    break;
                
                default:
                    break;
            }
        }

        public void TerminalNoTerminal()
        {
            switch (_token)
            {
                case "NoTerminal":
                    NoTerminal();
                    TerminalNoTerminal();
                    break;
                case "Terminal":
                    Terminal();
                    TerminalNoTerminal();
                    break;
                case "|":
                    Match("|");
                    break;
                case " ":
                    Match(" ");
                    break;

                default:
                    break;
            }
        }

        public void Terminal()
        {
            Match("Terminal");
        }

        public void NoTerminal()
        {
            Match("NoTerminal");
        }

        public void Match(string tag)
        {
            if (_token == tag)
            {
                _index++;
                if (_index < tokens.Count)
                {
                    _token = tokens[_index].symbol;
                }
                else
                {
                    _token = "EOF";
                }
            }
            else
            {
                resultado = false;
            }
        }

        public bool Validar()
        {
            _token = tokens[_index].symbol;
            Principales();
            return resultado;
        }
    }
}
