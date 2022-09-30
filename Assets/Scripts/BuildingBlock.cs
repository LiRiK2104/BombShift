using UnityEngine;

public class BuildingBlock : MonoBehaviour
{
    public virtual void DestructSelf()
    {
        Destroy(gameObject);
    }
}
