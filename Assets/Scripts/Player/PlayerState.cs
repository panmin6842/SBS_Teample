using Enemy;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] protected int maxHp = 20; //체력
    [SerializeField] protected int curHp = 20;
    [SerializeField] protected int maxMp = 20; //마나
    [SerializeField] protected int curMp = 20;
    [SerializeField] protected float atk = 10f; //공격력
    [SerializeField] protected float def = 1f; //방어력
    [SerializeField] protected int maxActCount = 10; //행동력
    [SerializeField] protected int curActCount = 10; //행동력
    [SerializeField] protected int skillPoint = 0; //스킬 포인트

    [SerializeField] private HealthSystem healthSystem;

    private void Awake()
    {
        //healthSystem.Init(maxHp);
    }
}
