using System;
using System.Collections;
using System.Collections.Generic;
using Shop.Items;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChestReward : MonoBehaviour
{
    [SerializeField] private List<Chest> _chestsTemplates = new List<Chest>();
    [SerializeField] private int _chestIndex;
    [Space] 
    [SerializeField] private Item _itemTemplate;

    [SerializeField] private Transform _chestPoint;
    [SerializeField] private Transform _itemPoint;
    [SerializeField] private Light _lightPoint;
    
    private static readonly int PullOut = Animator.StringToHash(RewardPointAnimator.Triggers.PullOut);
    
    private Animator _animator;
    private Chest _chest;
    private Item _item;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        CreateChest();
        SetColorToLightPont();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Open();
        }
    }

    private void OnValidate()
    {
        _chestIndex = Mathf.Clamp(_chestIndex, 0, _chestsTemplates.Count - 1);
    }

    private void Open()
    {
        CreateItem();
        _chest.Open();
        PullOutItem();
    }

    private void CreateChest()
    {
        _chest = Instantiate(_chestsTemplates[_chestIndex], _chestPoint.position, _chestPoint.rotation, _chestPoint);
    }

    private void SetColorToLightPont()
    {
        _lightPoint.color = _chest.LightColor;
    }

    private void CreateItem()
    {
        _item = Instantiate(_itemTemplate, _itemPoint.position, _itemPoint.rotation, _itemPoint);
    }

    private void PullOutItem()
    {
        _animator.SetTrigger(PullOut);
    }
}

public class RewardPointAnimator
{
    public class Triggers
    {
        public const string PullOut = "PullOut";
    }
}
