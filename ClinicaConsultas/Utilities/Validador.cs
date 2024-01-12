using System.Text.RegularExpressions;
using ClinicaConsultas.Models.Domain;

namespace ClinicaConsultas.Utilities
{
    public class Validador
    {
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
            string input;

            while ( true )
            {
                 input = Console.ReadLine();

                if ( Regex.IsMatch( input, regex ) )
                {
                    return input;  
                }

                Console.WriteLine( "Você precisa digitar algo válido. Por favor, tente novamente:" );  
            }

        }

    }
}
