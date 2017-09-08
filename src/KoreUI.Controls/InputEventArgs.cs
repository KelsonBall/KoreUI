using OpenTK.Input;
using Processing.OpenTk.Core.Math;
using System.Collections.Generic;

namespace KoreUI.Controls
{
    public class InputEventArgs
    {
        public PVector Mouse { get; set; }
        public PVector PMouse { get; set; }
        public Dictionary<MouseButton, bool> MouseButtons { get; } = new Dictionary<MouseButton, bool>();
        public KeyboardState KeyboardState { get; set; }
        public char PressedKey { get; set; }
        public char ReleasedKey { get; set; }
        public double MouseWheelDelta { get; set; }
    }
}
