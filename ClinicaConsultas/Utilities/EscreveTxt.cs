using ClinicaConsultas.Models.Domain;

namespace ClinicaConsultas.Utilities
{
    public class EscreveTxt
    {
        public static void WriteFile( string caminhoArquivo, List<Paciente> pacientesCadastrados )
        {
            try
            {
                // Abre o arquivo para escrever os dados do paciente por linha
                using StreamWriter escritor = new( caminhoArquivo );
                foreach ( var paciente in pacientesCadastrados )
                {
                    string linha = $"{paciente.IdPaciente},{paciente.Nome},{paciente.Telefone}";
                    escritor.WriteLine( linha );
                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"Erro ao escrever dados no arquivo: {ex.Message}" );
            }
        }
    }
}

