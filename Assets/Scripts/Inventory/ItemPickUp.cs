using UnityEngine;

/// <summary>
/// Item ｋ 怨듦 由ы⑹ 而댄щ몃 異媛怨 몄ㅽ곗 댄 
/// </summary>

public class ItemPickUp : MonoBehaviour
{
    public bool canPickUp = false;
    private void Start()
    {
        Invoke("EnablePickUp", 1);
    }

    private void EnablePickUp()
    {
        canPickUp = true;
    }
    [Header("해당 오브젝트에 할당되는 아이템")]
    [SerializeField] private Item item;
    /// <summary>
    /// 몄 媛ν 媛泥닿 媛吏怨  댄
    /// /// </summary>
    /// <value></value>
    public Item Item
    {
        get
        {
            return item;
        }
    }

    [Header("대 ㅻ�몄 몄 , 蹂댁以 몃耳댄곗 ")]
    [SerializeField] private float indicatorHeight;

    public float IndicatorHeight
    {
        get
        {
            return indicatorHeight;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Place"))
        {
            GameObject shadow = transform.GetChild(1).gameObject;
            if (shadow != null)
            {
                shadow.SetActive(true);
            }
        }
    }
}
