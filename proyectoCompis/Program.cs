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

        }
    }
}
