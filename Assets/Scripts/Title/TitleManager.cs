using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject option;
    public void GameStart()
    {
        SoundManager.instance.titleUiAudioSource.PlayOneShot(SoundManager.instance.buttonClickSoundClip);
        Invoke("CharacterChoiceSceneMove", 0.5f);
    }

    private void CharacterChoiceSceneMove()
    {
        SceneManager.LoadScene("CharacterChoiceScene");
    }

    public void OptionAppear()
    {
        SoundManager.instance.titleUiAudioSource.PlayOneShot(SoundManager.instance.buttonClickSoundClip);
        option.SetActive(true);
    }
    public void OptionDisAppear()
    {
        option.SetActive(false);
    }

    public void ButtonPointerEnter()
    {
        SoundManager.instance.titleUiAudioSource.PlayOneShot(SoundManager.instance.buttonPointerEnterSoundClip);
    }
}
