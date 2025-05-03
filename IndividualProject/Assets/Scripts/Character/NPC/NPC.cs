using Character.NPC.Presenters;
using UnityEngine;

namespace Character.NPC
{
    public abstract class NPC : Character
    {
        [Header("Settings")]
        public string npcName;

        [SerializeField] protected bool _needInvert = true;

        [Header("References")] [SerializeField]
        protected NPCInfoPresenter _npcInfoPresenter;

        [SerializeField] private Transform _cameraTransform;

        public Transform CameraTransform
        {
            get => _cameraTransform;
            set => _cameraTransform = value;
        }
        public NPCInfoPresenter NPCInfoPresenter => _npcInfoPresenter;

        public bool NeedInvert
        {
            get => _needInvert;
            set => _needInvert = value;
        }
        
        protected override void Awake()
        {
            base.Awake();
            
            _npcInfoPresenter.SetNameText();
        }

        protected virtual void Update()
        {
            _npcInfoPresenter.UpdateNPCInfoRotation(_cameraTransform, _needInvert);
        }
    }
}