using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected float originMoveSpeed = 5.5f;
    public float moveSpeed = 5.5f;
    protected bool noDamage = false;

    [SerializeField] protected int maxHp = 20; //체력
    [SerializeField] protected int curHp = 20;
    [SerializeField] protected int maxMp = 20; //마나
    [SerializeField] protected int curMp = 20;
    [SerializeField] protected float curATK = 10f; //공격력
    [SerializeField] protected float maxATK = 10f;
    [SerializeField] protected float curDEF = 1f; //방어력
    [SerializeField] protected float maxDEF = 1f;
    [SerializeField] protected int maxActCount = 10; //행동력
    [SerializeField] protected int curActCount = 10;
    [SerializeField] protected int skillPoint = 0; //스킬 포인트

    [SerializeField] protected int swordBasicAttackCount = 0; //검 기본 공격 횟수
}
