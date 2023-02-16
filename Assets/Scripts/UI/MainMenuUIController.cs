using UnityEngine;
using UnityEngine.UIElements;

namespace Immerse.BfhClient.UI
{
    public class MainMenuUIController : MonoBehaviour
    {
        private Button _joinButton;

        private void Start()
        {
            var root = GetComponent<UIDocument>().rootVisualElement;
        
            _joinButton = root.Q<Button>("Join");
            _joinButton.clicked += JoinButtonPressed;
        }

        private void JoinButtonPressed()
        {
            SceneManager.Load(SceneManager.Instance.JoinMenu);
        }
    }
}
