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
    [SerializeField] private TextMeshProUGUI criticalPointText;
    [SerializeField] private GameObject AttentionWindow;
    private int upStateCheckCount; //1 = hp 2 = atk 3 = def 4 = critical
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

    public void AttentionWindowDisAppear()
    {
        upStateCheckCount = 0;
        AttentionWindow.SetActive(false);
    }

    public void UpYes()
    {
        if (upStateCheckCount == 1)
        {
            HpPointUp();
        }
        else if (upStateCheckCount == 2)
        {
            ATKPointUp();
        }
        else if (upStateCheckCount == 3)
        {
            DEFPointUp();
        }
        else if (upStateCheckCount == 4)
        {
            CriticalPointUp();
        }
        upStateCheckCount = 0;
        AttentionWindow.SetActive(false);
    }

    public void HpPointCheck()
    {
        upStateCheckCount = 1;
        AttentionWindow.SetActive(true);
    }
    public void ATKPointCheck()
    {
        upStateCheckCount = 2;
        AttentionWindow.SetActive(true);
    }
    public void DEFPointCheck()
    {
        upStateCheckCount = 3;
        AttentionWindow.SetActive(true);
    }
    public void CriticalPointCheck()
    {
        upStateCheckCount = 4;
        AttentionWindow.SetActive(true);
    }

    private void HpPointUp()
    {
        int hpPoint = playerProfile.HpPointUp(1);
        GameManager.instance.hpPoint = hpPoint;
        playerProfile.SetMaxHp(hpPoint, GameManager.instance.a_hp, GameManager.instance.e_hp);
        hpPointText.text = GameManager.instance.hpPoint.ToString();
    }
    private void ATKPointUp()
    {
        int atkPoint = playerProfile.ATKPointUp(1);
        GameManager.instance.atkPoint = atkPoint;
        playerProfile.SetMaxATK(atkPoint, GameManager.instance.a_atk, GameManager.instance.e_atk);
        atkPointText.text = GameManager.instance.atkPoint.ToString();
    }
    private void DEFPointUp()
    {
        float defPoint = playerProfile.DEFPointUp(1);
        GameManager.instance.defPoint = defPoint;
        playerProfile.SetMaxDEF(defPoint, GameManager.instance.a_def, GameManager.instance.e_def);
        defPointText.text = GameManager.instance.defPoint.ToString();
    }
    private void CriticalPointUp()
    {
        float cpPoint = playerProfile.CriticalPointUp(1);
        GameManager.instance.criticalPoint = cpPoint;
        playerProfile.SetCritical(cpPoint, GameManager.instance.a_critical, GameManager.instance.e_critical);
        criticalPointText.text = GameManager.instance.criticalPoint.ToString();
    }
}
