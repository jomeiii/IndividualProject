using UnityEngine;
using Weapons;

namespace Character.NPC.AttackEnemies
{
    public class AttackEnemy : NPC, IAttackCharacter
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

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
        }

        protected override void Start()
        {
            base.Start();
            
            GetDamage(0);
        }

        public void TakeDamage(IAttackCharacter iAttackCharacter)
        {
            iAttackCharacter.GetDamage(_weapon.Damage);
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
            if (gameObject != null)
                Destroy(gameObject);
        }

        public void CanAttackOn()
        {
            _weapon.CanAttack = true;
        }

        public void CanAttackOff()
        {
            _weapon.CanAttack = false;
        }

        protected virtual void StartAttacking()
        {
            if (_weapon.IsAttacking) return;
            
            _weapon.IsAttacking = true;
            _animator.applyRootMotion = true;
            _animator.SetTrigger(Attack);
        }

        protected virtual void StopAttacking()
        {
            _weapon.IsAttacking = false;
            _animator.applyRootMotion = false;
        }
    }
}