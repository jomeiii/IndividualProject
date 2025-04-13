using System;
using UnityEngine;

namespace Controllers
{
    public class ColliderController : MonoBehaviour
    {
        public event Action<Collision> TriggerEnterEvent;
        public event Action<Collision> TriggerExitEvent;
        public event Action<Collision> TriggerStayEvent;

        private void OnCollisionEnter(Collision collision)
        {
            TriggerEnterEvent?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            TriggerExitEvent?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            TriggerStayEvent?.Invoke(collision);
        }
    }
}