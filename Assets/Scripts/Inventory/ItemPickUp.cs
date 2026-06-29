using UnityEngine;


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
