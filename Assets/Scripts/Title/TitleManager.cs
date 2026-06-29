using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject option;
    public void GameStart()
    {
        SceneManager.LoadScene("CharacterChoiceScene");
    }

    public void OptionAppear()
    {
        option.SetActive(true);
    }
    public void OptionDisAppear()
    {
        option.SetActive(false);
    }
}
