using UnityEngine;
using System.Collections;

public class StageClearManager : MonoBehaviour
{
    [SerializeField] private GameObject jobChoiceUI;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(!GameManager.instance.tutorialClear)
            {
                Time.timeScale = 0;
                jobChoiceUI.SetActive(true);
                other.GetComponent<PlayerAttack>().uiClicking = true;
                UIManager.Instance.inventory.playerProfile.SetActive(false);
                GameManager.instance.tutorialClear = true;
            }
            GameManager.instance.skillPoint++;
            GameManager.instance.statusPoint++;
            GameManager.instance.curLevel++;

            DayManager.instance.sunLight.transform.rotation
                = Quaternion.Euler(DayManager.instance.nightSunRotation);
        }
    }
}
