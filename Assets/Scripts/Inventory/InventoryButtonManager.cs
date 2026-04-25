using UnityEngine;

public class InventoryButtonManager : MonoBehaviour
{
    [SerializeField] GameObject equipmentInventory;
    [SerializeField] GameObject accessoryInventory;
    [SerializeField] GameObject etcInventory;
    [SerializeField] GameObject artFactInventory;

    [SerializeField] GameObject equipmentBase;
    [SerializeField] GameObject artiFactBase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void EquipmentAppear()
    {
        equipmentInventory.SetActive(true);
        accessoryInventory.SetActive(false);
        etcInventory.SetActive(false);
        artFactInventory.SetActive(false);
    }

    public void AccessoryAppear()
    {
        etcInventory.SetActive(false);
        equipmentInventory.SetActive(false);
        accessoryInventory.SetActive(true);
        artFactInventory.SetActive(false);
    }

    public void ETCAppear()
    {
        etcInventory.SetActive(true);
        equipmentInventory.SetActive(false);
        accessoryInventory.SetActive(false);
        artFactInventory.SetActive(false);
    }

    public void ArtFactAppear()
    {
        etcInventory.SetActive(false);
        equipmentInventory.SetActive(false);
        accessoryInventory.SetActive(false);
        artFactInventory.SetActive(true);
    }

    public void EquipmentBaseAppear()
    {
        equipmentBase.SetActive(true);
        artiFactBase.SetActive(false);
    }

    public void ArtiFactBaseAppear()
    {
        equipmentBase.SetActive(false);
        artiFactBase.SetActive(true);
    }
}
