namespace LicencaApi.MSSQL.Builders
{
    public class LicencaSqlBuilder
    {        
        public static string GetAllWithDetailsSql()
        {
            return @"
                SELECT
                -- Licença principal
                l.numLic AS NumLic,
                l.IdCliente AS IdCliente,
                l.IdContrato AS IdContrato,
                a.NomeFantasia AS NomeCliente,
                l.TipoLic AS TipoLic,
                l.MacAddress AS MacAddress,
                l.DataLic AS DataLic,
                l.Scade AS Scade,
                l.Attivo AS Attivo,
                l.IdRevenda AS IdRevenda_Licenca,
                l.SistemaOp AS SistemaOp,
                l.DataAtivacao AS DataAtivacao,
                l.TipoPc AS TipoPc,
                l.NomeComputador AS NomeComputador,
                l.Software AS Software,
                l.Ip AS Ip,
                l.Processador AS Processador,
                l.Status AS StatusLicenca,
                l.IdLicencaChave AS IdLicencaChave,
                l.MaxDevices AS MaxDevices,

                -- Chave da Licença
                l2.Chave AS ChaveLicenca,
                l2.DataInser AS DataGeracaoChave,
                l2.Status AS StatusChave,
                l2.IdSoftware AS IdSoftware,
                s.nSoftware AS NomeSoftware,

                -- Cliente (Anagrafica)
                a.RazaoSocial AS RazaoSocialCliente,
                a.NomeFantasia AS NomeFantasiaCliente,
                a.CNPJ AS CNPJCliente,
                a.Email AS EmailCliente,

                -- Contrato
                c.IdContrato AS IdContrato,
                c.DataInicio AS DataInicioContrato,
                c.DataFim AS DataFimContrato,
                c.StatusContrato AS StatusContrato,
                c.Plano AS PlanoContrato,
                c.QtdLicencas AS QtdLicencasContrato,
                c.PagamentoEmDia AS PagamentoEmDiaContrato,

                -- Revenda
                r.RazaoSocial AS RazaoSocialRevenda,
                r.idRevenda AS IdRevenda_Revenda,
                ru.idUser AS UsuarioRevenda,

                -- Dispositivo
                l3.DeviceFingerprint AS DeviceFingerprint,
                l3.DeviceInfo AS DeviceInfo,
                l3.ActivatedAt AS DeviceActivatedAt,
                l3.LastSeenAt AS DeviceLastSeenAt,
                l3.IsActive AS DeviceIsActive

            FROM licencas.licenca l
            INNER JOIN licencas.anagrafica a ON l.IdCliente = a.idanagrafica
            INNER JOIN licencas.licencaschave l2 ON l.idlicencachave = l2.idLicencaChave
            LEFT JOIN licencas.licencadispositivo l3 ON l3.numLic = l.numLic
            LEFT JOIN licencas.contrato c ON c.idCliente = a.idanagrafica
            LEFT JOIN licencas.revenda r ON r.idrevenda = l.IdRevenda
            LEFT JOIN licencas.revenda_user ru ON ru.idRevenda = r.idrevenda
            INNER JOIN licencas.software s ON l2.idSoftware = s.idSoftware";
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
