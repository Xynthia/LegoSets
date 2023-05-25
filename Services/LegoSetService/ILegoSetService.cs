using LegoSets.Dtos.LegoSets;

namespace LegoSets.Services.LegoSetService
{
    public interface ILegoSetService
    {
        Task<ServiceResponse<List<GetLegoSetDto>>> GetAll();
        Task<ServiceResponse<GetLegoSetDto>> GetLegoSetByID(int id);
        Task<ServiceResponse<List<GetLegoSetDto>>> Add(AddLegoSetDto addLegoSet);
        Task<ServiceResponse<GetLegoSetDto>> Update(int id ,UpdateLegoSetDto updateLegoSet);
        Task<ServiceResponse<GetLegoSetDto>> UpdateOwned(int id, bool Owned);
        Task<ServiceResponse<List<GetLegoSetDto>>> Delete(int id);

        Task<ServiceResponse<IOrderedEnumerable<GetLegoSetDto>>> SortingAndFilter(string sortOrder, string searchString);

        Task<ServiceResponse<List<GetLegoSetDto>>> readCsvLegoSets();
    }
}
