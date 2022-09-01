using System;
using System.Linq;
using UnityEngine;

public class SpiritSetter : MonoBehaviour
{
    [SerializeField] private Spirit _spiritQuad;
    [SerializeField] private SpiritBox _spiritBox;

    public event Action<Vector3> SpiritPointFound;

    private void OnEnable()
    {
        Gate.Passed += SetSpirit;
        ChunkSpawner.Instance.GateSpawner.GateSpawned += SetSpirit;
        Player.Instance.Died += Hide;
    }

    private void OnDisable()
    {
        Gate.Passed -= SetSpirit;
        ChunkSpawner.Instance.GateSpawner.GateSpawned -= SetSpirit;
        Player.Instance.Died -= Hide;
    }

    private void Start()
    {
        SetSpirit();
    }

    private void Show()
    {
        if (Player.Instance.Alive == false)
            return;
        
        _spiritBox.gameObject.SetActive(true);
        _spiritQuad.gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        _spiritQuad.gameObject.SetActive(false);
        _spiritBox.gameObject.SetActive(false);
    }

    private void SetSpirit()
    {
        if (TryFindSpiritPoint(out Transform spiritPoint))
        {
            Show();
            _spiritQuad.UpdateTargetPosition(spiritPoint.position);
        }
        else
        {
            Hide();
        }
    }

    private bool TryFindSpiritPoint(out Transform point)
    {
        point = null;

        float sideHalfSize = 3;
        float searchDistance = 100;
        int layerMask = LayerMask.GetMask("Spirit Point");
        Vector3 boxHalfSize = new Vector3(sideHalfSize, sideHalfSize, sideHalfSize);

        RaycastHit[] hits = Physics.BoxCastAll(transform.position, boxHalfSize, 
            Vector3.forward, Quaternion.identity, searchDistance, layerMask);

        if (hits.Length > 0)
        {
            RaycastHit nearestHit = hits.OrderBy(hit => hit.distance).First();
            point = nearestHit.transform;
            SpiritPointFound?.Invoke(point.position);
            return true;
        }

        return false;
    }
}
