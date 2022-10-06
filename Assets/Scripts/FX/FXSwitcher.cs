using System;
using System.Collections.Generic;
using PlayerLogic;
using UnityEngine;

namespace FX
{
    [RequireComponent(typeof(Player))]
    public class FXSwitcher : MonoBehaviour
    {
        [SerializeField] private Transform _originPoint;
        [SerializeField] private List<FX> _effectsTemplates = new List<FX>();
    
        private List<FX> _createdEffects = new List<FX>();
        private Player _player;


        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            _player.SpeedSwitcher.SpeedChanged += UpdateEffects;
            _player.LifeSwitcher.LifeChanged += UpdateEffects;
        }

        private void OnDisable()
        {
            _player.SpeedSwitcher.SpeedChanged -= UpdateEffects;
            _player.LifeSwitcher.LifeChanged -= UpdateEffects;
        }
    
        private void UpdateEffects()
        {
            if (_player.SpeedSwitcher.Setting == null || 
                _player.LifeSwitcher.Setting == null)
                return;
        
            foreach (var template in _effectsTemplates)
            {
                if (_player.SpeedSwitcher.Setting.HasEffect(template) || 
                    _player.LifeSwitcher.Setting.HasEffect(template))
                    GetEffect(template).Play();
                else
                    GetEffect(template).Stop();
            }
        }

        private FX GetEffect(FX template)
        {
            FX foundEffect = null;

            foreach (var effect in _createdEffects)
            {
                if (foundEffect == null && effect.Prefab == template)
                    foundEffect = effect;
            }

            if (foundEffect == null)
                foundEffect = CreateEffect(template);

            return foundEffect;
        }
    
        private FX CreateEffect(FX template)
        {
            var effect = Instantiate(template, _originPoint.position, Quaternion.identity, _originPoint);
            effect.OnCreate(template);
            _createdEffects.Add(effect);

            return effect;
        }
    }

    public interface IWasCreatedFrom<T>
    {
        public T Prefab { get; }
        public void OnCreate(T prefab);
    }
}