using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Utilities;
using ClinicaConsultas.Data;

internal class Program
{
    public static void Main( string [] args )
    {
        //variaveis
        List<Paciente> pacientesCadastrados = LoadingData.CarregarDadosPacientes();
        List<Consulta> consultasCadastrados = new();

        //inicio programa
        Menu menu = new( pacientesCadastrados, consultasCadastrados );
    }
}