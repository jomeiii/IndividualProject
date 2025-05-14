using System;
using Character.Player;
using UnityEngine;

namespace Controllers
{
    public class GoldenKeyController : MonoBehaviour
    {
        public event Action GoldenKeyPressedEvent;
        
        [SerializeField] private float _speedRotation;
        [SerializeField] private float _amplitude;
        
        [SerializeField] private TriggerController _triggerController;

        private Transform _transform;
        private Vector3 _startPosition;

        private void OnEnable()
        {
            _triggerController.OnTriggerEnterEvent += TriggerEnter;
        }

        private void OnDisable()
        {
            _triggerController.OnTriggerEnterEvent -= TriggerEnter;
        }

        private void Awake()
        {
            _transform = transform;
            _startPosition = _transform.position;
        }

        private void Update()
        {
            _transform.Rotate(Vector3.up, _speedRotation * Time.deltaTime);

            float yOffset = Mathf.Sin(Time.time) * _amplitude;
            transform.position = _startPosition + Vector3.up * yOffset;
        }

        private void TriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                GoldenKeyPressedEvent?.Invoke();
                Destroy(gameObject);
            }   
        }
    }
}