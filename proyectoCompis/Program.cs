using System;
using System.Collections.Generic;
using System.IO;
using static proyectoCompis.LALRNode;

namespace proyectoCompis
{
    class Program
    {
        public static List<Token> tokens = new List<Token>();
        public static List<string> grammarTokens = new List<string>();
        public static LALRNode grammar = new LALRNode();

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la url del archivo a evaluar");
            string expresion = Console.ReadLine();

            Console.WriteLine(expresion);

            string text = System.IO.File.ReadAllText(@expresion);

            #region Analisis de la gramatica

            Validate validate = new Validate(text);
            tokens = validate.recorrer();
            grammarValidator Gvalidator = new grammarValidator(tokens);
            foreach (var item in tokens)
            {
                Console.WriteLine(item);
            }
            bool resultadovalido = Gvalidator.Validar();
            if (resultadovalido)
            {
                Console.WriteLine("GRAMATICA CORRECTA");
            }
            else
            {
                Console.WriteLine("ERROR EN GRAMATICA");
            }
            bool primero = true;
            bool termina = false;
            List<estados> estadosCol = new List<estados>();
            estados estados;
            List<Token> coleccion = new List<Token>();
            List<Token> coleccion2;
            coleccion = tokens;



            while (!termina)
            {
                estados = new estados();
                coleccion2 = new List<Token>();
                int index = coleccion.FindIndex(x => x == Token.SEMICOLON);
                for (int i = 0; i <= index; i++)
                {
                    if (coleccion[0] != Token.COLON && coleccion[0] != Token.SEMICOLON)
                    {
                        if (coleccion[0] != Token.PIPE)
                        {
                            if (primero)
                            {
                                estados.padre = coleccion[0];

                                primero = false;
                            }
                            else
                            {
                                coleccion2.Add(coleccion[0]);

                            }
                        }
                        else
                        {
                            estados.produce = coleccion2;
                            estadosCol.Add(estados);
                            Token pad = new Token(estados.padre.symbol, estados.padre.param);
                            estados = new estados();
                            coleccion2 = new List<Token>();
                            estados.padre = pad;
                        }

                    }

                    coleccion.RemoveAt(0);
                }
                estados.produce = coleccion2;
                if (estados.produce.Count != 0)
                {
                    estadosCol.Add(estados);
                }

                primero = true;
                if (coleccion.Count == 0)
                {
                    termina = true;
                }
            }

            #endregion

            #region Precedencia

            grammarTokens.Add("S'");

            foreach (var item in estadosCol)
            {
                if (!grammarTokens.Contains(item.padre.param))
                {
                    grammarTokens.Add(item.padre.param);
                }
                foreach (var item2 in item.produce)
                {
                    if (!grammarTokens.Contains(item2.param))
                    {
                        grammarTokens.Add(item2.param);
                    }
                }
            }

            grammar.Tokens = grammarTokens.ToArray();
            List<PrecedenceGroup> listaPrecedencias = new List<PrecedenceGroup>();
            List<int> listaNums = new List<int>();
            List<Production> producesGroup = new List<Production>();
            PrecedenceGroup precedencia = new PrecedenceGroup();
            Production nodo = new Production();
            string actual = "";

            precedencia.Derivation = LALRNode.Derivation.None;
            nodo.Left = 0;
            nodo.Right = new int[] {1};
            precedencia.Productions = new Production[1];
            precedencia.Productions[0] = nodo;

            listaPrecedencias.Add(precedencia);
            precedencia = new PrecedenceGroup();
            listaNums = new List<int>();

            foreach (var item in estadosCol)
            {
                if(item.padre.param == actual)
                {
                    listaNums = new List<int>();
                    nodo = new Production();
                    nodo.Left = grammarTokens.IndexOf(item.padre.param);

                    foreach (var item2 in item.produce)
                    {
                        listaNums.Add(grammarTokens.IndexOf(item2.param));
                    }
                    nodo.Right = listaNums.ToArray();
                    producesGroup.Add(nodo);
                }
                else
                {
                    if(producesGroup.Count != 0)
                    {
                        //agrega la precedencia
                        precedencia.Productions = new Production[producesGroup.Count];
                        precedencia.Productions = producesGroup.ToArray();
                        listaPrecedencias.Add(precedencia);

                        //inicia otra precedencia
                        precedencia = new PrecedenceGroup();
                        listaNums = new List<int>();
                        producesGroup = new List<Production>();
                        nodo = new Production();

                        actual = item.padre.param;
                        precedencia.Derivation = LALRNode.Derivation.LeftMost;
                        nodo.Left = grammarTokens.IndexOf(item.padre.param);

                        foreach (var item2 in item.produce)
                        {
                            listaNums.Add(grammarTokens.IndexOf(item2.param));
                        }
                        nodo.Right = listaNums.ToArray();
                        producesGroup.Add(nodo);
                    }
                    else
                    {
                        //inicia una precedencia
                        precedencia = new PrecedenceGroup();
                        listaNums = new List<int>();
                        producesGroup = new List<Production>();
                        nodo = new Production();

                        actual = item.padre.param;
                        precedencia.Derivation = LALRNode.Derivation.LeftMost;
                        nodo.Left = grammarTokens.IndexOf(item.padre.param);

                        foreach (var item2 in item.produce)
                        {
                            listaNums.Add(grammarTokens.IndexOf(item2.param));
                        }

                        nodo.Right = listaNums.ToArray();
                        producesGroup.Add(nodo);
                    }
                }
            }

            //agrega el ultimo
            if (producesGroup.Count != 0)
            {
                precedencia.Productions = new Production[producesGroup.Count];
                precedencia.Productions = producesGroup.ToArray();
                listaPrecedencias.Add(precedencia);
            }

            grammar.PrecedenceGroups = listaPrecedencias.ToArray();
            #endregion

            Parser parser = new Parser(grammar);

            
            Debug.DumpParseTable(parser);

        }
    }
} 
