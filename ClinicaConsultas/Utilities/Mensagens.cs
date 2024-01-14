using System.Text;


namespace ClinicaConsultas.Utilities
{
    public class Mensagens
    {
        //constroi as mensagens do sistema conforme o parametro inserido
        public static void MessageWriter( String mensagem )
        {

            StringBuilder sb = new();

            sb.AppendLine();
            sb.Append( "." );
            sb.Append( '-', mensagem.Length + 2 );
            sb.Append( "." );
            sb.AppendFormat( "\n| {0} |\n", mensagem );
            sb.Append( "." );
            sb.Append( '-', mensagem.Length + 2 );
            sb.Append( "." );
            sb.AppendLine();

            Console.Write(sb);
        }

        public static void PrintMenu( string [] frases )
        {
            StringBuilder sb = new();

            if ( frases == null )
            {
                throw new Exception( "Adicione strings ao array para imprimir as mensagens corretamente!" );
            }

            else
            {

                int indiceMaiorPalavra = ReturnBiggestString( frases );
                int larguraMenu = frases [indiceMaiorPalavra].Length;

                sb.AppendLine( $".{new string( '_', larguraMenu + 2 )}." );
                sb.AppendFormat( "| {0,-" + larguraMenu + "} |\n", frases [0].Trim() );
                sb.AppendLine( $".{new string( '_', larguraMenu + 2 )}." );

                for ( int i = 1 ; i < frases.Length - 1 ; i++ )
                {
                    sb.AppendFormat( "| {0,-" + larguraMenu + "} |\n", frases [i].Trim() );
                }
                sb.AppendLine( $"|{new string( '_', larguraMenu + 2 )}|" );
                sb.AppendLine();
                sb.AppendLine( frases [frases.Length - 1] );
            }
            Console.WriteLine( sb.ToString() );

        }

        public static int ReturnBiggestString( string [] frases )
        {
            int indiceMaiorPalavra;

            string maiorPalavra = frases [0];

            foreach ( string s in frases )
            {
                if ( s.Length > maiorPalavra.Length )
                    maiorPalavra = s;
            }

            indiceMaiorPalavra = Array.IndexOf( frases, maiorPalavra );

            return indiceMaiorPalavra;
        }

    }
}
