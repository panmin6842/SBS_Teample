using UnityEngine;

public class InventoryButtonManager : MonoBehaviour
{
    [SerializeField] GameObject equipmentInventory;
    [SerializeField] GameObject accessoryInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EquipmentAppear()
    {
        equipmentInventory.SetActive(true);
        accessoryInventory.SetActive(false);
    }

    public void AccessoryAppear()
    {
        equipmentInventory.SetActive(false);
        accessoryInventory.SetActive(true);
    }
}
