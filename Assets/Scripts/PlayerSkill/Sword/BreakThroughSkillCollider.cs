using UnityEngine;

public class BreakThroughSkillCollider : MonoBehaviour
{
    [SerializeField] private int Count;

    private BreakThroughSkill parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        parent = GetComponentInParent<BreakThroughSkill>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌이 발생하면 부모에게 정보를 넘김
        if (other.CompareTag("Enemy"))
        {
            parent.OnPartHit(other, Count);
        }
    }
}
