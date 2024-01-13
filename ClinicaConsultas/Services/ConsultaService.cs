using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaConsultas.Services
{

    public class ConsultaService
    {
        /*
        //Valida criação de Consultas e adiciona a Lista de Consultas cadastradas
        public static List<Consulta> UpdatePacientesList( List<Paciente> consultasCadastrados )
        {
            //criacao de paciente e confere se telefone já foi cadastrado p/outro paciente
            Paciente? p = CreatePaciente( pacientesCadastrados );
            bool IsRepetead = IsPacientRepeated( p.Telefone, pacientesCadastrados );

            if ( p == null ) //se paciente == nulo -  retorna para tela de criancao de pacientes
            {
                Mensagens.RetornaMensagem( "Houve um erro na criacao do paciente. Por favor, tente novamente!" );
            }
            else if ( p != null && IsRepetead == true ) //se paciente tem telefone existente imprime mensagem e retorna p/menu
            {
                Mensagens.RetornaMensagem( "Erro: Paciente já cadastrado! Retornando ao menu inicial..." );

            }
            else if ( p != null && IsRepetead == false ) //se está tudo ok paciente é adicionada a lista de Pacientes Cadastrados
            {
                Mensagens.RetornaMensagem( $"Paciente: {p.Nome} cadastrado com sucesso!" );
                pacientesCadastrados.Add( p );
            }

            return pacientesCadastrados;
        }
        */
        //cria Objetos Consultas a partir do input validado.
        public static Consulta? CreateAppointment( List<Consulta> consultasCadastrados, List<Paciente> pacientesCadastrados )
        {
            DateOnly data;
            TimeOnly horario;
            int IdPaciente, idConsulta;
            string especialidade, regexNome = "^^(?!$).*", regexNumero = @"^[0-9]?[0-9]*$";
            

            try
            {
                Mensagens.RetornaMensagem( "     MARCAR CONSULTA     " );
                PacienteService.PrintPacientsList( pacientesCadastrados );
                
                Console.WriteLine( $"{"\n"}Para marcar uma consulta adicione as informacoes solicitadas abaixo:" );
                Console.WriteLine( "Digite a Id do paciente:" );
                IdPaciente = Validador.RetornaInt( regexNumero );

                Console.WriteLine( $"{"\n"}Data que deseja marcar (ex.:):" );
                telefone = Validador.RetornaString( regexTelefone );

                id = CriaId.AtribuiId( consultasCadastrados );

                Paciente paciente = new( id, nome, telefone );

                return paciente;

            }
            catch ( Exception e )
            {
                Mensagens.RetornaMensagem( $"Houve um erro ao Criar Paciente: {e.Message}. Tente Novamente!" );
                return null;
            }
        }
        /*
        //confere se a data consulta está vaga 
        public static bool IsPacientRepeated( DateOnly data, DateTime horario, List<Consulta> consultasCadastrados )

        {
            foreach ( Paciente paciente in pacientesCadastrados )
            {
                if ( telefone.Equals( paciente.Telefone ) )
                {
                    return true;
                }
            }
            return false;
        }

        //imprime lista de consultas Cadastrados
        public static void PrintPacientsList( List<Consulta> consultasCadastrados )
        {

            if ( pacientesCadastrados.Count == 0 )
            {
                Mensagens.RetornaMensagem( "Nao ha pacientes cadastrados no sistema!" );
            }
            foreach ( Paciente p in pacientesCadastrados )
            {
                p.ToString();
            }
        }*/
    }
}
