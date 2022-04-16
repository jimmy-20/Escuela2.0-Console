using System;
using System.Collections.Generic;
using System.Linq;
using CoreEscuela.Entidades;

namespace CoreEscuela
{
    public sealed class EscuelaEngine
    {
        //La palabra sealed, permite sellar una clase, permitiendo
        //crear instancias pero no realizar herencia
        public Escuela Escuela { get; set; }

        //Constructor 
        public EscuelaEngine()  {}

        //Inicializacion
        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TiposEscuela.Primaria,
            ciudad: "Bogotá", pais: "Colombia"
            );

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela
        (
        bool traeEval = true,
        bool traeAlumnos = true,
        bool traeCursos = true,
        bool traeAsignaturas = true
        ){
            int dummy = 0;
            return GetObjetosEscuela(out dummy,out dummy,out dummy,out dummy); 
        }   

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela
        (
        out int conteoEvaluaciones,
        bool traeEval = true,
        bool traeAlumnos = true,
        bool traeCursos = true,
        bool traeAsignaturas = true
        ){
            int dummy = 0;
            return GetObjetosEscuela(out conteoEvaluaciones,out dummy,out dummy,out dummy); 
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela
        (
        out int conteoEvaluaciones,
        out int conteoAlumnos,
        bool traeEval = true,
        bool traeAlumnos = true,
        bool traeCursos = true,
        bool traeAsignaturas = true
        ){
            int dummy = 0;
            return GetObjetosEscuela(out conteoEvaluaciones,out conteoAlumnos,out dummy,out dummy); 
        }         

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela
        (
        out int conteoEvaluaciones,
        out int conteoAlumnos,
        out int conteoCursos,
        bool traeEval = true,
        bool traeAlumnos = true,
        bool traeCursos = true,
        bool traeAsignaturas = true
        ){
            int dummy = 0;
            return GetObjetosEscuela(out conteoEvaluaciones,out conteoAlumnos,out conteoCursos,out dummy); 
        }   

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela
        (
        out int conteoEvaluaciones ,
        out int conteoAlumnos ,
        out int conteoCursos ,
        out int conteoAsignaturas ,
        bool traeEval = true,
        bool traeAlumnos = true,
        bool traeCursos = true,
        bool traeAsignaturas = true
        )
        {
            conteoEvaluaciones = 0;
            conteoAlumnos = 0;
            conteoCursos = 0;
            conteoAsignaturas = 0;
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);

            
            if (traeCursos){
                listaObj.AddRange(Escuela.Cursos);
            }
            
            conteoCursos = Escuela.Cursos.Count;
            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;
                
                if (traeAsignaturas){
                    listaObj.AddRange(curso.Asignaturas);
                }

                if (traeAlumnos){
                    listaObj.AddRange(curso.Alumnos);
                }
                

                if (traeEval ){
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones+= alumno.Evaluaciones.Count;
                    }
                }
            }

            return listaObj;
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }


        #region Metodos de Carga
        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                        new Curso(){ Nombre = "101", Jornada = TiposJornada.Mañana },
                        new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                        new Curso{Nombre = "301", Jornada = TiposJornada.Mañana},
                        new Curso(){ Nombre = "401", Jornada = TiposJornada.Tarde },
                        new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde},
            };

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                            new Asignatura{Nombre="Matemáticas"} ,
                            new Asignatura{Nombre="Educación Física"},
                            new Asignatura{Nombre="Castellano"},
                            new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private void CargarEvaluaciones()
        {

            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        var rnd = new Random(System.Environment.TickCount);

                        for (int i = 0; i < 5; i++)
                        {
                            var ev = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = (float)(5 * rnd.NextDouble()),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(ev);
                        }
                    }
                }
            }
        }
        #endregion
    }
}