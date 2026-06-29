using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle windowModeToggle;

    private List<Resolution> resolutions = new List<Resolution>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitResolution();
        //이벤트 연결
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
        windowModeToggle.onValueChanged.AddListener(ToggleWindowMode);
    }
    private void InitResolution()
    {
        resolutions.Clear();
        foreach (Resolution res in Screen.resolutions)
        {
            if (res.width >= 1280 && res.height >= 720)
            {
                resolutions.Add(res);
            }
        }

        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResIndex = 0;
        for(int i = 0; i < resolutions.Count; i++)
        {
            //드롭다운에 표시될 텍스트
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // 현재 시스템 해상도와 일치하는 항목을 찾아서 기본값으로 설정
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options); //옵션 추가
        resolutionDropdown.value = currentResIndex; //현재 해상도로 선택 바 표시
        resolutionDropdown.RefreshShownValue(); //새로고침
    }
    private void ChangeResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode);
    }

    private void ToggleWindowMode(bool isWindowed)
    {
        if(isWindowed)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }
}
