using Microsoft.EntityFrameworkCore;
using GeneralStore.Models;
using GeneralStore.Interfaces;

namespace GeneralStore.Repositories
{
public class ItemRepository : _Test_IItemRepository
{
    private Db _context;

    public ItemRepository(Db context)
    {
        _context = context;
    }

    public async Task<List<Item>> GetAllItemsAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<Item> GetItemByIdAsync(int id)
    {
        return await _context.Items.FindAsync(id);
    }

    public async Task<Item> AddItemAsync(Item item)
    {
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<Item> UpdateItemAsync(Item updateItem)
    {
        var item = await _context.Items.FindAsync(updateItem.Id);
        if (item == null) return null;
        item.ItemName = updateItem.ItemName;
        item.ItemValue = updateItem.ItemValue;
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task DeleteItemAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item != null)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
}

