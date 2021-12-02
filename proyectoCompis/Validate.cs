using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyectoCompis
{
    class Validate
    {

        private string cadenaValidar;
        private List<Token> tokens;
        private StringBuilder entero;
        private StringBuilder cadena;

        private List<char> simbilos = new List<char>()
        {
            '!','"','#','%','&','(',')','*','+',',','-','.','/',':',';','<','=','>','?','[',']','^','_','{','|','}','~','\n','\t','\\','\''
        };
        public Validate(string cadena)
        {
            cadenaValidar = cadena;
        }

        public List<Token> recorrer()
        {
            tokens = new List<Token>();
            List<char> list = new List<char>();
            entero = new StringBuilder();
            cadena = new StringBuilder();
            bool abierto = false;
            for (int i = 0; i < cadenaValidar.Length; i++)
            {
                char item = Convert.ToChar(cadenaValidar.Substring(i, 1));
                switch (item)
                {
                    case ' ':
                        if (abierto)
                        {
                            entero.Append(item);
                        }
                        else if (cadena.Length!=0)
                        {
                            Scanner scanner = new Scanner(cadena.ToString());
                            string[] result =  scanner.GetToken();
                            tokens.Add(new Token(result[0], result[1]));
                            cadena.Clear();
                        }
                        break;
                    case '\n':
                        if (abierto)
                        {
                            entero.Append(item);
                        }
                        else if (cadena.Length != 0)
                        {
                            Scanner scanner = new Scanner(cadena.ToString());
                            string[] result = scanner.GetToken();
                            tokens.Add(new Token(result[0], result[1]));
                            cadena.Clear();
                        }

                        break;
                    case '\r':
                        if (abierto)
                        {
                            entero.Append(item);
                        }
                        else if (cadena.Length != 0)
                        {
                            Scanner scanner = new Scanner(cadena.ToString());
                            string[] result = scanner.GetToken();
                            tokens.Add(new Token(result[0], result[1]));
                            cadena.Clear();
                        }

                        break;
                    case '\t':
                    break;

                    case '\\':
                        char validar45 = Convert.ToChar(cadenaValidar.Substring((i + 1), 1));
                        switch (validar45)
                        {
                            case 'n':
                                entero.Append('\n');
                                break;
                            case '\'':
                                entero.Append('\'');
                                break;
                            case '\\':
                                entero.Append('\\');
                                break;
                            case '\t':
                                entero.Append('\t');
                                break;
                                
                            default:
                                break;
                        }
                        i++;
                        break;
                        
                    case ':':
                        if (abierto)
                        {
                            entero.Append(item);
                        }
                        else
                        {
                            tokens.Add(Token.COLON);
                        }
                        
                        break;
                    
                       
                    case ';':
                        if (abierto)
                        {
                            entero.Append(item);
                        }
                        else if (cadena.Length != 0)
                        {
                            Scanner scanner = new Scanner(cadena.ToString());
                            string[] result = scanner.GetToken();
                            tokens.Add(new Token(result[0], result[1]));
                            cadena.Clear();
                            tokens.Add(Token.SEMICOLON);
                        }
                        else
                        {
                            tokens.Add(Token.SEMICOLON);
                        }
                        
                        break;
                    case '|':
                        if (abierto)
                        {
                            entero.Append(item);
                        }
                        else
                        {
                            tokens.Add(Token.PIPE);
                        }
                        
                        break;
                    case '\'':
                        if (Convert.ToChar(cadenaValidar.Substring((i+1),1)) == ' ' || Convert.ToChar(cadenaValidar.Substring((i + 1), 1)) == ';' )
                        {
                            
                            if (abierto)
                            {
                                abierto = false;
                                entero.Append(item);
                                tokens.Add(new Token("Terminal", entero.ToString()));
                                entero.Clear();
                            }
                            else
                            {
                                abierto = true;
                                entero.Append(item);
                            }
                            
                        }
                        else
                        {
                            abierto = true;
                            entero.Append(item);
                        }
                        break;
    
                    default:

                        if (simbilos.Exists(e => e.Equals(item)) || Char.IsNumber(item) || Char.IsLetter(item))
                        {

                           

                            if (abierto)
                            {
                                if ((simbilos.Exists(e => e.Equals(item)) || Char.IsNumber(item) || Char.IsLetter(item)) && cadenaValidar.Length != (i + 1))
                                    entero.Append(item);
                                else if (item == '\'' || cadenaValidar.Length == (i + 1))
                                {
                                    entero.Append(item);
                                    tokens.Add(new Token("Terminal", entero.ToString()));
                                    entero.Clear();
                                }
                            }

                            else
                            {
                                cadena.Append(item);
                            }

                           
                            

                        }
                        

                        break;
                }
            }


            return tokens;
        }


    }
}
