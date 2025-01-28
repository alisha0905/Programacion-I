namespace Actividad_Alisha_2
{
    class Programa
    {
        static void Main(string[] args)
        {
            Console.Write("Por favor, escribe un año: ");
            int añoEvaluar = Convert.ToInt32(Console.ReadLine());

            while (añoEvaluar < 1582)
            {
                Console.WriteLine("El año ingresado debe ser igual o mayor a 1582.");
                Console.Write("Vuelve a ingresar un año válido: ");
                añoEvaluar = Convert.ToInt32(Console.ReadLine());
            }

            if (EsAñoBisiesto(añoEvaluar))
            {
                Console.WriteLine($"¡El año {añoEvaluar} es bisiesto!");
            }
            else
            {
                Console.WriteLine($"El año {añoEvaluar} no es bisiesto.");
            }

            bool EsAñoBisiesto(int año)
            {
                if (año % 400 == 0)
                {
                    return true;
                }
                if (año % 100 == 0)
                {
                    return false;
                }
                if (año % 4 == 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
