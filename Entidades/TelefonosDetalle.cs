using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RegistroDetalle.Entidades
{
    public class TelefonosDetalle
    {
        private int idPersona;

        [Key]
        public int Id { get; set; }
        public int PersonaId { get; set; }
        public string TipoTelefono { get; set; }
        public string Telefono { get; set; }


       
        public TelefonosDetalle()
        {
            Id = 0;
            PersonaId = 0;
            TipoTelefono = string.Empty;
            Telefono = string.Empty;
        }

        public TelefonosDetalle(int id, int IdPersona, string telefono, string tipoTelefono)
        {
            Id = id;
            idPersona = IdPersona;
            Telefono = telefono;
            TipoTelefono = tipoTelefono;
        }
    }
}
