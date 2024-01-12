using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Utilities;

namespace ClinicaConsultas.Services
{
    public class PacienteService
    {
        public static List<Paciente> UpdatePacientesList( List<Paciente> pacientesCadastrados ) 
        {
            //criacao de paciente e confere se telefone já foi cadastrado p/outro paciente
            Paciente? p = CreatePaciente( pacientesCadastrados );
            bool IsRepetead = IsPacientRepeated( p.Telefone, pacientesCadastrados );

            if ( p == null ) //se paciente == nulo -  retorna para tela de criancao de pacientes
            {
                Mensagens.RetornaMensagem( "Houve um erro na criacao do paciente. Por favor, tente novamente" );
            }
            else if ( p != null && IsRepetead == true ) //se paciente tem telefone existente no sistema chama update telefone
            {
                UpdateTelephone( p, pacientesCadastrados );

            }
            else if ( p != null && IsRepetead == false ) //se está tudo ok paciente é adicionada a lista de Pacientes Cadastrados
            {   
                Mensagens.RetornaMensagem( $"Paciente: {p.Nome} cadastrado com sucesso!" );
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
                Mensagens.RetornaMensagem( $"Houve um erro ao Criar Paciente: {e.Message}. Tente Novamente!");
                return null;
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

        //funcao que apenas altera o telefone do paciente caso ele já conste no sistema
        public static Paciente UpdateTelephone( Paciente p, List<Paciente> pacientesCadastrados ) 
        {
            string regexTelefone = @"^[0-9]?[0-9]*$", telefone;
            bool isTelefoneRepetead;

            while ( true )
            {
                Mensagens.RetornaMensagem( "Este telefone já pertence a outro paciente. Por favor, digite outro telefone(ex:51999999999)" );
                telefone = Validador.RetornaString( regexTelefone );
                isTelefoneRepetead = IsPacientRepeated(  p.Telefone, pacientesCadastrados );

                if( isTelefoneRepetead == false )
                { 
                    p.Telefone = telefone;
                    return p;
                }
            }
        
        }

        //imprime lista de pacientes Cadastrados
        public static void PrintPacietsList( List<Paciente> pacientesCadastrados ) 
        {
            
            if ( pacientesCadastrados.Count == 0 ) 
            {
                Mensagens.RetornaMensagem( "Nao ha pacientes cadastrados no sistema!" );
            }
            foreach ( Paciente p in pacientesCadastrados ) 
            {
                p.ToString();
            }
        }
    }
}
