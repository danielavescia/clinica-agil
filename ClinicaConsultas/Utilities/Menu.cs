using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Services;
using System.Text;

namespace ClinicaConsultas.Utilities
{
    public class Menu
    {

        private bool MenuEstaAtivo;
        private int OpcaoMenu;
        List<Paciente> PacientesCadastrados;
        List<Consulta> AgendamentosCadastrados;

        public Menu( List<Paciente> PacientesCadastrados, List<Consulta> AgendamentosCadastrados )
        {
            this.PacientesCadastrados = PacientesCadastrados;
            this.AgendamentosCadastrados = AgendamentosCadastrados;
            MenuEstaAtivo = true;
            int OpcaoMenu;
            Iniciar();
        }


        //chamada de todos os métodos para executar o programa
        public void Iniciar()
        {

            while ( MenuEstaAtivo )
            {

                ImprimeMenuInicial();
                string regex = "^[1-9][0-9]*$";
                OpcaoMenu = Validador.RetornaInt( regex );
                Console.WriteLine( "Direcionando para a opcao desejada.." );

                switch ( OpcaoMenu )
                {
                    //Cadastrar Paciente       
                    case 1:
                        PacienteService.UpdatePacientesList( PacientesCadastrados );
                        Wait( 2000 );
                        break;

                    //Marcar Consultas
                    case 2:



                        break;

                    case 3:



                        break;

                    case 4:

                        MenuEstaAtivo = false;
                        Console.WriteLine( "Encerrando o sistema..." );
                        break;

                    default:
                        Console.WriteLine( "Digite uma opcao valida!" );
                        break;
                }
            }
        }

        //metodo que imprime menu inicial
        public static void ImprimeMenuInicial()
        {
           
            string [] frases = new string []
             {

                "SISTEMA DE CONSULTAS",
                "1. Cadastrar Paciente",
                "2. Marcar Consultas",
                "3. Cancelamento de Consultas",
                "4. Encerrar aplicação",
                 "Digite a opção desejada:",
             };

            Wait( 2000 );

            Console.Clear();
            Mensagens.RetornaMenu( frases );
     
        }

        //Deleta mensagens console
        public static void Wait( int milissegundos )
        {
            try
            {
                Thread.Sleep( milissegundos );
            }
            catch ( ThreadInterruptedException e )
            {
                Console.WriteLine( e.StackTrace );
            }

        }
    }
}

