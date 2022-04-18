using System;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela.App
{
    class Program
    {
        static void Main(string[] args)
        {
            //AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (o,s) => Printer.Beep(100,1000,1);

            var engine = new EscuelaEngine();
            engine.Inicializar();
            bool flag = true;
            string stringOpcion;
            int opcion = 0;

            do{
                Printer.WriteTitle("Bienvenido a " + engine.Escuela.Nombre);
                Printer.Menu();

                WriteLine("Ingrese una opcion >> ");
                stringOpcion = ReadLine();

                try{
                    if (string.IsNullOrEmpty(stringOpcion) && int.TryParse(stringOpcion, out int dummy)){
                        opcion = 0;
                    }else{
                        opcion = int.Parse(stringOpcion);
                    }

                }catch (FormatException fe){
                    Printer.WriteTitle(fe.Message);
                    WriteLine(nameof(stringOpcion) + ", no se puede convertir a entero \n");
                }

                switch (opcion)
                {
                    case 1:

                    break;

                    case 2:

                    break;

                    case 3:

                    break;

                    case 4:

                    break;

                    case 5:

                    break;

                    case 6:
                    flag = false;
                    break;
                    
                    default:
                        WriteLine("Por favor, ingrese una opcion valida \n\n");
                    break;
                }
            }while(flag);
            
        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.WriteTitle("Saliendo. . .");
            Printer.Beep(700,500,3);
            Printer.WriteTitle("SALIO");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de la Escuela");
            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre  }, Id  {curso.UniqueId}");
                }
            }
        }

        //is permite preguntar si un objeto es un objeto determinado
        //as permite castear un objeto y devuelve null si no puede
    }
}
