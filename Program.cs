using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela.App
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            AppDomain.CurrentDomain.ProcessExit += (o,s) => Printer.Beep(100,1000,1);

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var report = new Reporteador(engine.getDiccionarioObjetos());
            var Evaluaciones = report.GetEvaluaciones();
            //Printer.Beep(10000, cantidad: 10);
            //ImpimirCursosEscuela(engine.Escuela);
            //is permite preguntar si un objeto es un objeto determinado
            //as permite castear un objeto y devuelve null si no puede
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
    }
}
