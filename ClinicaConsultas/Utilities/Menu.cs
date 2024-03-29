﻿using ClinicaConsultas.Data;
using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Services;

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
            Iniciar();
        }

        //chamada de todos os métodos para executar o programa
        public void Iniciar()
        {

            while ( MenuEstaAtivo )
            {

                PrintMenu();
                string regex = "^[1-9][0-9]*$";
                OpcaoMenu = Validador.ReturnInt( regex, "Digite um numero! Tente novamente:" );
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
                        ConsultaService.UpdateAppointmentList( AgendamentosCadastrados, PacientesCadastrados  );
                        Wait( 2000 );
                        break;
                    
                    //Cancelar Consultas
                    case 3:
                        ConsultaService.DeleteAppointment( AgendamentosCadastrados );
                        Wait( 2000 );
                        break;

                    //Salvar dados dos pacientes no txt e Encerrar Programa
                    case 4:
                        MenuEstaAtivo = false;
                        Mensagens.MessageWriter( "Salvando os dados..." );
                        Wait( 100 );
                        Console.WriteLine( "Encerrando o sistema..." );
                        LoadingData.SalvarDadosPacientes( PacientesCadastrados );
                        break;

                    default:
                        Console.WriteLine( "Digite uma opcao valida!" );
                        break;
                }
            }
        }

        //metodo que imprime menu inicial
        public static void PrintMenu()
        {
           
            string [] frases = new string []
             {

                "SISTEMA DE CONSULTAS",
                "1. Cadastrar Paciente",
                "2. Marcar Consultas",
                "3. Cancelamento de Consultas",
                "4. Encerrar aplicação (Salvar os dados)",
                 "Digite a opção desejada:",
             };

            Wait( 100 );
            Console.Clear();
            Mensagens.PrintMenu( frases );
     
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

