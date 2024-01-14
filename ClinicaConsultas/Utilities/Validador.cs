using System.Collections;
using System.Text.RegularExpressions;


namespace ClinicaConsultas.Utilities
{
    public class Validador
    {
        //Método que retorna um int validado pelo regex fornecido.
        public static int ReturnInt( string regex, string message )
        {
            string input;

            while ( true )
            {
                input = Console.ReadLine();

                if ( Regex.IsMatch( input, regex ) )
                {

                    return int.Parse( input );
                }

                Mensagens.MessageWriter( message );
            }
        }

        //Método que retorna uma string validada pelo regex fornecido.
        public static string ReturnString( string regex, string message )
        {
            string input;

            while ( true )
            {
                 input = Console.ReadLine();

                if ( Regex.IsMatch( input, regex ) )
                {
                    return input;  
                }

                Mensagens.MessageWriter( message );
            }
        }

        //Método que retorna um int válido e correspondente a posição do paciente selecionado na lista de pacientesCadastrados
        public static int ReturnValidId( IList list, string message )
        {
            int idlist;
            int intervaloMaximo = list.Count;

            do
            {
                string regex = "^^[0-9]+$"; // regex que permite qualquer caracter exceto numeros
                idlist = Validador.ReturnInt( regex, "Input inválid. Por favor, tente novamente (insira somente números):" );

                if ( idlist < 0 || idlist > intervaloMaximo )
                {
                    Mensagens.MessageWriter( message );
                }

            } while ( idlist < 0 || idlist > intervaloMaximo );

            return idlist - 1; // pegar a posicao na lista corretamente 
        }
    }
}

