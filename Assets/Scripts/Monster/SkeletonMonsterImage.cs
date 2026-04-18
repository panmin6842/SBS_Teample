using UnityEngine;

public enum AnimState
{
    Idle,
    Walk,
    Attack
}

public class SkeletonMonsterImage : MonoBehaviour
{
    MonsterBehavior monsterBehavior;
    public AnimState animstate;
    Animator animator;
    Transform originalTrans;

    private void Awake()
    {
        monsterBehavior = GetComponentInParent<MonsterBehavior>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        originalTrans = transform;
    }

    void Update()
    {
        switch (animstate)
        {
            case AnimState.Idle:
                animator.SetBool("Idle", true);
                animator.SetBool("Walk", false);
                animator.SetBool("Attack", false);
                break;
            case AnimState.Walk:
                animator.SetBool("Walk", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Attack", false);
                break;
            case AnimState.Attack:
                animator.SetBool("Attack", true);
                animator.SetBool("Idle", false);
                animator.SetBool("Walk", false);
                break;
        }

        transform.rotation = Quaternion.Euler(30, 0, 0);
    }
}
