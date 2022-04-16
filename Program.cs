using System;
using System.Collections.Generic;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            Printer.WriteTitle("BIENVENIDOS A LA ESCUELA");
            //Printer.Beep(10000, cantidad: 10);
            ImpimirCursosEscuela(engine.Escuela);
            //var listaObjetos = engine.GetObjetosEscuela();

            var objDummy = new ObjetoEscuelaBase();
            Printer.DrawLine(20);
            Printer.WriteTitle("Pruebas de Polimorfismo");
            var alumnoTest = new Alumno{Nombre = "Claire Underwood"};
            
            Printer.WriteTitle("Alumno");
            WriteLine($"Nombre: {alumnoTest.Nombre}");
            WriteLine($"Id: {alumnoTest.UniqueId}");
            WriteLine($"Tipo: {alumnoTest.GetType()}");

            var Evaluacion = new Evaluación{Nombre="Matematicas",Nota = 4.5f};
            Printer.WriteTitle("Evaluacion");
            WriteLine($"Nombre: {Evaluacion.Nombre}");
            WriteLine($"Id: {Evaluacion.UniqueId}");
            WriteLine($"Nota: {Evaluacion.Nota}");
            WriteLine($"Tipo: {Evaluacion.GetType()}");

            objDummy = Evaluacion;
            Printer.WriteTitle("ObjetoEscuelaBase");
            WriteLine($"Nombre: {objDummy.Nombre}");
            WriteLine($"Id: {objDummy.UniqueId}");
            WriteLine($"Tipo: {Evaluacion.GetType()}");

            //Evaluacion es un OSB pero el OBS no es un Alumno, aunque C# nos permita
            alumnoTest = (Alumno) (ObjetoEscuelaBase) Evaluacion;
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
