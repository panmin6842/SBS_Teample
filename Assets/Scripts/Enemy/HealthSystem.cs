using System.Collections.Generic;
using Interface;
using JJW._02_Code;
using UnityEngine;
using Utility;

namespace Enemy
{
    public class HealthSystem : MonoBehaviour
    {
        public NotifyValue<int> CurrentHp { get; }
        public NotifyValue<int> MaxHp { get; }
        
        [SerializeField] private List<IHealthObserver> observers = new List<IHealthObserver>();

        public void Init(int maxHp)
        {
            MaxHp.Value = maxHp;
            CurrentHp.Value = maxHp;

            CurrentHp.OnValueChanged += (beforeHp, currentHp) =>
            {
                if (beforeHp - currentHp < 0)
                {
                    foreach (var observer in  observers)
                    {
                        observer.OnHealthIncreased(beforeHp, currentHp);
                    }
                }
                else
                {
                    foreach (var observer in observers)
                    {
                        observer.OnHealthDecreased(beforeHp, currentHp);
                    }
                }

                
            };
        }
    }
}