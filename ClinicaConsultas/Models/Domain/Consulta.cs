﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaConsultas.Models.Domain
{
    public class Consulta
    {
        public int IdConsulta { get; set; }
        public Paciente IdPaciente { get; set; }

        public DateOnly Data { get; set; }

        public TimeOnly Hora { get; set; }

        public string Especialidade { get; set; }

        public Consulta(int IdConsulta, Paciente IdPaciente, DateOnly Data, TimeOnly Hora, string Especialidade ) 
        {
            this.IdConsulta = IdConsulta;
            this.IdPaciente = IdPaciente;
            this.Data = Data;
            this.Hora = Hora;
            this.Especialidade = Especialidade;
        }
    }
}
