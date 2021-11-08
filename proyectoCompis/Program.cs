using System;
using System.Collections.Generic;

namespace proyectoCompis
{
    class Program
    {
        public static List<Token> tokens = new List<Token>();
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la expresion que desea evaluar");
            string expresion = Console.ReadLine();
            Console.WriteLine(expresion);
            Validate validate = new Validate(expresion);
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
