namespace LicencaApi.MSSQL.Builders
{
    public class LicencaSqlBuilder
    {        
        public static string GetAllWithDetailsSql()
        {
            return @"
                SELECT 
                    l.NumLic,
                    l.IdCliente,
                    a.NomeFantasia AS NomeCliente,  -- Join com Anagrafica
                    l.TipoLic,
                    l.MacAddress,
                    l.DataLic,
                    l.Scade,
                    l.Attivo,
                    l.IdRevenda,
                    l.SistemaOp,
                    l.DataAtivacao,
                    l.TipoPc,
                    l.NomeComputador,
                    l.Software,
                    l.Ip,
                    l.Processador,
                    l.Status,
                    l.IdLicencaChave,
                    lc.Chave AS ChaveLicenca  -- Join com LicencasChave
                FROM 
                    Licenca l
                LEFT JOIN 
                    Anagrafica a ON l.IdCliente = a.IdAnagrafica
                LEFT JOIN 
                    LicencasChave lc ON l.IdLicencaChave = lc.IdLicencaChave";
        }
        public static string GetByIdSql()
        {
            return @"
                SELECT 
                    l.NumLic,
                    l.IdCliente,
                    a.NomeFantasia AS NomeCliente,  -- Join com Anagrafica
                    l.TipoLic,
                    l.MacAddress,
                    l.DataLic,
                    l.Scade,
                    l.Attivo,
                    l.IdRevenda,
                    l.SistemaOp,
                    l.DataAtivacao,
                    l.TipoPc,
                    l.NomeComputador,
                    l.Software,
                    l.Ip,
                    l.Processador,
                    l.Status,
                    l.IdLicencaChave,
                    lc.Chave AS ChaveLicenca  -- Join com LicencasChave
                FROM 
                    Licenca l
                LEFT JOIN 
                    Anagrafica a ON l.IdCliente = a.IdAnagrafica
                LEFT JOIN 
                    LicencasChave lc ON l.IdLicencaChave = lc.IdLicencaChave
                WHERE 
                    l.NumLic = @NumLic";
        }
        public static string GetAtivasSql()
        {
            return @"
                SELECT 
                    l.NumLic,
                    l.IdCliente,
                    a.NomeFantasia AS NomeCliente,  -- Join com Anagrafica
                    l.TipoLic,
                    l.MacAddress,
                    l.DataLic,
                    l.Scade,
                    l.Attivo,
                    l.IdRevenda,
                    l.SistemaOp,
                    l.DataAtivacao,
                    l.TipoPc,
                    l.NomeComputador,
                    l.Software,
                    l.Ip,
                    l.Processador,
                    l.Status,
                    l.IdLicencaChave,
                    lc.Chave AS ChaveLicenca  -- Join com LicencasChave
                FROM 
                    Licenca l
                LEFT JOIN 
                    Anagrafica a ON l.IdCliente = a.IdAnagrafica
                LEFT JOIN 
                    LicencasChave lc ON l.IdLicencaChave = lc.IdLicencaChave
                WHERE 
                    l.Attivo = 1"; // Considerando que 'Ativas' significa Attivo = true (1)
        }
        public static string CreateSql()
        {
            return @"
                INSERT INTO Licenca 
                (IdCliente, TipoLic, MacAddress, DataLic, Scade, Attivo, IdRevenda, SistemaOp, DataAtivacao, TipoPc, NomeComputador, Software, Ip, Processador, Status, IdLicencaChave) 
                VALUES 
                (@IdCliente, @TipoLic, @MacAddress, @DataLic, @Scade, @Attivo, @IdRevenda, @SistemaOp, @DataAtivacao, @TipoPc, @NomeComputador, @Software, @Ip, @Processador, @Status, @IdLicencaChave);
                SELECT CAST(SCOPE_IDENTITY() as int)";
        }
        public static string UpdateSql()
        {
            return @"
                UPDATE Licenca SET 
                    IdCliente = @IdCliente,
                    TipoLic = @TipoLic,
                    MacAddress = @MacAddress,
                    DataLic = @DataLic,
                    Scade = @Scade,
                    Attivo = @Attivo,
                    IdRevenda = @IdRevenda,
                    SistemaOp = @SistemaOp,
                    DataAtivacao = @DataAtivacao,
                    TipoPc = @TipoPc,
                    NomeComputador = @NomeComputador,
                    Software = @Software,
                    Ip = @Ip,
                    Processador = @Processador,
                    Status = @Status,
                    IdLicencaChave = @IdLicencaChave
                WHERE 
                    NumLic = @NumLic";
        }
        public static string DeactivateSql()
        {
            return @"
                UPDATE Licenca SET 
                    Attivo = 0
                WHERE 
                    NumLic = @NumLic";
        }
        public static string DeleteSql()
        {
            return @"
                DELETE FROM Licenca 
                WHERE 
                    NumLic = @NumLic";
        }
    }
}
