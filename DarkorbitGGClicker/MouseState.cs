using System;
using System.Collections.Generic;
using System.Text;

namespace DarkorbitGGClicker
{
    public struct MouseState
    {
        public bool IsLeftClickPressed { get; set; }
        public bool IsMiddleClickPressed { get; set; }
        public bool IsRightClickPressed { get; set; }

        public override bool Equals(object obj)
        {
            return obj is MouseState mouseState && IsLeftClickPressed == mouseState.IsLeftClickPressed &&
                   IsRightClickPressed == mouseState.IsRightClickPressed &&
                   IsMiddleClickPressed == mouseState.IsMiddleClickPressed;
        }
    }
}
