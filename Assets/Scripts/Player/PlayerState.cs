using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected float originMoveSpeed = 5.5f;
    public float moveSpeed = 5.5f;
    protected float passiveMoveSpeed = 5.5f;

    [SerializeField] protected float maxHp; //ü��
    [SerializeField] protected float curHp;
    [SerializeField] protected int maxMp = 20; //����
    [SerializeField] protected int curMp = 20;
    [SerializeField] protected float curATK; //���ݷ�
    [SerializeField] protected float maxATK;
    [SerializeField] protected float passiveATK;
    [SerializeField] protected float basicATK;
    [SerializeField] protected float maxBasicATK;
    [SerializeField] protected float curDEF; //����
    [SerializeField] protected float maxDEF;
    [SerializeField] protected float passiveDEF;
    [SerializeField] protected int maxActCount; //�ൿ��
    [SerializeField] protected int curActCount;
    [SerializeField] protected int skillPoint = 0; //��ų ����Ʈ
    [SerializeField] protected int level = 0;
    [SerializeField] protected float critical = 15; //ũ��Ƽ��

    protected int hpPoint;
    protected int atkPoint;
    protected float defPoint;
    protected float criticalPoint;

    [SerializeField] protected int swordBasicAttackCount = 0; //검 기본 공격 횟수
    [SerializeField] protected bool skillStart = false;
    [SerializeField] protected bool bloodHeal = false;
    [SerializeField] protected bool barrier = false;
    [SerializeField] protected bool stampPassiveSKill3 = false;
}
