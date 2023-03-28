using Infrastructure.Interfaces;
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

        public bool IsInventoryButtonPressed()
            => UnityEngine.Input.GetKeyDown(KeyCode.I);


        public bool IsJumped()
            => UnityEngine.Input.GetKeyDown(KeyCode.Space);

        public bool IsSprinting()
            => Vertical > 0.02f && UnityEngine.Input.GetKey(KeyCode.LeftShift);

        public bool IsSwitchButtonPressed(out int i)
        {
            i = 0;
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                i = 1;
                return true;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
            {
                i = 2;
                return true;
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
            {
                i = 3;
                return true;
            }

            return false;
        }
    }
}