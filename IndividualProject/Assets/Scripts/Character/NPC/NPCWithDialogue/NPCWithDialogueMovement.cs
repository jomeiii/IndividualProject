using System;
using System.Collections;
using Systems.DialogueSystem;
using UnityEngine;
using UnityEngine.AI;

namespace Character.NPC.NPCWithDialogue
{
    public class NPCWithDialogueMovement : NPCWithDialogue
    {
        protected event Action<int> CurrentIndexChangedEvent;

        [SerializeField] protected Waypoints[] _waypoints;
        [SerializeField] protected int _currentIndex;

        private bool _isMoving;
        private NavMeshAgent _navMeshAgent;

        private DialogueManager DialogueManager => DialogueManager.Instance;

        protected override void Awake()
        {
            base.Awake();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            DialogueManager.NPCMovementStartedEvent += OnNPCMovementStart;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            DialogueManager.NPCMovementStartedEvent -= OnNPCMovementStart;
        }

        protected override void TriggerEnter(Collider other)
        {
            if (!_isMoving)
                base.TriggerEnter(other);
        }

        protected override void TriggerExit(Collider other)
        {
            if (!_isMoving)
                base.TriggerExit(other);
        }

        private void OnNPCMovementStart(NPCWithDialogue npcWithDialogue)
        {
            if (npcWithDialogue != this) return;
            StartCoroutine(MoveAlongWaypoints());
        }

        private IEnumerator MoveAlongWaypoints()
        {
            if (!TryGetCurrentPath(out var path)) yield break;

            BeginMovement();

            foreach (var point in path)
            {
                _navMeshAgent.SetDestination(point.position);
                yield return StartCoroutine(WaitUntilReachedDestination());
            }

            EndMovement();
        }

        private IEnumerator WaitUntilReachedDestination()
        {
            while (_navMeshAgent.pathPending ||
                   _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance ||
                   _navMeshAgent.velocity.sqrMagnitude > 0.01f)
            {
                yield return null;
            }
        }

        private bool TryGetCurrentPath(out Transform[] path)
        {
            path = null;

            if (_currentIndex >= _waypoints.Length) return false;

            var waypointSet = _waypoints[_currentIndex];
            if (waypointSet == null || waypointSet.waypoints == null || waypointSet.waypoints.Length == 0)
                return false;

            path = waypointSet.waypoints;
            return true;
        }

        private void BeginMovement()
        {
            _isMoving = true;
            _canDialogue = false;
            SetAnimation(walking: true);
        }

        private void EndMovement()
        {
            CurrentIndexChangedEvent?.Invoke(++_currentIndex);
            _isMoving = false;
            _canDialogue = true;
            SetAnimation(walking: false);
        }

        private void SetAnimation(bool walking)
        {
            _animator.SetBool(Idle, !walking);
            _animator.SetBool(Walk, walking);
        }
    }

    [Serializable]
    public class Waypoints
    {
        public Transform[] waypoints;
    }
}