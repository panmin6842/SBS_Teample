using UnityEngine;

namespace Enemy.EnemyAttack
{
    [CreateAssetMenu(fileName = "EnemyAttackSO", menuName = "JJW/Enemy/EnemyAttack/EnemyBoxAttackSO", order = 0)]
    public class EnemyBoxAttackSO : AttackActionSO
    {
        private static readonly Collider[] HitBuffer = new Collider[1];

        public override void Attack(GoalAgent.GoalAgent goalAgent)
        {
            int damage = goalAgent.EnemyInfoSO.AttackPower + AdditionalMeleeAttackDamage;

            Vector3 center = goalAgent.transform.TransformPoint(HitboxSO.Offset);
            Vector3 halfExtents = HitboxSO.Size * 0.5f;

            int count = Physics.OverlapBoxNonAlloc(
                center,
                halfExtents,
                HitBuffer,
                goalAgent.transform.rotation,
                AttackTargetLayerMask
            );

            if (count > 0 && HitBuffer[0].TryGetComponent<HealthSystem>(out var healthSystem))
            {
                //플레이어 공격
                // healthSystem.CurrentHp.Value -= damage;
                Debug.Log("플레이어 공격함");

#if UNITY_EDITOR
                Debug.Log($"플레이어에게 {damage}만큼 대미지 적용");
#endif
            }
        }
    }
}