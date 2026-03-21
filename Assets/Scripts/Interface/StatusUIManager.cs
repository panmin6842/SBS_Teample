using TMPro;
using UnityEngine;

public class StatusUIManager : MonoBehaviour
{
    private PlayerProfile playerProfile;

    [Header("프로필창 텍스트")]
    [SerializeField] private TextMeshProUGUI nickName;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI jobText;

    [Header("스테이터스창 텍스트")]
    [SerializeField] private TextMeshProUGUI hpPointText;
    [SerializeField] private TextMeshProUGUI atkPointText;
    [SerializeField] private TextMeshProUGUI defPointText;
    private void OnEnable()
    {
        playerProfile = GameObject.Find("Player").GetComponent<PlayerProfile>();

        nickName.text = GameManager.instance.nickName;
        levelText.text = GameManager.instance.level.ToString();
        jobText.text = GameManager.instance.job.ToString();

        hpPointText.text = GameManager.instance.hpPoint.ToString();
        atkPointText.text = GameManager.instance.atkPoint.ToString();
        defPointText.text = GameManager.instance.defPoint.ToString();
    }

    public void HpPointUp()
    {
        int hpPoint = playerProfile.HpPointUp(1);
        GameManager.instance.hpPoint = hpPoint;
        playerProfile.SetMaxHp(hpPoint, 0); //장비 착용했을 때 효과는 나중에 넣기 지금은 0
        hpPointText.text = GameManager.instance.hpPoint.ToString();
    }
    public void ATKPointUp()
    {
        int atkPoint = playerProfile.ATKPointUp(1);
        GameManager.instance.atkPoint = atkPoint;
        playerProfile.SetMaxATK(atkPoint, 0); //장비 착용했을 때 효과는 나중에 넣기 지금은 0
        atkPointText.text = GameManager.instance.atkPoint.ToString();
    }
    public void DEFPointUp()
    {
        float defPoint = playerProfile.DEFPointUp(0.5f);
        GameManager.instance.defPoint = defPoint;
        playerProfile.SetMaxDEF(defPoint, 0); //장비 착용했을 때 효과는 나중에 넣기 지금은 0
        defPointText.text = GameManager.instance.defPoint.ToString();
    }
}
