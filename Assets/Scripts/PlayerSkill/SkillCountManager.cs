using TMPro;
using UnityEngine;

public class SkillCountManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillPointText;

    private void OnEnable()
    {
        skillPointText.text = "SkillPoint : " + GameManager.instance.skillPoint.ToString();
    }
}
