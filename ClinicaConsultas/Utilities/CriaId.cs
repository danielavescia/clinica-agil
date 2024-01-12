using ClinicaConsultas.Models.Domain;

namespace ClinicaConsultas.Utilities
{
    public class CriaId
    {
        public static int AtribuiId( List<Paciente> pacientesCadastrados )
        {
            int ultimaId = pacientesCadastrados.Count;

                if ( pacientesCadastrados == null || pacientesCadastrados.Count == 0 )
                {
                    return 1;
                }

                return ultimaId + 1;
        }
    }
}
