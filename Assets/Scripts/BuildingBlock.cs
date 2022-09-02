using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuildingBlock : MonoBehaviour
{
    [SerializeField] private FracturedBuildingBlock _fracturedBlock;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void DestructSelf()
    {
        if (_fracturedBlock == null)
            return;
        
        
        var block = Instantiate(_fracturedBlock, transform.position, transform.rotation, transform.parent);
        block.transform.localScale = transform.localScale;
        
        var pieces = block.GetComponentsInChildren<Rigidbody>();

        float force = 50f;
        float radius = 5;
        
        foreach (var piece in pieces)
        {
            //piece.velocity = _rigidbody.velocity;
            piece.AddExplosionForce(force, transform.position, radius);
        }
        
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        DestructSelf();
    }
}
