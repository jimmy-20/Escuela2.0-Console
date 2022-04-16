using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Entidades.Interfaces;
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

            int dummy = 0;
            var listaObjetos = engine.GetObjetosEscuela(out int conteoEvaluaciones,out int Alumnos);
            Dictionary<int,string> diccionario = new Dictionary<int, string>();
            diccionario.Add(10,"Juan");
            diccionario.Add(23,"Lorem Ipsum");

            foreach (var keyValPair in diccionario)
            {
                WriteLine($"Key: {keyValPair.Key} Valor: {keyValPair.Value}");
            }

            
            Printer.WriteTitle("Acceso a Diccionario");
            WriteLine(diccionario[23]);

            Printer.WriteTitle("Otro diccionario");
            Dictionary<string,string> dic = new Dictionary<string, string>();
            dic["Luna"] = "Cuerpo celeste que gira alrededor de la tierra";

            WriteLine(dic["Luna"]);
            //is permite preguntar si un objeto es un objeto determinado
            //as permite castear un objeto y devuelve null si no puede
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
