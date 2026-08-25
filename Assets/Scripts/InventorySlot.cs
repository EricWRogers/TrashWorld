using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
   [SerializeField] public Image itemImage;

   public void AddItem(Item item)
   {
    itemImage.sprite = item.itemIcon;
    itemImage.enabled = true;
   }

   public void ClearSlot(){
    itemImage.sprite = null;
    itemImage.enabled = false;
   }
}
