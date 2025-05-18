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
        [SerializeField] private GameObject _currentIAttackCharacterGameObject;

        private IAttackCharacter _iAttackerCharacter;
        private IAttackCharacter _currentIAttackCharacter;

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
            triggerController.OnTriggerStayEvent += TriggerStay;
        }

        protected virtual void OnDisable()
        {
            triggerController.OnTriggerStayEvent -= TriggerStay;
        }

        protected virtual void Awake()
        {
            if (triggerController == null)
            {
                triggerController = GetComponent<TriggerController>();
            }

            _currentIAttackCharacter = _currentIAttackCharacterGameObject.GetComponent<IAttackCharacter>();
        }

        protected virtual void Update()
        {
            if (_canAttack)
            {
                if (_iAttackerCharacter != null)
                {
                    _canAttack = false;
                    _iAttackerCharacter.GetDamage(_damage);
                    if (_iAttackerCharacter.Health <= 0)
                    {
                        _iAttackerCharacter = null;
                    }
                }
            }
        }

        private void TriggerStay(Collider other)
        {
            if (other.TryGetComponent(out AttackCharacterColliderController attackCharacterColliderController))
            {
                if (_isAttacking)
                {
                    if (_currentIAttackCharacter != attackCharacterColliderController.IAttackCharacter)
                    {
                        _iAttackerCharacter = attackCharacterColliderController.IAttackCharacter;
                    }
                }
            }
        }
    }
}