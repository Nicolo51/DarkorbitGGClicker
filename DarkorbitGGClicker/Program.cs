using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Input; 

namespace DarkorbitGGClicker
{
    class Program
    {
        static BotState State;
        static Point buttonPosition;
        static Point checkBoxPosition;
        static Mouse mouse;
        static bool IsSelectingPos;
        static bool flagStop;
        static int rate; 
        static void Main(string[] args)
        {
            IsSelectingPos = true;
            string rateStr = "";
            bool isNumber = false; 
            Console.WriteLine("Please select the rate (in ms) at which the bot should click"); 
            while(!isNumber)
            {
                rateStr = Console.ReadLine();
                if (int.TryParse(rateStr, out rate))
                    isNumber = true; 
            }
            Console.WriteLine("Press Enter when your DO window is ready");
            Console.ReadLine(); 
            mouse = new Mouse();
            mouse.OnMouseInput += OnMouseInput;
            Console.WriteLine("Click on the button...");
            while (IsSelectingPos)
            {
                Thread.Sleep(100); 
            }
            while(!flagStop)
            {
                mouse.SetMousePosition(buttonPosition);
                Thread.Sleep(rate);
                mouse.SimMouseInput(MouseButton.LeftClick);
                Thread.Sleep(rate);
                mouse.SetMousePosition(checkBoxPosition);
                Thread.Sleep(rate);
                mouse.SimMouseInput(MouseButton.LeftClick);
                Thread.Sleep(rate);
            }
        }

        public static void OnMouseInput(Point position, MouseButton button)
        {
            if(button == MouseButton.LeftClick) {
                if (State == BotState.WaitingForButtonPosition)
                { 
                    buttonPosition = position;
                    State = BotState.WaitingforCheckBoxPosition;
                    Console.WriteLine("Click on the checkbox...");
                    return; 
                }
                if (State == BotState.WaitingforCheckBoxPosition)
                {
                    State = BotState.Running;
                    checkBoxPosition = position;
                    IsSelectingPos = false; 
                    Console.WriteLine("Now running press middle click to stop. ");
                    return;
                }
            }
            if(button == MouseButton.MiddleClick)
            {
                mouse.Stop();
                flagStop = true; 
            }
        }

        public enum BotState
        {
            WaitingForButtonPosition,
            WaitingforCheckBoxPosition,
            Running, 
        }
    }
}
