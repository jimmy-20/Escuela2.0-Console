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

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromedioAlumnoAsignatura(){
            var rta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var dicEvalXAsig = GetDicEvalXAsignatura();

            foreach (var dic in dicEvalXAsig)
            {
                var promedioAlumnos = from eval in dic.Value
                            group eval by new {
                                eval.Alumno.UniqueId,
                                eval.Alumno.Nombre
                            }
                            into grupoEvalAlumno
                            select new AlumnoPromedio {
                                AlumnoId = grupoEvalAlumno.Key.UniqueId,
                                AlumnoNombre = grupoEvalAlumno.Key.Nombre,
                                Promedio = grupoEvalAlumno.Average( (e) => e.Nota)
                            }; //la variable dummy es de tipo anonima. no se especifica el tipo de objeto que contiene
                rta.Add(dic.Key,promedioAlumnos);
            }

            return rta;
        }

        public Dictionary<string,IEnumerable<AlumnoPromedio>> GetExcelencia(int top){
            var promediosAsignatura = GetPromedioAlumnoAsignatura();
            var mejoresNotas = new Dictionary<string,IEnumerable<AlumnoPromedio>>();
            foreach (var dic in promediosAsignatura)
            {
                /*
                De la lista de valores del diccionario, ordenar por promedio
                de manera descendente y seleccionar las evaluaciones, del resultado
                tomar los -top- primeros
                */
                var topEvaluacion = (from eval in dic.Value
                                    orderby eval.Promedio descending
                                    select eval).Take(top);

                mejoresNotas.Add(dic.Key,topEvaluacion);
            }

            return mejoresNotas;           
        }
    }
}