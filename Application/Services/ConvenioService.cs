using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Application.Services
{
    public class ConvenioService : IConvenioService
    {
        private readonly IMapper _mapper;
        private readonly IConvenioRepository _convenioRepository;

        public ConvenioService(IMapper mapper, IConvenioRepository convenioRepository)
        {
            _mapper = mapper;
            _convenioRepository = convenioRepository;
        }
        public async Task<Convenio> Create(Convenio convenioDTO)
        {
            var convenio = _mapper.Map<Convenio>(convenioDTO);

            return await _convenioRepository.Create(convenio);
        }

        public async Task<Convenio> Get(long id)
        {
            return await _convenioRepository.Get(id);
        }

        public async Task<List<Convenio>> Get()
        {
            return await _convenioRepository.Get();
        }

        public async Task Remove(long id)
        {
            await _convenioRepository.Remove(id);
        }

        public async Task<List<Convenio>> SearchByNome(string nome)
        {
            return await _convenioRepository.SearchByNome(nome);
        }

        public async Task<Convenio> Update(Convenio convenioDto)
        {
            var convenio = _mapper.Map<Convenio>(convenioDto);

            return await _convenioRepository.Update(convenio);
        }
    }
}
