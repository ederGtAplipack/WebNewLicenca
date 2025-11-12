namespace LicencaApi.MSSQL.Builders
{
    public class LicencaDispositivoSqlBuilder
    {
        public static string GetAllDevicesQuery()
        {
            // O uso de {{}} é crucial aqui para evitar o System.FormatException, 
            // caso a string seja usada em um contexto de interpolação ($"") ou string.Format() 
            // na camada do Repository.
            const string sql = @"
                SELECT
                    -- Colunas da Tabela Licenca (L)
                    L.numLic,
                    L.IdCliente,
                    L.DataLic,
                    L.IdRevenda,
                    L.MaxDevices,

                    -- COALESCE (Int/String) para evitar System.InvalidCastException:
                    COALESCE(L.idlicencachave, 0) AS idlicencachave,
                    COALESCE(L.TipoLic, '') AS TipoLic,
                    COALESCE(L.MacAddress, '') AS MacAddress,
                    COALESCE(L.SistemaOp, '') AS SistemaOp,
                    COALESCE(L.tipoPc, '') AS tipoPc,
                    COALESCE(L.software, '') AS software,
                    COALESCE(L.ip, '') AS ip,
                    COALESCE(L.processador, '') AS processador,
                    COALESCE(L.nomeComputador, '') AS nomeComputador,
                    
                    -- Colunas de data/status da Licença
                    L.scade AS DataExpiracao,
                    L.attivo AS LicencaAtiva,
                    L.DataAtivacao,
                    L.Status AS StatusLicenca,

                    -- Colunas da Tabela LicencaDispositivo (LD) - Opcionais devido ao LEFT JOIN
                    COALESCE(LD.idDispositivo, 0) AS idDispositivo,
                    COALESCE(LD.DeviceFingerprint, '') AS DeviceFingerprint,
                    COALESCE(LD.DeviceInfo, '{{}}') AS DeviceInfo, -- ⬅️ CORREÇÃO: Escapando chaves literais para o JSON vazio
                    COALESCE(LD.IsActive, 0) AS IsActive,
                    
                    -- Datas (Devem ser DateTime? na DTO)
                    LD.ActivatedAt,
                    LD.LastSeenAt
                FROM
                    licenca L
                LEFT JOIN
                    licencadispositivo LD ON L.numLic = LD.numLic
                ORDER BY
                    L.numLic, LD.idDispositivo;
            ";

            return sql;
        }
    }
}
