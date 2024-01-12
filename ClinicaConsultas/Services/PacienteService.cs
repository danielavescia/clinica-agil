using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Utilities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace ClinicaConsultas.Services
{
    public class PacienteService
    {
        public static List<Paciente> UpdatePacientesList( List<Paciente> pacientesCadastrados ) 
        {
            Paciente? p = CreatePaciente( pacientesCadastrados );
            bool IsRepetead = IsPacientRepeated( p, pacientesCadastrados );
            string mensagem = $"Paciente: {p.Nome} cadastrado com sucesso!";

            if ( p == null ) //se paciente == nulo -  retorna para tela de criancao de pacientes
            {
                Console.WriteLine( "Houve um erro na criacao do paciente. Por favor, tente novamente::" );
                p = CreatePaciente( pacientesCadastrados );
            }
            else if ( p != null && IsRepetead == true ) //se paciente tem telefone existente no sistema chama update telefone
            {
                UpdateTelephone( p, pacientesCadastrados );

            }
            else if ( p != null && IsRepetead == false ) //se está tudo ok paciente é adicionada a lista de Pacientes Cadastrados
            {   
                Mensagens.RetornaMensagem( mensagem );
                pacientesCadastrados.Add( p );
            }

            return pacientesCadastrados;
        }

        //cria Objetos Pacientes a partir do input validado.
        public static Paciente? CreatePaciente( List<Paciente> pacientesCadastrados ) 
        {
            string nome, telefone, regexNome = "^^(?!$).*", regexTelefone = @"^[0-9]?[0-9]*$";
            int id;

            try 
            {   
                Mensagens.RetornaMensagem( "     CRIAR PACIENTE     " );
                Console.WriteLine( "Para criar uma paciente adicione as informacoes solicitadas abaixo:" );

                Console.WriteLine( $"{"\n"}Digite o nome do paciente:" );
                nome = Validador.RetornaString( regexNome );

                Console.WriteLine( $"{"\n"}Número de telefone(são aceitos somente numeros):" );
                telefone = Validador.RetornaString( regexTelefone );

                id = CriaId.AtribuiId( pacientesCadastrados );

                Paciente paciente = new( id, nome, telefone );

                return paciente;

            } catch ( Exception e )
            {
                Console.WriteLine( $"Houve um erro ao Criar Paciente: {e.Message}. Tente Novamente!");
                return null;
            }
        }

        public static bool IsPacientRepeated( Paciente p, List<Paciente> pacientesCadastrados ) 

        {
            foreach ( Paciente paciente in pacientesCadastrados ) 
            {
                if ( p.Telefone.Equals( paciente.Telefone ) ) 
                {
                    return true;
                }
            }
            return false;
        }

        //funcao que apenas altera o telefone do paciente caso ele já conste no sistema
        public static Paciente UpdateTelephone( Paciente p, List<Paciente> pacientesCadastrados ) 
        {
            string regexTelefone = "@\"^[0-9]+$", telefone, mensagem = "Este telefone já pertence a outro paciente. Por favor, digite outro telefone(são aceitos somente numeros";
            bool isTelefoneRepetead;

            while ( true )
            {
                Mensagens.RetornaMensagem( mensagem );
                telefone = Validador.RetornaString( regexTelefone );
                isTelefoneRepetead = IsPacientRepeated(  p, pacientesCadastrados );

                if( isTelefoneRepetead == false )
                { 
                    p.Telefone = telefone;
                    return p;
                }
            }
        
        }

        public static void PrintPacietsList( List<Paciente> pacientesCadastrados ) 
        {
            string mensagem = "Nao ha pacientes cadastrados no sistema!";

            if ( pacientesCadastrados.Count == 0 ) 
            {
                Mensagens.RetornaMensagem( mensagem );
            }
            foreach ( Paciente p in pacientesCadastrados ) 
            {
                p.ToString();
            }
        }
    }
}
