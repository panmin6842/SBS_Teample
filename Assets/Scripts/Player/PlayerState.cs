using Enemy;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    protected int maxHp = 20; //체력
    protected float atk = 10f; //공격력
    protected float def = 1f; //방어력
    protected int actCount = 2; //행동력

    [SerializeField] private HealthSystem healthSystem;

    private void Awake()
    {
        //healthSystem.Init(maxHp);
    }
}
