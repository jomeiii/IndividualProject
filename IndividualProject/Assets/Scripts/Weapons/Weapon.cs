using Character.AttackCharacter;
using Controllers;
using UnityEngine;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] protected int _damage;
        [SerializeField] protected bool _isAttacking;

        [Header("References")] [SerializeField]
        public TriggerController triggerController;

        public int Damage => _damage;

        public bool IsAttacking
        {
            get => _isAttacking;
            set => _isAttacking = value;
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

        private void TriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out AttackCharacterColliderController attackCharacterColliderController))
            {
                if (_isAttacking)
                {
                    attackCharacterColliderController.IAttackCharacter.GetDamage(_damage);
                    _isAttacking = false;
                }
            }
        }
    }
}