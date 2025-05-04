using Character;
using Character.AttackCharacter;
using Controllers;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] protected int _damage;
        [SerializeField] protected bool _isAttacking;
        [SerializeField] protected bool _canAttack;

        [Header("References")] public TriggerController triggerController;

        private IAttackCharacter _iAttackerCharacter;

        public int Damage => _damage;

        public bool IsAttacking
        {
            get => _isAttacking;
            set => _isAttacking = value;
        }

        public bool CanAttack
        {
            set => _canAttack = value;
        }

        protected virtual void OnEnable()
        {
            triggerController.OnTriggerEnterEvent += TriggerEnter;
        }

        protected virtual void OnDisable()
        {
            triggerController.OnTriggerEnterEvent -= TriggerEnter;
        }

        protected virtual void Awake()
        {
            if (triggerController == null)
            {
                triggerController = GetComponent<TriggerController>();
            }
        }

        protected virtual void Update()
        {
            if (_iAttackerCharacter != null && _canAttack)
            {
                _iAttackerCharacter.GetDamage(_damage);
                _canAttack = false;
                _isAttacking = false;
            }
        }

        private void TriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AttackCharacterColliderController attackCharacterColliderController))
            {
                if (_isAttacking)
                {
                    _iAttackerCharacter = attackCharacterColliderController.IAttackCharacter;
                }
            }
        }
    }
}