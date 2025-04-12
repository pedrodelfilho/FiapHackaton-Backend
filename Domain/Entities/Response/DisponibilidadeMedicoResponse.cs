﻿namespace Domain.Entities.Response
{
    public class DisponibilidadeMedicoResponse
    {
        public string Id { get; set; }
        public DateTime Data { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
    }

}
