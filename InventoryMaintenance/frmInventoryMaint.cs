using System.Threading.Channels;

namespace InventoryMaintenance
{

    public partial class frmInventoryMaint : Form
    {
        public frmInventoryMaint()
        {
            InitializeComponent();
        }

        public delegate void ChangeHandler(InventoryItemList list);

        public event ChangeHandler Changed = null!;

        private InventoryItemList items = new();

        public void OnChanged()
        {
            Changed?.Invoke(items);
        }


        private void frmInventoryMaint_Load(object sender, EventArgs e)
        {
            items.Fill();
            FillItemListBox();
        }

        private void FillItemListBox()
        {
            InventoryItem item;
            lstItems.Items.Clear();
            for (int i = 0; i < items.Count; i++)
            {
                item = items[i];
                lstItems.Items.Add(item.GetDisplayText());
            }
            OnChanged();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewItem newItemForm = new();
            InventoryItem item = newItemForm.GetNewItem();
            if (item != null)
            {
                items = item+items;
                items.Save();
                FillItemListBox();
            }
            OnChanged();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int i = lstItems.SelectedIndex;

            if (i == -1)
            {
                MessageBox.Show("Please select an item to delete.", "No item selected");
            }
            else
            {
                InventoryItem item = items[i];

                string message = $"Are you sure you want to delete {item.Description}?";
                DialogResult result =
                    MessageBox.Show(message, "Confirm Delete",
                    MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    items.Remove(item);
                    items.Save();
                    FillItemListBox();
                }
            }
            OnChanged();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}