using System;

namespace proyectoCompis
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingrese la expresion que desea evaluar");
            string expresion = Console.ReadLine();
            Console.WriteLine(expresion);
            Validate validate = new Validate(expresion);
            foreach (var item in validate.recorrer())
            {
                Console.WriteLine(item);
            }
        }
    }
}
