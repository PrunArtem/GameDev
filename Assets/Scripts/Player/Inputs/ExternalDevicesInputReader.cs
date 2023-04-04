using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class ExternalDevicesInputReader : IEntityInputSource
    {
        public float Direction => Input.GetAxisRaw("Horizontal");

        public bool Jump { get; private set; }

        public bool Attack { get; private set; }

        private bool IsPointesOverUI() => EventSystem.current.IsPointerOverGameObject();
        public void OnUpdate()
        {
            if (Input.GetButton("Jump"))
                Jump = true;
            if (!IsPointesOverUI() && Input.GetButton("Fire1"))
                Attack = true;
        }

        public void ResetOneTimeAction()
        {
            Jump = false;
            Attack = false;
        }
    }
}