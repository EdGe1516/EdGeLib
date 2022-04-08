using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using EdGeLib.Scenes;
using Microsoft.Xna.Framework;

namespace EdGeLib.Systems
{
    public class KeyInputEventArgs : EventArgs
    {
        public Keys PressedKey { get; set; }

        public KeyInputEventArgs(Keys pressedKey)
        {
            PressedKey = pressedKey;
        }
    }
    public class KeyCommand
    {
        public Keys Key { get; private set; }
        public event EventHandler<KeyInputEventArgs> Pressed;

        public KeyCommand(Keys key)
        {
            Key = key;
        }

        public void Press()
        {
            Pressed?.Invoke(this, new KeyInputEventArgs(Key));
        }
    }
    public class KeyboardInputSystem : EntityComponentSystem
    {
        public KeyboardState KeyboardState;
        public KeyboardState KeyboardStateLast;

        public KeyboardInputSystem(Scene scene)
            : base(scene)
        {
            KeyboardStateLast = Keyboard.GetState();
        }

        public override void Go(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();

            Keys[] downKeys = KeyboardState.GetPressedKeys();
            if (downKeys.Length > 0)
            {
                if (Scene.FocusedEntity != null)
                {
                    if (!Scene.FocusedEntity.Input.HasFocus)
                    {
                        Scene.FocusedEntity = null;
                    }
                    else
                    {
                        foreach (KeyCommand keyCommand in Scene.FocusedEntity.Input.KeyCommands)
                        {
                            if (downKeys.Contains(keyCommand.Key) &&
                                KeyboardStateLast.IsKeyUp(keyCommand.Key))
                            {
                                keyCommand.Press();
                            }
                        }
                    }
                }
            }
            KeyboardStateLast = KeyboardState;
        }
    }
}
