using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileSkill : MonoBehaviour
{
    //[SerializeField] private string tag;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    [SerializeField] private SkillUISlot slot;
    public float coolTime;
    public int choiceNumber; //선택한 스킬

    private SkillPlay skillPlay;


    private void Start()
    {
        skillPlay = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillPlay>();
    }

    private void OnEnable()
    {
        if (slot != null)
        {
            GetSkillItem();
        }

        //구분하는거 바꿀 수 있으면 바꾸자
        if (skillPlay != null)
        {
            skillPlay.SkillNumberSetting();
            //skillPlay.SwordPassiveSkill();
            //skillPlay.BowPassiveSkill();
            skillPlay.StampPassiveSkill();
        }
    }

    public void GetSkillItem()
    {
        if (slot.SkillItem != null)
        {
            itemImage.sprite = slot.SkillItem.Image;
            itemText.text = slot.SkillItem.ItemName;
            coolTime = slot.SkillItem.CoolTime;
            choiceNumber = slot.SkillItem.Number;

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
