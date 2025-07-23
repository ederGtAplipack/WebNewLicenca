using LicencaApi.DTOs;
using LicencaApi.Models;

namespace LicencaApi.Helpers
{
    public static class LicencaMapper
    {
        public static LicencaModel ToModel(this CriarLicencaDTO dto)
        {
            return new LicencaModel
            {
                NumLic = dto.NumLic,
                IdCliente = dto.IdCliente,
                TipoLic = dto.TipoLic,
                MacAddress = dto.MacAddress,
                DataLic = dto.DataLic,
                Scade = dto.Scade,
                Attivo = dto.Attivo,
                IdRevenda = dto.IdRevenda,
                SistemaOp = dto.SistemaOp,
                DataAtivacao = dto.DataAtivacao,
                Tipo_Pc = dto.TipoPc,
                Nome_Computador = dto.NomeComputador,
                Software = dto.Software,
                Ip = dto.Ip,
                Processador = dto.Processador,
                //IdLicencaChave = int.TryParse(dto.IdLicencaChave, out var parsedValue) ? parsedValue : 0
                IdLicencaChave = string.IsNullOrEmpty(dto.IdLicencaChave) 
                    ? null 
                    : int.TryParse(dto.IdLicencaChave, out var parsedValue) 
                        ? parsedValue.ToString() 
                        : dto.IdLicencaChave,
                Status = "Pedente Analise" // Definindo um status padrão, pode ser alterado conforme a lógica de negócio

            };
        }

        public static void ApplyUpdate(this LicencaModel model, AtualizarLicencaDTO dto)
        {
            model.TipoLic = dto.TipoLic ?? model.TipoLic;
            model.MacAddress = dto.MacAddress ?? model.MacAddress;
            model.DataLic = dto.DataLic;
            model.Scade = dto.Scade;
            model.Attivo = dto.Attivo;
            model.IdRevenda = dto.IdRevenda;
            model.SistemaOp = dto.SistemaOp ?? model.SistemaOp;
            model.DataAtivacao = dto.DataAtivacao;
            model.Tipo_Pc = dto.TipoPc ?? model.Tipo_Pc;
            model.Nome_Computador = dto.NomeComputador ?? model.Nome_Computador;
            model.Software = dto.Software ?? model.Software;
            model.Ip = dto.Ip ?? model.Ip;
            model.Processador = dto.Processador ?? model.Processador;
            /*model.IdLicencaChave = string.IsNullOrEmpty(dto.IdLicencaChave) 
                ? model.IdLicencaChave 
                : int.TryParse(dto.IdLicencaChave, out var parsedValue) 
                    ? parsedValue 
                    : model.IdLicencaChave;*/
            model.IdLicencaChave = string.IsNullOrEmpty(dto.IdLicencaChave)
                ? model.IdLicencaChave 
                : int.TryParse(dto.IdLicencaChave, out var parsedValue) 
                    ? parsedValue.ToString() 
                    : dto.IdLicencaChave;
        }
    }
}
