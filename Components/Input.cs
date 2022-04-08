using System;
using System.Collections.Generic;
using EdGeLib.Systems;

namespace EdGeLib.Components
{
    public class Input : Component
    {
        public bool MouseEnabled { get; set; }
        public MouseInputState MouseInputState { get; set; }
        public event EventHandler<MouseInputEventArgs> MouseEnter;
        public event EventHandler<MouseInputEventArgs> MouseHover;
        public event EventHandler<MouseInputEventArgs> MouseExit;
        public event EventHandler<MouseInputEventArgs> MouseDownBegin;
        public event EventHandler<MouseInputEventArgs> MouseDown;
        public event EventHandler<MouseInputEventArgs> Drag;
        public event EventHandler<MouseInputEventArgs> Drop;
        public event EventHandler<MouseInputEventArgs> Click;

        public List<KeyCommand> KeyCommands { get; set; }
        private bool hasFocus;
        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                if (hasFocus != value)
                {
                    hasFocus = value;
                    FocusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public event EventHandler FocusChanged;

        public bool CanMoveOnDrag { get; set; }

        public Input(Entity entity)
            : base(entity)
        {
            MouseEnabled = false;
            MouseInputState = MouseInputState.Idle;
            KeyCommands = new List<KeyCommand>();
            hasFocus = false;
            CanMoveOnDrag = false;
        }

        public void OnMouseEnter(MouseInputEventArgs m)
        {
            MouseInputState = MouseInputState.MouseHover;
            MouseEnter?.Invoke(this, m);
        }

        public void OnMouseHover(MouseInputEventArgs m)
        {
            MouseHover?.Invoke(this, m);
        }

        public void OnMouseExit(MouseInputEventArgs m)
        {
            MouseInputState = MouseInputState.Idle;
            MouseExit?.Invoke(this, m);
        }

        public void OnMouseDownBegin(MouseInputEventArgs m)
        {
            MouseInputState = MouseInputState.MouseDown;
            MouseDownBegin?.Invoke(this, m);
        }

        public void OnMouseDown(MouseInputEventArgs m)
        {
            MouseDown?.Invoke(this, m);
        }

        public void OnDrag(MouseInputEventArgs m)
        {
            if (MouseInputState == MouseInputState.MouseDown)
            {
                MouseInputState = MouseInputState.Dragging;
            }
            if (CanMoveOnDrag)
            {
                Entity.Graphics.Position = m.MousePosition + (m.MouseDownEntityPosition - m.MouseDownMousePosition);
            }
            Drag?.Invoke(this, m);
        }

        public void OnDrop(MouseInputEventArgs m)
        {
            MouseInputState = MouseInputState.MouseHover;
            Drop?.Invoke(this, m);
        }

        public void OnClick(MouseInputEventArgs m)
        {
            MouseInputState = MouseInputState.MouseHover;
            Click?.Invoke(this, m);
        }
    }
}
