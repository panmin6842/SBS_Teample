using UnityEngine;

public class SealStoneManager : MonoBehaviour
{
    [SerializeField] private float curHp;
    [SerializeField] private float maxHp;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite[] sprites;

    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if(curHp <= 0)
        {
            spriteRenderer.sprite = sprites[2];
            this.enabled = false;
            boxCollider.enabled = false;
            //Destroy(gameObject);
        }

        if(curHp <= maxHp * 0.8f && curHp >= maxHp * 0.4f)
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if(curHp <= maxHp * 0.4f && curHp > 0)
        {
            spriteRenderer.sprite = sprites[1];
        }
    }
    public void Damage(float damage)
    {
        curHp -= damage;
    }
}
