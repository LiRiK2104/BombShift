using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.GrayscaleLogic
{
    public class GrayscaleGroup : MonoBehaviour
    {
        private List<Grayscale> _grayscales = new List<Grayscale>();

        public bool IsGrayAll
        {
            get
            {
                int isNotGrayCount = _grayscales.Count(grayscale => grayscale.IsGray == false);
                return isNotGrayCount == 0;
            }
        
            set
            {
                _grayscales.ForEach(grayscale => grayscale.IsGray = value);
            }
        }
    
        private void Awake()
        {
            _grayscales = GetComponentsInChildren<Grayscale>().ToList();
        
            if (TryGetComponent(out Grayscale myGrayscale))
                _grayscales.Add(myGrayscale);
        }
    }
}
