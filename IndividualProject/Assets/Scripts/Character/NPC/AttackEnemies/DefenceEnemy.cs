using Character.Player;
using Controllers;
using UnityEngine;
using UnityEngine.AI;

namespace Character.NPC.AttackEnemies
{
    public class DefenceEnemy : AttackEnemy
    {
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        [Header("Settings")] [SerializeField] private float _defenceRange;

        [Header("References")] [SerializeField]
        private NavMeshAgent _navMeshAgent;

        [SerializeField] private TriggerController _triggerTriggerController;
        [SerializeField] private BoxCollider _triggerBoxCollider;

        [SerializeField] private TriggerController _attackTriggerController;

        private Vector3 _startPosition;

        protected override void OnEnable()
        {
            base.OnEnable();

            _triggerTriggerController.OnTriggerStayEvent += TriggerTriggerStay;
            _triggerTriggerController.OnTriggerExitEvent += TriggerTriggerExit;
            _attackTriggerController.OnTriggerStayEvent += AttackTriggerStay;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _triggerTriggerController.OnTriggerEnterEvent -= TriggerTriggerStay;
            _triggerTriggerController.OnTriggerExitEvent -= TriggerTriggerExit;
            _attackTriggerController.OnTriggerStayEvent -= AttackTriggerStay;
        }

        protected override void Awake()
        {
            base.Awake();
            
            _triggerBoxCollider.size = new Vector3(_defenceRange, _triggerBoxCollider.size.y, _defenceRange);
            _startPosition = _transform.position;
        }

        protected override void Update()
        {
            base.Update();

            if (!_navMeshAgent.pathPending && 
                _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && 
                !_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                if (!_weapon.IsAttacking)
                {
                    _animator.SetBool(IsMoving, false);
                }
            }
        }

        private void TriggerTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                _navMeshAgent.SetDestination(playerController.transform.position);
                _animator.SetBool(IsMoving, true);
            }
        }

        private void TriggerTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                _navMeshAgent.SetDestination(_startPosition);
                _animator.SetBool(IsMoving, true);
            }
        }

        private void AttackTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController) && !_weapon.IsAttacking)
            {
                StartAttacking(other.transform);
            }
        }
    }
}