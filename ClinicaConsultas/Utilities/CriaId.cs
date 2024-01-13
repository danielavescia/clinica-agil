using ClinicaConsultas.Models.Domain;
using System.Collections;

namespace ClinicaConsultas.Utilities
{
    public class CriaId
    {
        public static int IdGenerator( IList list )
        {
            int ultimaId = list.Count;

                if ( list == null || list.Count == 0 )
                {
                    return 1;
                }

                return ultimaId + 1;
        }
    }
}
