using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DungeonEntryManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject ui;
    [SerializeField] private Transform mapSpawnPos;
    [SerializeField] private int dungeonNumber;
    private GameObject player;
    private PlayerProfile playerProfile;
    private Image backGroundImage;

    private void OnEnable()
    {
        backGroundImage = GetComponent<Image>();
        if (GameManager.instance.possibleDungeon[dungeonNumber - 1])
        {
            backGroundImage.color = Color.white;
        }
        else
        {
            backGroundImage.color = Color.gray;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.instance.possibleDungeon[dungeonNumber - 1])
        {
            Time.timeScale = 1f;
            Instantiate(map, mapSpawnPos.position, Quaternion.identity);
            GameManager.instance.curDungeonNumber = dungeonNumber;
            ui.SetActive(false);
            Invoke("PlayerMove", 0.5f);
        }
    }

    private void PlayerMove()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerProfile = player.GetComponent<PlayerProfile>();
        Vector3 spawnPos = GameObject.FindGameObjectWithTag("DungeonEntry").GetComponent<Transform>().position;
        player.transform.position = spawnPos;
        UIManager.Instance.inventory.currentUI = UIType.None;
        UIManager.Instance.inventory.playerProfile.SetActive(true);
        UIManager.Instance.inventory.playerAttack.uiClicking = false;
        playerProfile.AnimationReset();
    }
}
