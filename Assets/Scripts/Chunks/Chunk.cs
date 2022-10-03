using UnityEngine;

namespace Chunks
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private Transform _endPoint;
        [SerializeField] private Transform _gateSpawnPoint;

        public Transform EndPoint => _endPoint;
        public Transform GateSpawnPoint => _gateSpawnPoint;
    }
}
