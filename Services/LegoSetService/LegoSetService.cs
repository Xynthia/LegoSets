using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using LegoSets.Dtos.LegoSets;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LegoSets.Services.LegoSetService
{
    public class LegoSetService : ILegoSetService
    {
        public IMapper _mapper { get; }
        public DataContext _dataContext { get; }

        public LegoSetService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<GetLegoSetDto>>> Add(AddLegoSetDto addLegoSet)
        {
            var serviceResponse = new ServiceResponse<List<GetLegoSetDto>>();

            var legoset = _mapper.Map<LegoSet>(addLegoSet);

            if(legoset != null)
            {
                await _dataContext.LegoSet.AddAsync(legoset);
                await _dataContext.SaveChangesAsync();
            }

            serviceResponse = await GetAll();

            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetLegoSetDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetLegoSetDto>>();

            try
            {
                var legoset = await _dataContext.LegoSet.FirstOrDefaultAsync(l => l.Id == id);

                 _dataContext.LegoSet.Remove(legoset);
                await _dataContext.SaveChangesAsync();

                serviceResponse = await GetAll();
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetLegoSetDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetLegoSetDto>>();

            serviceResponse.Data = await _dataContext.LegoSet.Select(l => _mapper.Map<GetLegoSetDto>(l)).ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetLegoSetDto>> GetLegoSetByID(int id)
        {
            var serviceResponse = new ServiceResponse<GetLegoSetDto>();

            var legoset = await _dataContext.LegoSet.FirstOrDefaultAsync(l => l.Id == id);

            serviceResponse.Data = _mapper.Map<GetLegoSetDto>(legoset);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetLegoSetDto>> Update(int id, UpdateLegoSetDto updateLegoSet)
        {
            var serviceResponse = new ServiceResponse<GetLegoSetDto>();

            try
            {
                var legoset = await _dataContext.LegoSet.FirstOrDefaultAsync(l => l.Id == id);
                legoset = _mapper.Map<UpdateLegoSetDto, LegoSet>(updateLegoSet, legoset);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetLegoSetDto>(legoset);
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<GetLegoSetDto>> UpdateOwned(int id, bool Owned)
        {
            var serviceResponse = new ServiceResponse<GetLegoSetDto>();

            try
            {
                var legoset = await _dataContext.LegoSet.FirstOrDefaultAsync(l => l.Id == id);

                legoset.Owned = Owned;

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetLegoSetDto>(legoset);
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetLegoSetDto>>> readCsvLegoSets()
        {
            var serviceResponse = new ServiceResponse<List<GetLegoSetDto>>();

            var reader = new StreamReader(@"D:\School\leerjaar 3\_Stage\_examen\legosets.csv");
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csv.GetRecords<LegoSet>();

            foreach (var record in records)
            {
                var legoset = _mapper.Map<LegoSet>(record);

                if (legoset != null)
                {
                    await _dataContext.LegoSet.AddAsync(legoset);
                    await _dataContext.SaveChangesAsync();
                }
            }
            
            await GetAll();

            return serviceResponse;
        }

        public async Task<ServiceResponse<IOrderedEnumerable<GetLegoSetDto>>> SortingAndFilter(string sortOrder, string searchString)
        {
            var serviceResponse = new ServiceResponse<IOrderedEnumerable<GetLegoSetDto>>();

            var legosets = await _dataContext.LegoSet.Select(l => _mapper.Map<GetLegoSetDto>(l)).ToListAsync();

            IOrderedEnumerable<GetLegoSetDto> sortedLegosets = null;

            if (!String.IsNullOrEmpty(searchString))
            {
                legosets = legosets.Where(l => l.SetName.Contains(searchString) || l.Variant.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "CAprice_desc":
                    sortedLegosets = legosets.OrderByDescending(l => l.CAPrice);
                    break;
                case "CAprice_asc":
                    sortedLegosets = legosets.OrderBy(l => l.CAPrice);
                    break;
                case "UKprice_desc":
                    sortedLegosets = legosets.OrderByDescending(l => l.UKPrice);
                    break;
                case "UKprice_asc":
                    sortedLegosets = legosets.OrderBy(l => l.UKPrice);
                    break;
                case "USprice_desc":
                    sortedLegosets = legosets.OrderByDescending(l => l.USPrice);
                    break;
                case "USprice_asc":
                    sortedLegosets = legosets.OrderBy(l => l.USPrice);
                    break;
                case "id":
                    sortedLegosets = legosets.OrderByDescending(l => l.Id);
                    break;
                default:
                    sortedLegosets = legosets.OrderByDescending(l => l.SetID);
                    break;
            }

            serviceResponse.Data = sortedLegosets;

            return serviceResponse;
        }
    }
}
