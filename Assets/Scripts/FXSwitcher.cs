using System.Collections.Generic;
using UnityEngine;

public class FXSwitcher : MonoBehaviour
{
    [SerializeField] private Transform _originPoint;
    [SerializeField] private List<FX> _effectsTemplates = new List<FX>();
    
    private List<FX> _createdEffects = new List<FX>();

    private void OnEnable()
    {
        SpeedSetter.Instance.SpeedChanged += UpdateEffects;
    }

    private void OnDisable()
    {
        SpeedSetter.Instance.SpeedChanged -= UpdateEffects;
    }
    
    private void UpdateEffects()
    {
        foreach (var template in _effectsTemplates)
        {
            if (SpeedSetter.Instance.Setting.HasEffect(template))
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
