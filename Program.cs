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
            //AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            //AppDomain.CurrentDomain.ProcessExit += (o,s) => Printer.Beep(100,1000,1);

            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");

            var report = new Reporteador(engine.getDiccionarioObjetos());
            var Evaluaciones = report.GetEvaluaciones();
            var Asignaturas = report.GetAsignaturas();
            var AsignaturaXEvaluaciones = report.GetDicEvalXAsignatura();
            var listaPromedioAsignatura = report.GetPromedioAlumnoAsignatura();
            var MejoresAlumnosXAsignatura = report.GetExcelencia(3);

            Printer.WriteTitle("Captura de una Evaluacion por Consola");
            var newEval = new Evaluación();
            string nombre;
            string notastring;
            float nota;
            WriteLine("Ingrese el nombre de la evaluacion");
            Printer.PresioneENTER();
            nombre = Console.ReadLine();

            if (string.IsNullOrEmpty(nombre)){
                Printer.WriteTitle("El valor del nombre no puede ser nulo o vacio");
                WriteLine("Saliendo del programa . . .");
            }else{
                newEval.Nombre = nombre.ToLower();
                WriteLine("Nombre ingresado");
            }

            WriteLine("Ingrese la nota de la evaluacion");
            Printer.PresioneENTER();
            notastring = Console.ReadLine();

            if (string.IsNullOrEmpty(notastring)){
                Printer.WriteTitle("El valor de la nota no puede ser nulo o vacio");

            }else{
                try{
                    newEval.Nota = float.Parse(notastring);  

                    if (newEval.Nota < 0 || newEval.Nota > 5){
                        throw new ArgumentOutOfRangeException();
                    }

                    WriteLine("Nota ingresado");
                }catch(ArgumentOutOfRangeException ae){
                    Printer.WriteTitle(ae.Message);
                    WriteLine("La nota debe estar entre [0-5]");
                    WriteLine("Saliendo del programa");
                }catch(Exception e){
                    Printer.WriteTitle("El valor de la nota es invalido ");
                    WriteLine("Saliendo del programa");
                }finally{
                    Printer.Beep(2500,500,3);
                }
                
            }

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
