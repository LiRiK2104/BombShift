using Helpers;
using UnityEngine;

namespace DataBaseSystem
{
    public class DataBase : MonoBehaviour
    {
        [SerializeField] private DataBaseCore _core;

        public DataBaseCore Core => _core;
    }
}
