using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;
using CoreEscuela.Entidades.Llaves;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        private Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> diccionario;
        public Reporteador(Dictionary<LlaveDiccionario,IEnumerable<ObjetoEscuelaBase>> dicObjEsc)
        {
            if (dicObjEsc == null){
                throw new ArgumentNullException(nameof(dicObjEsc));
            }
            diccionario = dicObjEsc;
        }

        public IEnumerable<Evaluación> GetEvaluaciones(){

            if (diccionario.TryGetValue(LlaveDiccionario.Evaluaciones,out IEnumerable<ObjetoEscuelaBase> lista)){
                return diccionario[LlaveDiccionario.Evaluaciones].Cast<Evaluación>();
            }

            return new List<Evaluación>();
        }

        public IEnumerable<string> GetAsignaturas(){

            return GetAsignaturas(out var dummy); 
        }

        public IEnumerable<string> GetAsignaturas(out IEnumerable<Evaluación> listaEvaluaciones){
            listaEvaluaciones = GetEvaluaciones();

            return (from /*Evaluación*/ ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct(); 
        }

        public Dictionary<string,IEnumerable<Evaluación>> GetDicEvalXAsignatura(){
            var dicRta = new Dictionary<string,IEnumerable<Evaluación>>();

            var asignaturas = GetAsignaturas(out var listaEval);

            foreach (var a in asignaturas)
            {
                var evalAsig = from Evaluación ev in listaEval
                                where ev.Asignatura.Nombre == a 
                                select ev;
                dicRta.Add(a,evalAsig);
            }

            return dicRta;
        }
    }
}