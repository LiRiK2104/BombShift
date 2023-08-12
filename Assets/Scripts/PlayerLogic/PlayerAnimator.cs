using System;
using RoundLogic;
using UnityEngine;
using Zenject;

namespace PlayerLogic
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private static readonly int FreezeTrigger = Animator.StringToHash("Freeze");

        private RoundRunner _roundRunner;
        private Animator _animator;
        
        
        [Inject]
        private void Construct(RoundRunner roundRunner)
        {
            _roundRunner = roundRunner;
        }
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _roundRunner.Starter.RoundStarted += FreezeAnimation;
        }

        private void OnDisable()
        {
            _roundRunner.Starter.RoundStarted -= FreezeAnimation;
        }

        private void FreezeAnimation()
        {
            _animator.SetTrigger(FreezeTrigger);
        }
    }
}
