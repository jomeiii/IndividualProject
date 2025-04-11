using UnityEngine;

namespace Character
{
    public abstract class Character : MonoBehaviour
    {
        public int Health { get; protected set; }

        protected virtual void Awake()
        {
            
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
            
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            
        }
    }
}