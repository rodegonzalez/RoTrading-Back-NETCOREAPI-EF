using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IDataTable
    {
        DataTable? GetTest(); 
        Task<DataTable?> GetPositionsAsync();
    }
}
