using System;

namespace CoreEscuela.Entidades
{
    public class ObjetoEscuelaBase
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
    }
}