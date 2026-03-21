using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected float originMoveSpeed = 5.5f;
    public float moveSpeed = 5.5f;
    protected float passiveMoveSpeed = 5.5f;

    [SerializeField] protected float maxHp; //체력
    [SerializeField] protected float curHp;
    [SerializeField] protected int maxMp = 20; //마나
    [SerializeField] protected int curMp = 20;
    [SerializeField] protected float curATK; //공격력
    [SerializeField] protected float maxATK;
    [SerializeField] protected float passiveATK;
    [SerializeField] protected float basicATK;
    [SerializeField] protected float maxBasicATK;
    [SerializeField] protected float curDEF; //방어력
    [SerializeField] protected float maxDEF;
    [SerializeField] protected float passiveDEF;
    [SerializeField] protected int maxActCount; //행동력
    [SerializeField] protected int curActCount;
    [SerializeField] protected int skillPoint = 0; //스킬 포인트
    [SerializeField] protected int level = 0;

    protected int hpPoint;
    protected int atkPoint;
    protected float defPoint;

    [SerializeField] protected int swordBasicAttackCount = 0; //검 기본 공격 횟수
    [SerializeField] protected bool skillStart = false;
    [SerializeField] protected bool bloodHeal = false;
    [SerializeField] protected bool barrier = false;
    [SerializeField] protected bool stampPassiveSKill3 = false;
}
