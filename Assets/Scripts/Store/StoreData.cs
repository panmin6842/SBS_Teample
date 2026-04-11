using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopData", menuName = "Shop/Shop Data")]
public class StoreData : ScriptableObject
{
    [System.Serializable]
    public class ItemPool
    {
        public string gradeName; //등급 이름
        public List<Item> items; //해당 등급에 속한 아이템들
        [Range(0, 100)]
        public float dropChance; //뽑힐 확률
    }

    public List<ItemPool> itemPools;
}
