using GeneralStore.Models;

namespace GeneralStore.Interfaces 
{
public interface _Test_IItemRepository
{
    public Task<List<Item>> GetAllItemsAsync();
    public Task<Item> GetItemByIdAsync(int id);
    public Task<Item> AddItemAsync(Item item);
    public Task<Item> UpdateItemAsync(Item item);
    public Task DeleteItemAsync(int id);
}
}