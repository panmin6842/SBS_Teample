using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("AudioSource")]
    public AudioSource titleUiAudioSource;
    public AudioSource characterChoiceUiAudioSource;
    public AudioSource playerAudioSource;
    public AudioSource bgmAudioSource;

    [Header("UI 관련 사운드")]
    public AudioClip buttonClickSoundClip;
    public AudioClip buttonPointerEnterSoundClip;

    [Header("플레이어 관련 사운드")]
    public AudioClip buffGetSoundClip;
    public AudioClip hpGetSoundClip;
    public AudioClip swordBasicAttackSoundClip;
    public AudioClip stampBasicAttackSoundClip;
    public AudioClip bowBasicAttackSoundClip;
    public AudioClip meteoAttackSoundClip;
    public AudioClip mpPowerAttackSoundClip;
    public AudioClip hitVoiceSoundClip;
    public AudioClip skillVoiceSoundClip;
    public AudioClip[] footstepSoundClips = new AudioClip[5];

    [Header("비지엠 관련 사운드")]
    public AudioClip villageBGM;
    public AudioClip tutorialDungeonBGM;

    private float clampedVolume;

    static public SoundManager instance;

    private void Awake()
    {
        instance = this;
        clampedVolume = 0.5f;
    }

    public void SetSFXVolume(float volume)
    {
        clampedVolume = Mathf.Clamp01(volume); //0~1사이인지 확인
        titleUiAudioSource.volume = clampedVolume;
    }

    public void CharacterChoiceSceneSFXVolume()
    {
        characterChoiceUiAudioSource.volume = clampedVolume;
    }

    public void MainSceneSFXVolume()
    {
        playerAudioSource.volume = clampedVolume;
    }
}
