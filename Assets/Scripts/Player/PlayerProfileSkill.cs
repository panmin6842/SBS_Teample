using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileSkill : MonoBehaviour
{
    //[SerializeField] private string tag;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    [SerializeField] private SkillUISlot slot;

    private void Start()
    {

    }

    private void OnEnable()
    {
        if (slot != null)
        {
            GetSkillItem();
        }
    }

    public void GetSkillItem()
    {
        if (slot.SkillItem != null)
        {
            itemImage.sprite = slot.SkillItem.Image;
            itemText.text = slot.SkillItem.ItemName;

            SetColor(1);
        }
    }

    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
}
