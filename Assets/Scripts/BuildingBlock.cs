using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuildingBlock : MonoBehaviour
{
    [SerializeField] private FracturedBuildingBlock _fracturedBlock;

    public void DestructSelf()
    {
        if (_fracturedBlock == null)
            return;
        
        
        var block = Instantiate(_fracturedBlock, transform.position, transform.rotation, transform.parent);
        block.transform.localScale = transform.localScale;

        var meshRenderer = GetComponent<MeshRenderer>();
        
        var pieces = block.GetComponentsInChildren<Rigidbody>();

        float force = 50f;
        float radius = 5;
        
        foreach (var piece in pieces)
        {
            var pieceMeshRenderer = piece.GetComponent<MeshRenderer>();
            pieceMeshRenderer.material = meshRenderer.material;
            
            piece.AddExplosionForce(force, transform.position, radius);
        }
        
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        DestructSelf();
    }
}
