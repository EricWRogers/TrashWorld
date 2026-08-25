using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
   [SerializeField] public Image itemImage;

   
    private void Start()
    {
        itemImage.enabled = false;
    }

    public void AddItem(Item item)
    {
        itemImage.sprite = item.itemIcon;
        itemImage.enabled = true;
    }

    public void ClearSlot()
    {
        itemImage.sprite = null;
        itemImage.enabled = false;
    }
}

