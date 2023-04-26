using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.GrayscaleLogic
{
    public class GrayscaleGroup : MonoBehaviour
    {
        private bool _isInitialized;
        private List<Grayscale> _grayscales;
        
        public bool IsGrayAll
        {
            get
            {
                if (_isInitialized == false)
                    Initialize();
                
                int isNotGrayCount = _grayscales.Count(grayscale => grayscale.IsGray == false);
                return isNotGrayCount == 0;
            }
        
            set
            {
                if (_isInitialized == false)
                    Initialize();
                
                _grayscales.ForEach(grayscale => grayscale.IsGray = value);
            }
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized)
                return;
            
            _grayscales = GetComponentsInChildren<Grayscale>().ToList();
            
            if (TryGetComponent(out Grayscale myGrayscale))
                _grayscales.Add(myGrayscale);

            _isInitialized = true;
        }
    }
}
