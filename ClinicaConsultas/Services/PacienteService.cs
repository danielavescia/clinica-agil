using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Utilities;
using System.ComponentModel;

namespace ClinicaConsultas.Services
{
    public class PacienteService
    {
        //Valida criação do Paciente e adiciona a Lista de Pacientes cadastrados
        public static List<Paciente> UpdatePacientesList( List<Paciente> pacientesCadastrados ) 
        {
            //criacao de paciente e confere se telefone já foi cadastrado p/outro paciente
            Paciente? p = CreatePaciente( pacientesCadastrados );
            bool IsRepetead = IsPacientRepeated( p.Telefone, pacientesCadastrados );

            if ( p == null ) //se paciente == nulo -  retorna para tela de criancao de pacientes
            {
                Mensagens.MessageWriter( "Houve um erro na criacao do paciente. Por favor, tente novamente!" );
            }
            else if ( p != null && IsRepetead == true ) //se paciente tem telefone existente imprime mensagem e retorna p/menu
            {
                Mensagens.MessageWriter( "Erro: Paciente já cadastrado! Retornando ao menu inicial..." );
            }
            else if ( p != null && IsRepetead == false ) //se está tudo ok paciente é adicionada a lista de Pacientes Cadastrados
            {   
                Mensagens.MessageWriter( $"Paciente: {p.Nome} cadastrado com sucesso!" );
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
                Mensagens.MessageWriter( "     CRIAR PACIENTE     " );
                Console.WriteLine( "Para criar uma paciente adicione as informacoes solicitadas abaixo:" );

                Console.WriteLine( $"{"\n"}Digite o nome do paciente:" );
                nome = Validador.ReturnString( regexNome, "Por favor, digite o o nome do paciente:" );

                Console.WriteLine( $"{"\n"}Número de telefone(são aceitos somente numeros):" );
                telefone = Validador.ReturnString( regexTelefone, "Por favor, são aceitos somente numeros. Digite novamente o telefone: " );

                id = CriaId.IdGenerator( pacientesCadastrados );

                Paciente paciente = new( id, nome, telefone );

                return paciente;

            } catch ( Exception e )
            {
                Mensagens.MessageWriter( $"Houve um erro ao Criar Paciente: {e.Message}. Tente Novamente!");
                return  null;
            }
        }

        //confere se o telefone se o telefone já consta como telefone de outro paciente
        public static bool IsPacientRepeated( string telefone, List<Paciente> pacientesCadastrados ) 

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

        //imprime lista de pacientes Cadastrados
        public static void PrintPacientsList( List<Paciente> pacientesCadastrados ) 
        {

            if ( pacientesCadastrados.Count == 0 )
            {
                Mensagens.MessageWriter( "Nao ha pacientes cadastrados no sistema!" );
            }
            else
            {
                foreach ( Paciente p in pacientesCadastrados )
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }
    }
}
