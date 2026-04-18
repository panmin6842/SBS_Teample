using TMPro;
using UnityEngine;

public class InfoUIManager : MonoBehaviour
{
    [SerializeField] private GameObject skillUI;
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject playerUI;
    public void SkillUIClick()
    {
        skillUI.SetActive(true);
        statusUI.SetActive(false);
        playerUI.SetActive(false);
    }

    public void StatusUIClick()
    {
        skillUI.SetActive(false);
        statusUI.SetActive(true);
        playerUI.SetActive(false);
    }

    public void PlayerUIClick()
    {
        PlayerProfile playerProfile = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerProfile>();
        skillUI.SetActive(false);
        statusUI.SetActive(false);
        playerUI.SetActive(true);
        playerProfile.StateTestText();
    }
}
