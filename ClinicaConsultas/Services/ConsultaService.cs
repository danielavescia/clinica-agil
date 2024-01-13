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
            Consulta? c = CreateAppointment( consultasCadastradas, pacientesCadastrados );

            if ( c == null ) //se consulta == nulo -  retorna para menu inicial
            {
                Mensagens.RetornaMensagem( "Houve um erro na criacao da consulta. Por favor, tente novamente!" );
            }
            
            //se está tudo ok paciente é adicionada a lista de Consultas Cadastrados
            Mensagens.RetornaMensagem( $"Consulta: cadastrado com sucesso! {c.ToString} " );
            consultasCadastradas.Add( c );
            
            return consultasCadastradas;
        }
       
        //cria Objetos Consultas a partir do input validado por outros métodos
        public static Consulta? CreateAppointment( List<Consulta> consultasCadastradas, List<Paciente> pacientesCadastrados )
        {
            DateOnly data;
            TimeOnly horario;
            int IdPaciente, idConsulta;
            string especialidade, regexEspecialidade = "^^(?!$).*";
            bool IsValid;

            try
            {
                Mensagens.RetornaMensagem( "     MARCAR CONSULTA     " );
                PacienteService.PrintPacientsList( pacientesCadastrados ); // imprime lista de pacientes cadastrados
                
                Console.WriteLine( $"{"\n"}Para marcar uma consulta adicione as informacoes solicitadas abaixo:" );
                Console.WriteLine( "Digite a Id do paciente:" );
                IdPaciente = ReturnValidId( pacientesCadastrados ); //validação id do paciente

                do {

                    Console.WriteLine( $"{"\n"}Data que deseja marcar (ex.:):" );
                    data = RetornaData(); //validação input data

                    Console.WriteLine( $"{"\n"}Horario que deseja marcar (ex.:):" );
                    horario = RetornaHorario(); //validação input horario

                    IsValid = IsAppointmentValid( data, horario, consultasCadastradas ); // validação horario e dia consulta

                }while ( IsValid == false );

                especialidade = Validador.RetornaString( regexEspecialidade ); //validacao especialidade

                Console.WriteLine( $"{"\n"}Especialidade que deseja marcar" );
                idConsulta = CriaId.IdGenerator( consultasCadastradas ); // cria Id

                Consulta consulta = new( idConsulta, IdPaciente, data, horario, especialidade );

                return consulta;

            }
            catch ( Exception e )
            {
                Mensagens.RetornaMensagem( $"Houve um erro ao Criar Consulta: {e.Message}. Tente Novamente!" );
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
                Mensagens.RetornaMensagem( "Não há nenhuma Consulta Marcada!!" );

            } else
              {
                Mensagens.RetornaMensagem( "     DESMARCAR CONSULTA     " );
                PrintAppointmentsList( consultasCadastradas ); // imprime lista de consultas cadastradas

                Console.WriteLine( $"{"\n"}Para desmarcar consulta digite a Id da consulta:" );
                int posicaoConsulta = ReturnValidId( consultasCadastradas );

                Mensagens.RetornaMensagem( consultasCadastradas [posicaoConsulta].ToString() );
                Console.WriteLine( $"{"\n"}Digite 1 para confirmar o cancelamento da consulta e 2 para voltar ao menu inicial:" );
                opcao = Validador.RetornaInt( regex );

                if ( opcao == 1 )
                {
                    consultasCadastradas.RemoveAt( posicaoConsulta );
                }

                else if ( opcao != 1 )
                {
                    Mensagens.RetornaMensagem( "Retornando ao Menu inicial" );
                }
              }

        }

        //MÉTODOS VALIDADORES

        //Método que retorna um int válido e correspondente a posição do paciente selecionado na lista de pacientesCadastrados
        public static int ReturnValidId( IList list )
        {
            int idlist;
            int intervaloMaximo = list.Count;

            do
            {
                string regex = "^^[0-9]+$"; // regex que permite qualquer caracter exceto numeros
                idlist = Validador.RetornaInt( regex );

                if ( idlist < 0 || idlist > intervaloMaximo )
                {
                    Mensagens.RetornaMensagem( "O número se encontra fora do intervalo das IDs de Pacientes cadastrados" );
                }

            } while ( idlist < 0 || idlist > intervaloMaximo );

            return idlist - 1; // pegar a posicao na lista corretamente 
        }

        //Método que retorna uma data válida para consulta através do input do usuario
        public static DateOnly RetornaData()
        {
            string regexDia = @"^(0[1-9]|[1-2]\d|3[0-1])$"; // dia - só aceita numeros positivos com 2 digitos entre 1-31
            string regexMes = @"^(0[1-9]|1[0-2])$"; //mes - só aceita numeros positivos com 2 digitos entre 1-12

            Console.WriteLine( $"{"\n"}Digite o DIA da consulta (FORMATO: XX):" );
            int dia = Validador.RetornaInt( regexDia );

            Console.WriteLine( $"{"\n"}Digite o MES da consulta (FORMATO: XX):" );
            int mes = Validador.RetornaInt( regexMes );

            int ano = DateTime.Now.Year;

            // objeto data é construido
            return new DateOnly( ano, mes, dia );
        }

        //Método que retorna um horario válido para consulta através do input do usuario
        public static TimeOnly RetornaHorario()
        {
            string regexHora = "^[0-2][0-9]*$"; // hora - só aceita numeros positivos com 2 digitos entre 00-23
            string regexMinutos = "^[0-5][0-9]$"; //mes - só aceita numeros positivos com 2 digitos entre 00-59
            
            Console.WriteLine( $"{"\n"}Digite o HORA da transacao (FORMATO: XX):" );
            int hora = Validador.RetornaInt( regexHora );

            Console.WriteLine( $"{"\n"}Digite os MINUTOS da transacao (FORMATO: XX):" );
            int minutos = Validador.RetornaInt( regexMinutos );
    
            // objeto hora é construido
            return new TimeOnly( hora, minutos, 00 );
        }

        //Método que verifica se horário está vago e se data n é retrogada. Caso seja valido vago retorna TRUE, caso ocupado ou data retrograda retorna FALSE
        public static bool IsAppointmentValid( DateOnly data, TimeOnly hora, List<Consulta> consultasCadastrados ) 
        {
            DateOnly diaHoje = new( DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year );
             
            if ( consultasCadastrados.Count == 0 )

            {
                return true;
            }

            foreach ( Consulta c in consultasCadastrados ) 
            {
                if ( c.Data.Equals( data ) && data < diaHoje) 
                {
                    if ( c.Hora.Equals( hora ) ) 
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //MÉTODOS DE IMPRESSÃO

        //imprime lista de consultas Cadastrados
        public static void PrintAppointmentsList( List<Consulta> consultasCadastrados )
        {

            if ( consultasCadastrados.Count == 0 )
            {
                Mensagens.RetornaMensagem( "Nao ha pacientes cadastrados no sistema!" );
            }
            foreach ( Consulta c in consultasCadastrados )
            {
                c.ToString();
            }
        }
    }
}
