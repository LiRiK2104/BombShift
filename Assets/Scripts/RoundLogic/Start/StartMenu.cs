using System;
using UnityEngine;
using UnityEngine.UI;

namespace RoundLogic.Start
{
    [RequireComponent(typeof(Animator), typeof(CanvasGroup))]
    public class StartMenu : MonoBehaviour
    {
        private static readonly int CloseTrigger = Animator.StringToHash(StartMenuAnimator.Triggers.Close);
        
        private Animator _animator;
        private Button[] _buttons;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _buttons = GetComponentsInChildren<Button>();
        }

        public void OnStart()
        {
            foreach (var button in _buttons)
                button.interactable = false;
            
            _animator.SetTrigger(CloseTrigger);
        }
    }

    public static class StartMenuAnimator
    {
        public static class Triggers
        {
            public const string Close = nameof(Close);
        }
    } 
}
