using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerProfile : PlayerState
{
    [Header("HP관련 오브젝트")]
    [SerializeField] private Image hpBackground;
    [SerializeField] private Image hpMask;
    [SerializeField] private TextMeshProUGUI hpText;

    [Header("Mp관련 오브젝트")]
    [SerializeField] private Image mpBackground;
    [SerializeField] private Image mpMask;
    [SerializeField] private TextMeshProUGUI mpText;

    [Header("행동력관련 오브젝트")]
    [SerializeField] private Slider acSlider;
    [SerializeField] private TextMeshProUGUI acText;

    [Header("스테이터스 표시")]
    [SerializeField] private TextMeshProUGUI hpTestText;
    [SerializeField] private TextMeshProUGUI mpTestText;
    [SerializeField] private TextMeshProUGUI atkTestText;
    [SerializeField] private TextMeshProUGUI basicAtkTestText;
    [SerializeField] private TextMeshProUGUI defTestText;
    [SerializeField] private TextMeshProUGUI moveSpeedTestText;

    private float lerpSpeed = 5;

    private void Start()
    {
    }

    private void Update()
    {
        UpdateStateBarStatue(curHp, maxHp, hpText, hpMask, hpBackground);
        UpdateStateBarStatue(curMp, maxMp, mpText, mpMask, mpBackground);

        UpdateActCountBar();

        StateTestText();
    }

    private void StateTestText()
    {
        hpTestText.text = "hp : " + curHp;
        mpTestText.text = "mp : " + curMp;
        atkTestText.text = "atk : " + curATK;
        basicAtkTestText.text = "basicAtk : " + basicATK;
        defTestText.text = "def : " + curDEF;
        moveSpeedTestText.text = "moveSpeed : " + moveSpeed;
    }

    //public bool NoDamage
    //{
    //    set { NoDamage = value; }
    //    get { return NoDamage; }
    //}

    public bool SkillStart
    {
        set { skillStart = value; }
        get { return skillStart; }
    }

    public int SwordAttackCount
    {
        set { swordBasicAttackCount = value; }
        get { return swordBasicAttackCount; }
    }

    public bool BloodHeal
    {
        set { bloodHeal = value; }
        get { return bloodHeal; }
    }

    public float BasicATK
    {
        get { return basicATK; }
    }

    public void IncreasedHp(float increasedPercent)
    {
        maxHp = maxHp * (increasedPercent / 100f);
    }
    public void IncreasedMp(float increasedPercent)
    {
        maxMp = (int)(maxMp * (increasedPercent / 100f));
    }

    //패시브 효과
    public void PassiveATK(float increasedPercent)
    {
        passiveATK = maxATK * (1f + (increasedPercent / 100f));
        curATK = passiveATK;
    }

    public void PassiveBasicATK(float increasedPercent)
    {
        passiveATK = maxATK * (1f + (increasedPercent / 100f));
        basicATK = passiveATK;
    }
    public void PassiveDEF(float increasedPercent)
    {
        passiveDEF = maxDEF * (1f + (increasedPercent / 100f));
        curDEF = passiveDEF;
    }

    public void PassiveMoveSpeed(float increasedPercent)
    {
        passiveMoveSpeed = (int)(originMoveSpeed * (1f + (increasedPercent / 100f)));
        moveSpeed = passiveMoveSpeed;
    }

    //패시브 효과 초기화
    public void StateReset()
    {
        passiveATK = maxATK;
        passiveDEF = maxDEF;
        passiveMoveSpeed = originMoveSpeed;
        bloodHeal = false;
    }

    //스킬 효과
    public void GetDamage(int damage)
    {
        curHp -= damage;

        curHp = Mathf.Clamp(curHp, 0, maxHp);
    }

    public void UseMP(int mp)
    {
        curMp -= mp;

        curMp = Mathf.Clamp(curMp, 0, maxMp);
    }

    public float ATK(float damagePercent)
    {
        return curATK * (damagePercent / 100f);
    }

    public void ChangeATK(float changePercent)
    {
        curATK = passiveATK * (1f + changePercent / 100f);
    }

    public void ChangeDEF(float changePercent)
    {
        curDEF = passiveDEF * (1f + changePercent / 100f);
    }

    public void ChangeMoveSpeed(float changePercent)
    {
        moveSpeed = passiveMoveSpeed * (1f + (changePercent / 100f));
    }

    public void BloodHealHp(float bloodPercent, float damage)
    {
        float bloodValue;
        bloodValue = damage * (bloodPercent / 100f);
        curHp += bloodValue;

        curHp = Mathf.Clamp(curHp, 0, maxHp);
    }

    public void UseActCount(int actCount)
    {
        curActCount -= actCount;

        curActCount = Mathf.Clamp(curActCount, 0, maxActCount);
    }

    private void UpdateStateBarStatue(float curState, float maxState, TextMeshProUGUI stateText, Image _mask, Image _background)
    {
        float _curState = curState;
        float _maxState = maxState;

        stateText.text = string.Format("{0} / {1}", Mathf.CeilToInt(_curState), Mathf.CeilToInt(_maxState));

        float height = _mask.GetComponent<RectTransform>().sizeDelta.y;
        float fullWidth = _background.GetComponent<RectTransform>().sizeDelta.x;

        //목표 값
        float targetWidth = (_curState / _maxState) * fullWidth;

        //현재 값
        float curWidth = _mask.GetComponent<RectTransform>().sizeDelta.x;

        //게이지 부드럽게 이동
        float newWidth = Mathf.Lerp(curWidth, targetWidth, Time.deltaTime * lerpSpeed);
        _mask.GetComponent<RectTransform>().sizeDelta = new Vector2(newWidth, height);
    }

    private void UpdateActCountBar()
    {
        acText.text = string.Format("{0} / {1}", curActCount, maxActCount);

        float _curActCount = (float)curActCount / (float)maxActCount;

        acSlider.value = Mathf.Lerp(acSlider.value, _curActCount, Time.deltaTime * lerpSpeed);
    }
}
