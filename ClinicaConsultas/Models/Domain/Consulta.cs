using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaConsultas.Models.Domain
{
    public class Consulta:IPaciente
    {
        public int IdConsulta { get; set; }
        public int IdPaciente { get; set; }

        public DateTime Agendamento { get; set; }

        public string Especialidade { get; set; }

        public Consulta(int IdConsulta, int IdPaciente, DateTime Agendamento,  string Especialidade ) 
        {
            this.IdConsulta = IdConsulta;
            this.IdPaciente = IdPaciente;
            this.Agendamento = Agendamento;
            this.Especialidade = Especialidade;
        }

        public override string ToString()
        {
            return
               $@"
               -------------------------------------------
                DADOS REFERENTES DA CONSULTA {IdConsulta}:
               -------------------------------------------
               Data: {Agendamento.Day}/{Agendamento.Month}/{Agendamento.Year}
               Hora: {Agendamento.TimeOfDay}
               Especialidade: {Especialidade}
               --------------------------------------------";
        }
    }
}
