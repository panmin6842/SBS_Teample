using UnityEngine;

public class InventoryButtonManager : MonoBehaviour
{
    [SerializeField] GameObject equipmentInventory;
    [SerializeField] GameObject accessoryInventory;
    [SerializeField] GameObject etcInventory;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EquipmentAppear()
    {
        equipmentInventory.SetActive(true);
        accessoryInventory.SetActive(false);
        etcInventory.SetActive(false);
    }

    public void AccessoryAppear()
    {
        etcInventory.SetActive(false);
        equipmentInventory.SetActive(false);
        accessoryInventory.SetActive(true);
    }

    public void ETCAppear()
    {
        etcInventory.SetActive(true);
        equipmentInventory.SetActive(false);
        accessoryInventory.SetActive(false);
    }
}
