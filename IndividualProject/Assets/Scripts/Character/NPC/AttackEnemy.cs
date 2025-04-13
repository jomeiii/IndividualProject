using UnityEngine;
using Weapons;

namespace Character.NPC
{
    public class AttackEnemy : NPC, IAttackCharacter
    {
        [SerializeField] protected int _health;
        [SerializeField] protected Weapon _weapon;

        protected int _maxHealth;

        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public Weapon Weapon
        {
            get => _weapon;
            set => _weapon = value;
        }

        protected override void Awake()
        {
            base.Awake();

            _maxHealth = _health;
            GetDamage(0);
        }

        public void TakeDamage(IAttackCharacter iAttackCharacter)
        {
            throw new System.NotImplementedException();
        }

        public void GetDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
            else
            {
                _npcInfoPresenter.OnHealthChanged(_health, _maxHealth);
            }
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}