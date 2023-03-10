using Interfaces;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class StandaloneInputService : IInputService
    {
        private const string HorizontalConst = "Horizontal";
        private const string VerticalConst = "Vertical";
        private const string MouseYConst = "Mouse Y";
        private const string MouseXConst = "Mouse X";

        public float Horizontal
            => UnityEngine.Input.GetAxisRaw(HorizontalConst);

        public float Vertical
            => UnityEngine.Input.GetAxisRaw(VerticalConst);

        public float MouseY
            => UnityEngine.Input.GetAxisRaw(MouseYConst);

        public float MouseX
            => UnityEngine.Input.GetAxisRaw(MouseXConst);

        public bool IsAttackButtonPressed()
            => UnityEngine.Input.GetMouseButtonDown(0);

        public bool IsInteractButtonPressed()
            => UnityEngine.Input.GetKeyDown(KeyCode.E);


        public bool IsJumped()
            => UnityEngine.Input.GetKeyDown(KeyCode.Space);

        public bool IsSprinting()
            => Vertical > 0.02f && UnityEngine.Input.GetKey(KeyCode.LeftShift);
    }
}