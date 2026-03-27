using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator ani;

    [SerializeField] private PlayerProfile playerProfile;
    private void Start()
    {
        ani = GetComponent<Animator>();
    }
    public void SwordAttack1AniFalse()
    {
        playerProfile.currentState = PlayerSituation.Idle;
        ani.ResetTrigger("Attack1");
    }

    public void SwordAttack2AniFalse()
    {
        playerProfile.currentState = PlayerSituation.Idle;
        ani.ResetTrigger("Attack2");
    }
}
