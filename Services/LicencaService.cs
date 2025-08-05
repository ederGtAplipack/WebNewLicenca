using AutoMapper;
using LicencaApi.Data;
using LicencaApi.DTOs;
using LicencaApi.Interfaces;
using LicencaApi.Models;
using Microsoft.EntityFrameworkCore;

/*A classe LicencaService atua como intermediária entre a camada de repositório (que interage com o banco de dados) e a camada de apresentação (que consome os DTOs). 
 * Ela encapsula a lógica de negócios, garantindo que os dados sejam manipulados corretamente antes de serem enviados ou recebidos pela aplicação.*/
namespace LicencaApi.Services
{
    public class LicencaService : ILicencaService
    {
        private readonly ILicencaRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<LicencaService> _logger;
        private readonly LicencaDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public LicencaService(ILicencaRepository repository, IMapper mapper, ILogger<LicencaService> logger, LicencaDbContext context, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LicencaModel>> BuscarTodasAsync()
        {
            var licencas = await _repository.BuscarTodasAsync();
            return licencas.Select(l => _mapper.Map<LicencaModel>(l));
        }

        public async Task<LicencaModel?> BuscarPorIdAsync(int id)
        {
            return await _context.Licenca.FindAsync(id);

            /*var licenca = await _repository.BuscarPorIdAsync(id);
            return licenca == null ? null : _mapper.Map<LicencaModel>(licenca);*/
        }

        /*Esse método tem a responsabilidade de criar (inserir) uma nova licença no banco de dados a partir dos dados recebidos (DTO).*/
        public async Task<LicencaModel> CriarAsync(CriarLicencaDTO dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var licenca = _mapper.Map<LicencaModel>(dto);

            await _unitOfWork.Licencas.CriarAsync(licenca);
            await _unitOfWork.CompleteAsync();

            return licenca;
            /*await _repository.CriarAsync(licenca);
            await _repository.SalvarAsync();
            return _mapper.Map<LicencaDTO>(licenca);*/
        }

        public async Task<bool> AtualizarAsync(int id, AtualizarLicencaDTO dto)
        {
            var licenca = await _unitOfWork.Licencas.BuscarPorIdAsync(id);
            if (licenca == null)
                return false;                
            try
            {
                _mapper.Map(dto, licenca);
                await _unitOfWork.Licencas.AtualizarAsync(licenca);
                await _unitOfWork.CompleteAsync();
                return true;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao mapear DTO para LicencaModel");
                throw;
            }
        }

        public async Task<IEnumerable<LicencaModel>> BuscarAtivasAsync()
        {
            return await _context.Licenca
                .Where(l => l.Attivo)
                .ToListAsync();

            //throw new NotImplementedException();
        }
    }
}
