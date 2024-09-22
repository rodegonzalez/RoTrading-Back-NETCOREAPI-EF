using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IDataTable
    {
        DataTable? GetTest(); 
        Task<DataTable?> GetPositionsAsync();
        Task<DataTable?> GetPositionsSearchAsync(string? searchOptions);
        //Task<DataTable?> GetPositionsSearchAsync(SearchOptions? searchOptions);
    }
}
