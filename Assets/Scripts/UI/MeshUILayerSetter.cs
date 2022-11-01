using UnityEngine;

namespace UI
{
    public class MeshUILayerSetter : MonoBehaviour
    {
        private const string LayerUIName = "UI";
        
        public void SetUILayer(GameObject target)
        {
            int targetLayer = LayerMask.NameToLayer(LayerUIName);
            
            target.gameObject.layer = targetLayer;
            MeshRenderer[] children = target.GetComponentsInChildren<MeshRenderer>();
            
            foreach (var child in children)
                child.gameObject.layer = targetLayer;
        }
    }
}
