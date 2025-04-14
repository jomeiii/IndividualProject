using UnityEngine;
using Weapons;

namespace Character.NPC
{
    public class AttackEnemy : NPC, IAttackCharacter
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        [SerializeField] protected int _health;
        [SerializeField] protected Weapon _weapon;

        [SerializeField] protected Animator _animator;

        protected int _maxHealth;
        
        private bool _isAttacking;

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

        protected void StartAttacking(Transform target)
        {
            if (_weapon.IsAttacking || _isAttacking) return;

            _weapon.IsAttacking = true;
            _isAttacking = true;
            _animator.applyRootMotion = true;
            transform.LookAt(target);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            _animator.SetTrigger(Attack);
        }

        private void StopAttacking()
        {
            _animator.applyRootMotion = false;
            _weapon.IsAttacking = false;
            _isAttacking = false;
        }
    }
}