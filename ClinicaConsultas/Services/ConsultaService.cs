using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Utilities;
using System.Collections;

namespace ClinicaConsultas.Services
{

    public class ConsultaService
    {
        
        //Valida criação de Consultas e adiciona a Lista de Consultas cadastradas
        public static List<Consulta> UpdateAppointmentList(  List<Consulta> consultasCadastradas, List<Paciente> pacientesCadastrados )
        {
            //criacao de consulta e confere se horario e dia já foi agendado p/outro paciente
            Consulta c = CreateAppointment( consultasCadastradas, pacientesCadastrados );

            if ( c == null ) //se consulta == nulo -  retorna para menu inicial
            {
                Mensagens.MessageWriter( "Houve um erro na criacao da consulta. Por favor, tente novamente!" );
            }
            
            //se está tudo ok paciente é adicionada a lista de Consultas Cadastrados
            Mensagens.MessageWriter( $"Consulta: cadastrado com sucesso! " );
            consultasCadastradas.Add( c );
            
            return consultasCadastradas;
        }
       
        //cria Objetos Consultas a partir do input validado por outros métodos
        public static Consulta? CreateAppointment( List<Consulta> consultasCadastradas, List<Paciente> pacientesCadastrados )
        {
            DateTime agendamento;
            int IdPaciente, idConsulta;
            string especialidade, regexEspecialidade = "^^(?!$).*";
            bool IsValid;

            try
            {
                Mensagens.MessageWriter( "     MARCAR CONSULTA     " );
                PacienteService.PrintPacientsList( pacientesCadastrados ); // imprime lista de pacientes cadastrados
                
                Console.WriteLine( $"{"\n"}Para marcar uma consulta adicione as informacoes solicitadas abaixo:" );

                Console.WriteLine( $"{"\n"}Digite a Id do paciente:" );
                IdPaciente = Validador.ReturnValidId( pacientesCadastrados, "O número se encontra fora do intervalo das IDs de Pacientes cadastrados" ); //validação id do paciente

                Console.WriteLine( $"{"\n"}Especialidade que deseja marcar" );
                idConsulta = CriaId.IdGenerator( consultasCadastradas ); // cria Id

                especialidade = Validador.ReturnString( regexEspecialidade, "Entrada inválida!Por favor, digite novamente a especialidade: " ); //validacao especialidade
                
                do {
                    agendamento = ReturnDateTime();

                    IsValid = IsAppointmentValid( agendamento, consultasCadastradas ); // validação horario e dia consulta

                }while ( IsValid != true );

                Consulta consulta = new( idConsulta, IdPaciente, agendamento, especialidade );

                return consulta;

            } catch ( Exception e )
            {
                Mensagens.MessageWriter( $"Houve um erro ao Criar Consulta: {e.Message}. Tente Novamente!" );
                return null;
            }
        }

        //deleta Objetos Consultas input validado 
        public static void DeleteAppointment( List<Consulta> consultasCadastradas ) 
        {
            string regex = "^(1|2)$";
            int opcao;

            if ( consultasCadastradas.Count == 0 )
            {
                Mensagens.MessageWriter( "Não há nenhuma Consulta Marcada!!" );

            } else
              {
                Mensagens.MessageWriter( "     DESMARCAR CONSULTA     " );
                PrintAppointmentsList( consultasCadastradas ); // imprime lista de consultas cadastradas

                Console.WriteLine( $"{"\n"}Para desmarcar consulta digite a Id da consulta:" );
                int posicaoConsulta = Validador.ReturnValidId( consultasCadastradas, "O número se encontra fora do intervalo das IDs de Consultas cadastradas" );

                Console.WriteLine( consultasCadastradas [posicaoConsulta].ToString());
                Console.WriteLine( $"{"\n"}Digite 1 para confirmar o cancelamento da consulta ou 2 para voltar ao menu inicial:" );
                opcao = Validador.ReturnInt( regex, "Entrada inválida, digite 1 p/cancelar ou 2 p/ voltar ao menu inicial" );

                if ( opcao == 1 )
                {
                    consultasCadastradas.RemoveAt( posicaoConsulta );
                    Mensagens.MessageWriter( "Consulta Cancelada! Retornando ao Menu inicial..." );
                }

                else if ( opcao == 2 )
                {
                    Mensagens.MessageWriter( "Retornando ao Menu inicial" );
                }
              }

        }
        //Método que retorna uma data válida para consulta através do input do usuario
        public static DateTime ReturnDateTime()
        {
            string regexDia = @"^(0[1-9]|[1-2]\d|3[0-1])$"; // dia - só aceita numeros positivos com 2 digitos entre 1-31
            string regexMes = @"^(0[1-9]|1[0-2])$"; //mes - só aceita numeros positivos com 2 digitos entre 1-12
            string regexHora = @"^(0[0-9]|1[0-9]|2[0-3])$"; // hora - só aceita numeros positivos com 2 digitos entre 00-23
            string regexMinutos = @"^[0-5][0-9]$"; //mes - só aceita numeros positivos com 2 digitos entre 00-59

            Console.WriteLine( $"{"\n"}Digite o DIA da consulta (FORMATO: XX):" );
            int dia = Validador.ReturnInt( regexDia, "Input inválido! Tente inserir o dia novamente (2 digitos entre 1-31):" );

            Console.WriteLine( $"{"\n"}Digite o MES da consulta (FORMATO: XX):" );
            int mes = Validador.ReturnInt( regexMes, "Input inválido! Tente inserir o mes novamente (2 digitos entre 1-12):" );

            int ano = DateTime.Now.Year;

            Console.WriteLine( $"{"\n"}Digite a HORA da consulta (FORMATO: XX):" );
            int hora = Validador.ReturnInt( regexHora, "Input inválido! Tente inserir a hora novamente (2 digitos entre 1-23):" );

            Console.WriteLine( $"{"\n"}Digite os MINUTOS da consulta (FORMATO: XX):" );
            int minutos = Validador.ReturnInt( regexMinutos, "Input inválido! Tente inserir os minutos novamente (2 digitos entre 00-59): " );

            TimeSpan time = new ( hora, minutos, 0 );
           
            // objeto datatime é construido
            return new DateTime( ano, mes, dia ).Add( time );
        }

        //Método que verifica se horário está vago e se data n é retrogada. Caso seja valido vago retorna TRUE, caso ocupado ou data retrograda retorna FALSE
        public static bool IsAppointmentValid( DateTime agendamentoDesejado, List<Consulta> consultasCadastrados ) 
        {
            DateTime diaHoje = (DateTime.Now).Date;
            Console.WriteLine( diaHoje );
             
            if ( consultasCadastrados.Count == 0 )

            {
                return true;
            }


            foreach ( Consulta c in consultasCadastrados ) 
            {
                if ( c.Agendamento.Equals( agendamentoDesejado ) )
                {

                    if ( ( c.Agendamento ).TimeOfDay.Equals( agendamentoDesejado.TimeOfDay ) )
                    {
                        Mensagens.MessageWriter( "Já há uma consulta marcada neste horário! Tente Novamente:" );
                        return false;
                    }
                }

                else if ( agendamentoDesejado < diaHoje ) 
                {
                    Mensagens.MessageWriter( "Não é possível marcar em datas retrogradas! Tente Novamente:" );
                    return false;
                }
            }
            return true;
        }

        //imprime lista de consultas Cadastrados
        public static void PrintAppointmentsList( List<Consulta> consultasCadastrados )
        {

            if ( consultasCadastrados.Count == 0 )
            {
                Mensagens.MessageWriter( "Nao ha consultas cadastrados no sistema!" );
            }
            foreach ( Consulta c in consultasCadastrados )
            {
               Console.WriteLine( c.ToString());
            }
        }
    }
}
