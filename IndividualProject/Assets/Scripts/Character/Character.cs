using UnityEngine;

namespace Character
{
    public abstract class Character : MonoBehaviour
    {
        protected Transform _transform;
        
        protected virtual void Awake()
        {
            _transform = transform;
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
            
        }
    }
}