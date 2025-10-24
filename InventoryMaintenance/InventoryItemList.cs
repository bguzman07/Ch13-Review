using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace InventoryMaintenance
{
    public class InventoryItemList
    {
        private List<InventoryItem> items;


        public static InventoryItemList operator +(InventoryItem item, InventoryItemList list)
        {
            list.items.Add(item);
            return list;
        }
        public static InventoryItemList operator -(InventoryItem item, InventoryItemList list)
        {
            list.items.Remove(item);
            return list;
        }

        public InventoryItemList()
        {
            items = new List<InventoryItem>();
        }

        public int Count => items.Count;

        public InventoryItem this[int i]
        {
            get { return items[i]; }
            set { items[i] = value; }
        }

        public void Add(int itemNo, string description, decimal price)
        {
            InventoryItem i = new(itemNo, description, price);
            items.Add(i);
        }

        public void Remove(InventoryItem item) => items.Remove(item);

        public void Fill() => items = InventoryDB.GetItems();

        public void Save() => InventoryDB.SaveItems(items);
    }
}
