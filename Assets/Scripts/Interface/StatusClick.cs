using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatusClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI statusNameText;
    [SerializeField] private TextMeshProUGUI statusExplainText;

    [SerializeField] private string statusName;
    [SerializeField] private string statusExplain;
    public void OnPointerClick(PointerEventData eventData)
    {
        statusNameText.text = statusName;
        statusExplainText.text = statusExplain;
    }
}
