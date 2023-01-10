using ProjectSolitude.Interfaces;
using UnityEngine;

namespace ProjectSolitude.Inputs
{
    public class StandaloneInputService : IInputService
    {
        private const string HorizontalConst = "Horizontal";
        private const string VerticalConst = "Vertical";
        public float Horizontal 
            => Input.GetAxisRaw(HorizontalConst);
        public float Vertical 
            => Input.GetAxisRaw(VerticalConst);

        public bool IsJumped()
            => Input.GetKeyDown(KeyCode.Space);

        public bool IsSprinting() 
            => Vertical > 0.02f && Input.GetKey(KeyCode.LeftShift);
    }
}
