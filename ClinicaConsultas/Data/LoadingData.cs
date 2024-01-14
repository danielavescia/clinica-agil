using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Utilities;

namespace ClinicaConsultas.Data
{
    
    public class LoadingData
    {
        
        public static List<Paciente> CarregarDadosPacientes( ) 
        {
            List<Paciente> pacientesCadastrados = new();

            string caminhoArquivo;

            caminhoArquivo = PathConstructor();

            pacientesCadastrados = LeitorTxt.FileReader( caminhoArquivo );

            return  pacientesCadastrados;
        }

        public static void SalvarDadosPacientes( List<Paciente> pacientesCadastrados )
        {
            string caminhoArquivo, caminhoArquivoCompleto;

            caminhoArquivo = PathConstructor();

            EscreveTxt.WriteFile( caminhoArquivo, pacientesCadastrados );
     
        }

        public static string PathConstructor() 
        {
            // Obtém o diretório do arquivo do assembly que contém a classe Program
            string diretorioAtual = Path.GetDirectoryName( typeof( Program ).Assembly.Location );

            // Navega até 3 diretorios 
            string diretorioDestino = Path.Combine( diretorioAtual, "..", "..", ".." );

            // Constroi todo o caminho do arquivo
            string caminhoArquivo = Path.GetFullPath( diretorioDestino );

            return caminhoArquivo + "//Data//Pacientes.txt";

        }
    }
}
