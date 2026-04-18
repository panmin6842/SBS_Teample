using UnityEngine;
using System.Collections;

public class StageClearManager : MonoBehaviour
{
    [SerializeField] private GameObject jobChoiceUI;
    [SerializeField] private GameObject tutorialMap;
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
                GameManager.instance.possibleDungeon[0] = true;
                GameManager.instance.tutorialClear = true;
                tutorialMap.SetActive(false);
            }
            GameManager.instance.possibleDungeon[GameManager.instance.curDungeonNumber] = true;
            GameManager.instance.statusPoint++;
            GameManager.instance.curLevel++;

            DayManager.instance.sunLight.transform.rotation
                = Quaternion.Euler(DayManager.instance.nightSunRotation);
            DayManager.instance.curDay = Day.night;
        }
    }
}
