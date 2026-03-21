using UnityEngine;

public class InfoUIManager : MonoBehaviour
{
    [SerializeField] private GameObject skillUI;
    [SerializeField] private GameObject statusUI;

    public void SkillUIClick()
    {
        skillUI.SetActive(true);
        statusUI.SetActive(false);
    }

    public void StatusUIClick()
    {
        skillUI.SetActive(false);
        statusUI.SetActive(true);
    }
}
