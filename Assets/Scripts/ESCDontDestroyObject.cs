using Unity.AppUI.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ESCDontDestroyObject : MonoBehaviour
{
    public static ESCDontDestroyObject instance;

    public InputActionAsset uiInputAction;
    private InputActionMap uiActionMap;

    [SerializeField] private GameObject operate;
    [SerializeField] private GameObject escWindow;
    [SerializeField] private GameObject exitWindow;
    [SerializeField] private GameObject operateButton;
    [SerializeField] private GameObject exitButton;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //오브젝트 파괴 금지
        }
        else
        {
            Destroy(gameObject); //이미 있으면 파괴
        }

        uiActionMap = uiInputAction.FindActionMap("Option");
    }

    private void OnEnable()
    {
        uiActionMap.Enable();
        uiActionMap.FindAction("ESC").performed += OnESC;

    }

    private void OnDisable()
    {
        if (uiActionMap != null)
        {
            uiActionMap.FindAction("ESC").performed -= OnESC;
            uiActionMap.Disable();
        }
    }

    private void OnESC(InputAction.CallbackContext value)
    {
        if (SceneManager.GetActiveScene().name != "MainScene" && !escWindow.activeSelf)
        {
            escWindow.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name != "MainScene" && escWindow.activeSelf)
        {
            escWindow.SetActive(false);
            operate.SetActive(false);
            exitWindow.SetActive(false);
            operateButton.SetActive(true);
            exitButton.SetActive(true);
        }
    }

    public void OperateAppear()
    {
        operate.SetActive(true);
    }
    public void OperateDisAppear()
    {
        operate.SetActive(false);
    }

    public void ExitAppear()
    {
        exitWindow.SetActive(true);
        operateButton?.SetActive(false);
        exitButton.SetActive(false);
    }
    public void ExitDisAppear()
    {
        exitWindow.SetActive(false);
        operateButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
