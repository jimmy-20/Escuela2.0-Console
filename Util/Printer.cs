using static System.Console;

namespace CoreEscuela.Util
{
    public static class Printer
    {
        public static void DrawLine(int tam = 10)
        {
            WriteLine("".PadLeft(tam, '='));
        }
        public static void PresioneENTER(){
            WriteLine("Presione ENTER para continuar");
        }

        public static void WriteTitle(string titulo)
        {
            var tamaño =titulo.Length + 4;
            DrawLine(tamaño);
            WriteLine($"| {titulo} |");
            DrawLine(tamaño);
        }

        public static void Beep(int hz = 2000, int tiempo=500, int cantidad =1)
        {
            while (cantidad-- > 0)
            {
                System.Console.Beep(hz, tiempo);
            }
        }

        public static void Menu(){
            WriteLine("1. Informacion de la escuela");
            WriteLine("2. Ver Cursos");
            WriteLine("3. Ver Asignaturas");
            WriteLine("4. Ver Alumnos");
            WriteLine("5. Consultar calificaciones");
            WriteLine("6. Salir");
        }
    }
}