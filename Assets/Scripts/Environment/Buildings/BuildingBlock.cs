using UnityEngine;

namespace Environment.Buildings
{
    public class BuildingBlock : MonoBehaviour
    {
        public virtual void DestructSelf()
        {
            Destroy(gameObject);
        }
    }
}
