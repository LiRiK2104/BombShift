using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dev
{
    [RequireComponent(typeof(Button))]
    public class DevConsoleOpenButton : MonoBehaviour
    {
        [Inject] private DevConsole _devConsole;
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();

#if UNITY_EDITOR
            _button.onClick.AddListener(_devConsole.Open);
            _devConsole.Close();
#else
            gameObject.SetActive(false);
#endif
        }
    }
}
