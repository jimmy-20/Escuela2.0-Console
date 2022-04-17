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
            IEnumerable<Evaluación> rta;

            if (diccionario.TryGetValue(LlaveDiccionario.Evaluaciones,out IEnumerable<ObjetoEscuelaBase> lista)){
                rta = diccionario[LlaveDiccionario.Evaluaciones].Cast<Evaluación>();
            }else{
                rta = null;
            }
            return rta;
        }
    }
}