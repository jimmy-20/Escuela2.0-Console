using System;

namespace CoreEscuela.Entidades
{
    public abstract class ObjetoEscuelaBase
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }

        public ObjetoEscuelaBase()
        {
            UniqueId = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Nombre},{UniqueId}";
        }

        // la clave reservada Abstrac permite no crear instancia de esta clase
        // solo funciona para realizar herencia

        //En C# no existe la herencia multiple, solo en C++
        //aun asi no es un limitante para hacer que un objeto se vea
        //como otros objetos diferentes
    }
}