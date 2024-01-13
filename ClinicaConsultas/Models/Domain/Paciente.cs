

namespace ClinicaConsultas.Models.Domain
{
    public class Paciente: IPaciente
    {
        public int IdPaciente { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public Paciente( int IdPaciente, string Nome, string Telefone )
        {
            this.IdPaciente = IdPaciente;
            this.Nome = Nome;
            this.Telefone = Telefone;
        }

        public override string ToString()
        {
            return
                $@"
               -------------------------------------------
                DADOS PACIENTE {IdPaciente}:
               -------------------------------------------
               Nome: {Nome}
               Telefone: {Telefone}
               --------------------------------------------";
        }
    }
}
