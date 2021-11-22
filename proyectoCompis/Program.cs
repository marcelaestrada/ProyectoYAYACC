using System;
using System.Collections.Generic;
using System.IO;


namespace proyectoCompis
{
    class Program
    {
        public static List<Token> tokens = new List<Token>();

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la url del archivo a evaluar");
            string expresion = Console.ReadLine();
           
            Console.WriteLine(expresion);

            string text = System.IO.File.ReadAllText(@expresion);

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
                    if (coleccion[0]!=Token.COLON && coleccion[0] != Token.SEMICOLON)
                    {
                        if (coleccion[0]!=Token.PIPE)
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
                             Token pad = new Token(estados.padre.symbol,estados.padre.param);
                            estados = new estados();
                            coleccion2 = new List<Token>();
                            estados.padre = pad;
                        }
                        
                    }
                    
                    coleccion.RemoveAt(0);
                }
                estados.produce = coleccion2;
                if (estados.produce.Count !=0)
                {
                    estadosCol.Add(estados);
                }
                
                primero = true;
                if (coleccion.Count==0)
                {
                    termina = true;
                }
            }


            Console.WriteLine("Marce El listado se llama: estadosCol que contiene el nodo padre que es el nombre de la produccion y produce " +
                "es la produccion que hace. Me hablas por cualquier cosa, suerte");

        }
    }
}
