using GeneralStore.Models;

namespace GeneralStore.Interfaces
{
    public interface IDataTable
    {
        Task<DataTable?> GetPositionsAsync();
    }
}
