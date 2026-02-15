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

    private float lerpSpeed = 5;

    private void Start()
    {
    }

    private void Update()
    {
        UpdateStateBarStatue(curHp, maxHp, hpText, hpMask, hpBackground);
        UpdateStateBarStatue(curMp, maxMp, mpText, mpMask, mpBackground);

        UpdateActCountBar();
    }

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
        return atk * (damagePercent / 100f);
    }

    public void ChangeMoveSpeed(float changePercent)
    {
        moveSpeed = originMoveSpeed * (1f + (changePercent / 100f));
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

        stateText.text = string.Format("{0} / {1}", _curState, _maxState);

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
