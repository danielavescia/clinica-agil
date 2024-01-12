using System.Text.RegularExpressions;
using ClinicaConsultas.Models.Domain;
using ClinicaConsultas.Utilities;

namespace ClinicaConsultas.Utilities
{
    public class Validador
    {
        //Método que retorna um int validado pelo regex fornecido.
        public static int RetornaInt( string regex )
        {
            string input;
            bool isValid;

            do
            {
                input = Console.ReadLine();
                isValid = Regex.IsMatch( input, regex );

                
            } while ( !isValid );

            return int.Parse( input );

        }

        //Método que retorna uma string validada pelo regex fornecido.
        public static string RetornaString( string regex )
        {
            string input, mensagem = "Você precisa digitar algo válido. Por favor, tente novamente:";

            while ( true )
            {
                 input = Console.ReadLine();

                if ( Regex.IsMatch( input, regex ) )
                {
                    return input;  
                }

                Mensagens.RetornaMensagem( mensagem );
            }
        }

    }
}

