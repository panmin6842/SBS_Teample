using UnityEngine;
using UnityEngine.UI;

public class PingSelectButton : MonoBehaviour
{
    [SerializeField] Image pingImage;
    [SerializeField] Image curPingImage;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void OnButtonClick()
    {
        if (curPingImage != null && pingImage != null)
        {
            curPingImage.sprite = pingImage.sprite;
        }

        GetComponentInParent<MinimapStage>().isPingUiOn = false;
    }
}
