using Character.AttackCharacter;
using Character.Player;
using Controllers;
using UnityEngine;
using UnityEngine.AI;

namespace Character.NPC.AttackEnemies
{
    public class DefenceEnemy : AttackEnemy
    {
        [SerializeField] private TriggerController _followingTriggerController;

        private bool _isFollowing;
        private NavMeshAgent _navMeshAgent;
        private Vector3 _startPosition;
        private float _originalStoppingDistance;
        private bool _isGoingToStartPosition;
        private bool _waitingForPlayer;
        private IAttackCharacter _npcIAttackCharacter;

        private Transform _attackerTransform;

        protected override void OnEnable()
        {
            base.OnEnable();

            _followingTriggerController.OnTriggerStayEvent += OnFollowingTriggerStay;
            _followingTriggerController.OnTriggerExitEvent += OnFollowingTriggerExit;

            _weapon.triggerController.OnTriggerStayEvent += OnWeaponTriggerStay;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _followingTriggerController.OnTriggerStayEvent -= OnFollowingTriggerStay;
            _followingTriggerController.OnTriggerExitEvent -= OnFollowingTriggerExit;

            _weapon.triggerController.OnTriggerStayEvent -= OnWeaponTriggerStay;
        }

        protected override void Awake()
        {
            base.Awake();

            _navMeshAgent = GetComponent<NavMeshAgent>();
            _startPosition = transform.position;
            _originalStoppingDistance = _navMeshAgent.stoppingDistance;
            _npcIAttackCharacter = GetComponent<IAttackCharacter>();
        }

        protected override void Update()
        {
            base.Update();

            if (!_isFollowing && !_weapon.IsAttacking)
            {
                if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        if (!_isGoingToStartPosition && !_waitingForPlayer)
                        {
                            _navMeshAgent.stoppingDistance = 0f;
                            _navMeshAgent.SetDestination(_startPosition);
                            _isGoingToStartPosition = true;
                            _animator.SetBool(Walk, true);
                            _animator.SetBool(Idle, false);
                        }
                        else if (_isGoingToStartPosition)
                        {
                            _isGoingToStartPosition = false;
                            _navMeshAgent.stoppingDistance = _originalStoppingDistance;
                            _animator.SetBool(Walk, false);
                            _animator.SetBool(Idle, true);

                            _waitingForPlayer = true;
                        }
                    }
                }
            }
        }

        protected override void StartAttacking()
        {
            base.StartAttacking();

            _navMeshAgent.SetDestination(_navMeshAgent.transform.position);
            _navMeshAgent.transform.LookAt(_attackerTransform);
            _navMeshAgent.transform.rotation = Quaternion.Euler(0f, _navMeshAgent.transform.rotation.eulerAngles.y, 0f);
        }

        private void OnFollowingTriggerStay(Collider other)
        {
            if (IsPlayerObject(other, out PlayerController playerController))
            {
                if (_waitingForPlayer)
                {
                    _isFollowing = true;

                    _navMeshAgent.SetDestination(playerController.transform.position);
                    _animator.SetBool(Idle, false);
                    _animator.SetBool(Walk, true);
                }
            }
        }

        private void OnFollowingTriggerExit(Collider other)
        {
            if (IsPlayerObject(other, out PlayerController playerController))
            {
                _isFollowing = false;
                _waitingForPlayer = false;
            }
        }

        private void OnWeaponTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out AttackCharacterColliderController attackCharacterColliderController))
            {
                if (_npcIAttackCharacter != attackCharacterColliderController.IAttackCharacter && !_weapon.IsAttacking)
                {
                    StartAttacking();
                }
            }
        }

        private bool IsPlayerObject(Collider other, out PlayerController playerController)
        {
            return other.TryGetComponent(out playerController);
        }
    }
}