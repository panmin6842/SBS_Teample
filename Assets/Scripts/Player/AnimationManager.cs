using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator ani;

    [SerializeField] private PlayerProfile playerProfile;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void Attack1AniFalse()
    {
        playerProfile.currentState = PlayerSituation.Idle;
        playerProfile.ChangeMoveSpeed(0);
        ani.ResetTrigger("Attack1");
    }

    public void Attack2AniFalse()
    {
        playerProfile.currentState = PlayerSituation.Idle;
        playerProfile.ChangeMoveSpeed(0);
        ani.ResetTrigger("Attack2");
    }

    public void EffectDestory()
    {
        Destroy(gameObject);
    }
}
