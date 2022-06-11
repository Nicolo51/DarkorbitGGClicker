using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace DarkorbitGGClicker
{
    public class Mouse
    {
        MouseState currentMouseState; 
        MouseState previousMouseState;
        private Thread thread; 
        public delegate void MouseClickEventHandler(Point position, MouseButton mouseButton);
        public event MouseClickEventHandler OnMouseInput;
        private bool running; 

        public Mouse()
        {
            currentMouseState = GetState();
            previousMouseState = GetState(); 
            thread = new Thread(new ThreadStart(Update));
            thread.Start();
            running = true; 
        }

        public Point GetMousePosition()
        {
            var position = MouseOperations.GetCursorPosition();
            return new Point(position.X, position.Y); 
        }
        public void SetMousePosition(Point position)
        {
            MouseOperations.SetCursorPosition(position.X, position.Y); 
        }

        public void SetMousePosition(int x, int y)
        {
            SetMousePosition(new Point(x, y)); 
        }

        public void SimMouseInput(MouseButton mouseButton)
        {
            switch (mouseButton)
            {
                case MouseButton.LeftClick:
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
                    Thread.Sleep(50); 
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
                    break;
                case MouseButton.RightClick:
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
                    Thread.Sleep(50);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
                    break;
                case MouseButton.MiddleClick:
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.MiddleDown);
                    Thread.Sleep(50);
                    MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.MiddleUp);
                    break;

            }
        }

        public MouseState GetState()
        {
            return new MouseState()
            {
                IsRightClickPressed = MouseOperations.GetAsyncKeyState((ushort)MouseButton.RightClick) == 0 ? false : true,
                IsLeftClickPressed = MouseOperations.GetAsyncKeyState((ushort)MouseButton.LeftClick) == 0 ? false : true,
                IsMiddleClickPressed = MouseOperations.GetAsyncKeyState((ushort)MouseButton.MiddleClick) == 0 ? false : true,
            };
        }

        private void Update()
        {

            while (running)
            {
                previousMouseState = currentMouseState; 
                var pos = this.GetMousePosition();
                currentMouseState = GetState(); 

                if (currentMouseState.Equals(previousMouseState))
                    continue; 

                if (currentMouseState.IsRightClickPressed && !previousMouseState.IsRightClickPressed)
                    OnMouseInput?.Invoke(pos, MouseButton.RightClick);

                if (currentMouseState.IsLeftClickPressed && !previousMouseState.IsLeftClickPressed)
                    OnMouseInput?.Invoke(pos, MouseButton.LeftClick);

                if (currentMouseState.IsMiddleClickPressed && !previousMouseState.IsMiddleClickPressed)
                    OnMouseInput?.Invoke(pos, MouseButton.MiddleClick);
                Thread.Sleep(5);
            }
        }

        public void Stop()
        {
            running = false;
        } 
    }
    public enum MouseButton : UInt16
    {
        LeftClick = 0x01, 
        RightClick = 0x02, 
        MiddleClick = 0x04, 
    }
}
