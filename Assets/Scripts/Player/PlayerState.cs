using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected float originMoveSpeed = 5.5f;
    public float moveSpeed = 5.5f;
    protected float passiveMoveSpeed = 5.5f;



    [SerializeField] protected int swordBasicAttackCount = 0; //검 기본 공격 횟수
    [SerializeField] protected bool skillStart = false;
    [SerializeField] protected bool bloodHeal = false;
    [SerializeField] protected bool barrier = false;
    [SerializeField] protected bool stampPassiveSKill3 = false;
}
