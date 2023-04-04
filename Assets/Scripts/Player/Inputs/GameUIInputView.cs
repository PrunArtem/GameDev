using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    class GameUIInputView : MonoBehaviour, IEntityInputSource
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Button _attackButton;

        public float Direction => GetDirection();

        private bool jump;
        public bool Jump { 
            get         { return jump; } 
            private set { jump = _joystick.Vertical > 0.4; } 
        }

        public bool Attack { get; private set; }

        private float GetDirection()
        {
            if (_joystick.Horizontal < -0.2)
                return -1;
            else if (_joystick.Horizontal > 0.2)
                return 1;
            else return 0;
        }

        public void ResetOneTimeAction()
        {
            Jump = false;
            Attack = false;
        }

        private void Awake()
        {
            _attackButton.onClick.AddListener(()=> Attack = true);
        }

        private void OnDestroy()
        {
            _attackButton.onClick.RemoveAllListeners();
        }
    }
}
