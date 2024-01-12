using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Utilities;


namespace ClinicaConsultas.Data
{
    
    public class LoadingData
    {
        
        public static List<Paciente> CarregarDadosPacientes( ) 
        {
            List<Paciente> pacientesCadastrados = new();

            string caminhoArquivo = "E:\\Dani\\Dani\\SI\\Unisinos\\2- Meus_Projetos\\AceleradoraAgil\\Exercicio1\\ClinicaConsultas\\ClinicaConsultas\\Assets\\Pacientes.txt";

            pacientesCadastrados = LeitorTxt.LerDadosDoArquivo( caminhoArquivo );

            return  pacientesCadastrados;
        }


   
    }
}
