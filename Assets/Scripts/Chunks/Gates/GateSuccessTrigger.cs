using Helpers;
using PlayerLogic;
using UnityEngine;

namespace Chunks.Gates
{
    public class GateSuccessTrigger : MonoBehaviour
    {
        private Gate _gate;
        private bool _isTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered == false && other.gameObject.TryGetComponentInParent(out PlayerMover playerMover))
            {
                _isTriggered = true;
                _gate.OnPass();
            }
        }

        public void Init(Gate gate)
        {
            _gate = gate;
        }
    }
}
