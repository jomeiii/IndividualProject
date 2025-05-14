using UnityEngine;

namespace Controllers
{
    public class MillAnimationController : MonoBehaviour
    {
        [SerializeField] private float _speed = 100f;

        private void Update()
        {
            transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
        }
    }
}