using ClinicaConsultas.Models.Domain;


namespace ClinicaConsultas.Utilities
{
    public class LeitorTxt
    {
        public static List<Paciente> LerDadosDoArquivo( string caminhoArquivo )
        {
            List<Paciente> pacientesCadastrados= new ();

            try
            {
                // Abre o arquivo para leitura
                using StreamReader leitor = new( caminhoArquivo );
                while ( !leitor.EndOfStream )
                {
                    string linha = leitor.ReadLine();
                    string [] dados = linha.Split( ',' );

                    if ( dados.Length == 3 )
                    {
                        // Converte os dados e adiciona à lista
                        int idPaciente = Convert.ToInt32( dados [0] );
                        string nome = dados [1];
                        string telefone = dados [2];

                        Paciente paciente = new ( idPaciente, nome, telefone )
                        {
                            IdPaciente = idPaciente,
                            Nome = nome,
                            Telefone = telefone
                        };

                        pacientesCadastrados.Add( paciente );
                    }

                }
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"Erro ao ler dados do arquivo: {ex.Message}" );
            }

            return pacientesCadastrados;
        }
    }
}
