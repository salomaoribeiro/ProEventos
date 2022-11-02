﻿namespace ProEventos.Domain.Models
{
    public class RedeSocial
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string URL { get; set; }
        public int? EventoId { get; set; }
        public Evento Evento { get; set; }
        public int? PalestranteID { get; set; }
        public Palestrante Palestrante { get; set; }
    }
}
