using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
public enum PlayerSituation
{
    Idle,
    //Move,
    Attack,
    Hit,
    Die
}

public class PlayerProfile : PlayerState
{
    [Header("HP관련 오브젝트")]
    private Image hpBackground;
    private Image hpMask;
    private TextMeshProUGUI hpText;

    [Header("Mp관련 오브젝트")]
    private Image mpBackground;
    private Image mpMask;
    private TextMeshProUGUI mpText;

    [Header("행동력관련 오브젝트")]
    private Slider acSlider;
    private TextMeshProUGUI acText;

    [Header("스테이터스 표시")]
    private TextMeshProUGUI hpTestText;
    private TextMeshProUGUI mpTestText;
    private TextMeshProUGUI atkTestText;
    private TextMeshProUGUI basicAtkTestText;
    private TextMeshProUGUI defTestText;
    private TextMeshProUGUI moveSpeedTestText;
    private TextMeshProUGUI criticalTestText;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI levelText;
    private TextMeshProUGUI jobText;
    [Header("Hit 프리펩")]
    [SerializeField] private GameObject swordSkillHitPrefab;
    [SerializeField] private GameObject bowSkillHitPrefab;
    [SerializeField] private GameObject stampSkillHitPrefab;

    private CinemachineBasicMultiChannelPerlin noiseComponent;

    private float lerpSpeed = 5;

    public PlayerSituation currentState = PlayerSituation.Idle;
    public Animator ani;
    private void Start()
    {
        hpBackground = UIManager.Instance.hpBackground;
        hpMask = UIManager.Instance.hpMask;
        hpText = UIManager.Instance.hpText;
        mpBackground = UIManager.Instance.mpBackground;
        mpMask = UIManager.Instance.mpMask;
        mpText = UIManager.Instance.mpText;
        acSlider = UIManager.Instance.acSlider;
        acText = UIManager.Instance.acText;
        hpTestText = UIManager.Instance.hpStatusText;
        mpTestText = UIManager.Instance.mpStatusText;
        atkTestText = UIManager.Instance.atkStatusText;
        basicAtkTestText = UIManager.Instance.basicAtkStatusText;
        defTestText = UIManager.Instance.defStatusText;
        moveSpeedTestText = UIManager.Instance.moveSpeedStatusText;
        criticalTestText = UIManager.Instance.criticalStatusText;
        nameText = UIManager.Instance.nameText;
        levelText = UIManager.Instance.levelText;
        jobText = UIManager.Instance.jobText;

        UIManager.Instance.profileNameText.text = GameManager.instance.name.ToString();
        UIManager.Instance.profileLevelText.text = "LV." + GameManager.instance.level.ToString(); 

        if (UIManager.Instance.virtualCamera != null)
        {
            noiseComponent = UIManager.Instance.virtualCamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    private void Update()
    {
        UpdateStateBarStatue(curHp, maxHp, hpText, hpMask, hpBackground);
        UpdateStateBarStatue(curMp, maxMp, mpText, mpMask, mpBackground);

        UpdateActCountBar();

        //StateTestText();
    }

    public void StateTestText()
    {
        hpTestText.text = maxHp.ToString();
        mpTestText.text = maxMp.ToString();
        atkTestText.text = maxATK.ToString();
        basicAtkTestText.text = maxBasicATK.ToString();
        defTestText.text = maxDEF.ToString();
        moveSpeedTestText.text = moveSpeed.ToString();
        criticalTestText.text = critical.ToString();
        nameText.text = GameManager.instance.name;
        levelText.text = GameManager.instance.level.ToString();
        jobText.text = GameManager.instance.job.ToString();
    }

    public void AnimationReset()
    {
        ani.SetBool("isWalk", false);
        ani.ResetTrigger("Attack1");
        ani.ResetTrigger("Attack2");
    }

    //Hit 프리펩 소환
    public void SwordSkillHit(Vector3 hitPoint)
    {
        Instantiate(swordSkillHitPrefab, hitPoint, Quaternion.identity);
    }
    public void BowSkillHit(Vector3 hitPoint)
    {
        Instantiate(bowSkillHitPrefab, hitPoint, Quaternion.identity);
    }
    public void StampSkillHit(Vector3 hitPoint)
    {
        Instantiate(bowSkillHitPrefab, hitPoint, Quaternion.identity);
    }

    //카메라 흔들림
    public void ShakeCamera(float duration, float intensity, float frequency)
    {
        if (noiseComponent != null)
        {
            StartCoroutine(ShakeRoutine(duration, intensity, frequency));
        }
    }

    IEnumerator ShakeRoutine(float duration, float intensity, float frequency)
    {
        noiseComponent.AmplitudeGain = intensity;
        noiseComponent.FrequencyGain = frequency;

        yield return new WaitForSeconds(duration);
        noiseComponent.AmplitudeGain = 0f;
        noiseComponent.FrequencyGain = 0f;
    }

    //카메라 줌 인
    public void CameraZoom(float duration, float zoomSpeed, float zoomInFOV)
    {
        StartCoroutine(ZoomRoutine(duration, zoomSpeed, zoomInFOV));
    }

    IEnumerator ZoomRoutine(float duration, float zoomSpeed, float zoomInFOV)
    {
        float elapsed = 0f;
        while (elapsed < 0.2f)
        {
            UIManager.Instance.virtualCamera.Lens.FieldOfView =
                Mathf.Lerp(UIManager.Instance.virtualCamera.Lens.FieldOfView, zoomInFOV, Time.deltaTime * zoomSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(duration);

        elapsed = 0f;
        while (elapsed < 0.5f)
        {
            UIManager.Instance.virtualCamera.Lens.FieldOfView =
                Mathf.Lerp(UIManager.Instance.virtualCamera.Lens.FieldOfView, 53f, Time.deltaTime * zoomSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        UIManager.Instance.virtualCamera.Lens.FieldOfView = 53f;
    }

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

    public bool Barrier
    {
        set { barrier = value; }
        get { return barrier; }
    }

    public float MaxHp
    {
        get { return maxHp; }
    }

    public bool StampPassiveSkill3
    {
        set { stampPassiveSKill3 = value; }
    }

    public int Level
    {
        get { return level; }
    }

    public int ActCount
    {
        get { return curActCount; }
    }

    //max스테이터스 설정
    public void SetMaxHp(float hpPoint, float a_hp, float e_hp)
    {
        maxHp = Mathf.Round((hpPoint * 10) * (1 + a_hp) + e_hp);
        curHp = maxHp;
    }

    public void SetMaxATK(float atkPoint, float a_atk, float e_atk)
    {
        maxBasicATK = (2 * atkPoint) + (e_atk + a_atk);
        maxATK = 2 * atkPoint + (e_atk + a_atk);
        basicATK = maxBasicATK;
        curATK = maxATK;
    }

    public void SetMaxDEF(float defPoint, float a_def, float e_def)
    {
        maxDEF = (0.5f * defPoint) + (e_def + a_def);
        //maxDEF = Mathf.Clamp(maxDEF, 0f, 0.95f);
        curDEF = maxDEF;
    }

    public void SetMaxMp(int a_mp)
    {
        maxMp = maxMp + a_mp;
        curMp = maxMp;
    }

    public void SetCritical(float cpPoint, float a_critical, float e_critical)
    {
        critical = 15 + (cpPoint * 0.5f) + (a_critical + e_critical);
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
        passiveATK = maxBasicATK * (1f + (increasedPercent / 100f));
        basicATK = passiveATK;
    }
    public void PassiveDEF(float increasedPercent)
    {
        passiveDEF = maxDEF * (1f + (increasedPercent / 100f));
        curDEF = passiveDEF;
    }

    public void PassiveMoveSpeed(float increasedPercent)
    {
        passiveMoveSpeed = originMoveSpeed * (1f + (increasedPercent / 100f));
        moveSpeed = passiveMoveSpeed;
    }

    public void GetDamage(int damage)
    {
        curHp -= damage * (1 - curDEF);

        curHp = Mathf.Clamp(curHp, 0, maxHp);
    }

    public void PlayerDie()
    {
        if (curHp <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        playerDie = true;
        curHp = 0;
        yield return new WaitForSeconds(1);
        if (playerDie)
        {
            curActCount -= 5;
            int GoldDown = Mathf.RoundToInt(GameManager.instance.gold * 0.1f);
            GameManager.instance.gold -= GoldDown;
            Vector3 spawnPos = GameObject.FindGameObjectWithTag("DungeonEntry").GetComponent<Transform>().position;
            transform.position = spawnPos;
            curHp = maxHp;
            playerDie = false;
        }
    }

    public bool PlayerDeadCheck()
    {
        if (curHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActCountDie()
    {
        int GoldDown = Mathf.RoundToInt(GameManager.instance.gold * 0.3f);
        GameManager.instance.gold -= GoldDown;
        transform.position = UIManager.Instance.villagePos.position;
    }

    public void SelfHpDamage(float damagePercent)
    {
        float buff = maxHp * damagePercent;
        curHp -= buff;
        curHp = Mathf.Clamp(curHp, 0, maxHp);
    }

    public void HPBuff(float buffPercent)
    {
        float buff = maxHp * buffPercent;
        curHp += buff;
        curHp = Mathf.Clamp(curHp, 0, maxHp);
    }

    public void UseMP(int mp)
    {
        if (!stampPassiveSKill3)
        {
            curMp -= mp;
        }
        else if (stampPassiveSKill3)
        {
            curMp -= (mp * 2);
        }

        curMp = Mathf.Clamp(curMp, 0, maxMp);
    }

    public void MPBuff(int buff)
    {
        curMp += buff;
        curMp = Mathf.Clamp(curMp, 0, maxMp);
    }

    public bool MPBuffStart()
    {
        return curMp < maxMp;
    }

    public float ATK(float damagePercent)
    {
        return curATK * (damagePercent / 100f);
    }
    public float BasicATK(float damagePercent)
    {
        return basicATK * (damagePercent / 100f);
    }

    public float CriticalBuff(float damage)
    {
        return damage * 1.5f;
    }

    public bool CriticalProbability()
    {
        int random = Random.Range(1, 101);
        return (random >= 1 && random <= critical);
    }

    public void ChangeATK(float changePercent)
    {
        curATK = passiveATK * (1f + changePercent / 100f);
    }

    public void ChangeBasicATK(float changePercent)
    {
        basicATK = passiveATK * (1f + changePercent / 100f);
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
        float limitValue = maxHp * 0.01f;

        float finalHealAmount = Mathf.Min(bloodValue, limitValue); //더 작은 값 반환
        curHp += finalHealAmount;

        curHp = Mathf.Clamp(curHp, 0, maxHp);

        Debug.Log($"데미지: {damage} | 계산된 흡혈: {bloodValue} | 실제 흡혈(제한적용): {finalHealAmount}");
    }

    public void UseActCount(int actCount)
    {
        curActCount -= actCount;

        curActCount = Mathf.Clamp(curActCount, 0, maxActCount);

        if(curActCount <= 0)
        {
            ActCountDie();
        }
    }

    public void ActCountReset()
    {
        curActCount = maxActCount;
    }

    public void LevelUp(int levelCount)
    {
        level += levelCount;
    }

    public int HpPointUp(int _hpPoint)
    {
        hpPoint = GameManager.instance.hpPoint;
        hpPoint += _hpPoint;
        return hpPoint;
    }

    public int ATKPointUp(int _atkPoint)
    {
        atkPoint = GameManager.instance.atkPoint;
        atkPoint += _atkPoint;
        return atkPoint;
    }

    public float DEFPointUp(float _defPoint)
    {
        defPoint = GameManager.instance.defPoint;
        defPoint += _defPoint;
        return defPoint;
    }

    public float CriticalPointUp(float _cpPoint)
    {
        criticalPoint = GameManager.instance.criticalPoint;
        criticalPoint += _cpPoint;
        return criticalPoint;
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
