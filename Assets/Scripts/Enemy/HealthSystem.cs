using System;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class HealthSystem : MonoBehaviour
    {
        public IReadOnlyNotifyValue<int> CurrentHp => _currentHp;
        public IReadOnlyNotifyValue<int> MaxHp => _maxHp;

        public event Action<int, int> OnHealthDecreased;
        public event Action<int, int> OnHealthIncreased;
        public event Action OnDead;

        private NotifyValue<int> _currentHp;
        private NotifyValue<int> _maxHp;

        public void Init(int maxHp)
        {
            _currentHp.Value = maxHp;
            _maxHp.Value = maxHp;

            CurrentHp.OnValueChanged += (beforeHp, currentHp) =>
            {
                if (currentHp <= 0)
                {
                    OnDead?.Invoke();
                }
                else if (beforeHp - currentHp < 0)
                {
                    OnHealthDecreased?.Invoke(currentHp, maxHp);
                }
                else
                {
                    OnHealthIncreased?.Invoke(currentHp, maxHp);
                }
            };
        }

        public void ApplyDamage(int damage)
        {
            if (CurrentHp.Value <= 0 || CurrentHp.Value - damage <= 0)
            {
                OnDead?.Invoke();
            }
            else
            {
                _currentHp.Value -= damage;
            }
        }

        public void ApplyHeal(int heal)
        {
            if (CurrentHp.Value <= 0 || CurrentHp.Value + heal >= _maxHp.Value)
            {
                _currentHp.Value = MaxHp.Value;
            }
            else
            {
                _currentHp.Value += heal;
            }
        }
    }
}
