using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCompis
{
    class Scanner
    {

        private string _regexp = "";
        private int _index = 0;
        private StringBuilder cadena;



        public Scanner(string cadena)
        {
            _regexp = cadena + '@';
            _index = 0;
        }



        public string[] GetToken()
        {
            string[] result = new string[2];
            bool tokenFound = false;
            while (!tokenFound)
            {
                if (_regexp.Length != _index)
                {
                    char peek = _regexp[_index];
                    if (_index == 0)
                    {
                        if (Char.IsNumber(peek))
                        {
                            tokenFound = true;
                            result[0] = (TokenType.Error.ToString()) ;
                            result[1] = ("Error");
                        }
                        else
                        {
                            cadena = new StringBuilder();
                            bool stop = false;
                            while (!stop)
                            {
                                if (Char.IsLetter(peek) || Char.IsNumber(peek) || peek.Equals('-') || peek.Equals('_'))
                                {
                                    cadena.Append(peek);
                                }
                                else
                                {
                                    tokenFound = true;
                                    result[0] = (TokenType.Error.ToString());
                                    result[1] = ("Error");
                                }
                                _index++;
                                peek = _regexp[_index];
                                if (peek.Equals(' ') || peek.Equals('@'))
                                {
                                    tokenFound = true;
                                    stop = true;
                                }
                            }
                            if (!(result[0] == TokenType.Error.ToString()))
                            {



                                result[0] = TokenType.NoTerminal.ToString();
                                result[1] = cadena.ToString();
                            }
                        }
                    }
                }
            }
            return result;
        }

    }
}
