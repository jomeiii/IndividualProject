using Character.NPC.Presenters;
using UnityEngine;

namespace Character.NPC
{
    public abstract class NPC : Character
    {
        [Header("Settings")]
        public string npcName;

        [Header("References")] [SerializeField]
        protected NPCInfoPresenter _npcInfoPresenter;
        [SerializeField] private Transform _cameraTransform;

        public Transform CameraTransform => _cameraTransform;
        
        protected override void Awake()
        {
            base.Awake();
            
            _npcInfoPresenter.SetNameText();
        }

        protected virtual void Update()
        {
            _npcInfoPresenter.UpdateNPCInfoRotation();
        }
    }
}