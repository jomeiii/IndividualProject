using System.Diagnostics;
using Character.NPC.Presenters;
using UnityEngine;

namespace Character.NPC
{
    public abstract class NPC : Character
    {
        protected static readonly int Idle = Animator.StringToHash("Idle");
        protected static readonly int Walk = Animator.StringToHash("Walk");
        
        [Header("Settings")]
        public string npcName;

        [SerializeField] protected bool _needInvert = true;

        [Header("References")] [SerializeField]
        protected NPCInfoPresenter _npcInfoPresenter;

        [SerializeField] private Transform _cameraTransform;
        
        protected Animator _animator;

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
            _animator = GetComponent<Animator>();
            _npcInfoPresenter.SetNameText();
        }

        protected virtual void Update()
        {
            _npcInfoPresenter.UpdateNPCInfoRotation(_cameraTransform, _needInvert);
        }
    }
}