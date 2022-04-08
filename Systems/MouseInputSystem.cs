using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using EdGeLib.Scenes;

namespace EdGeLib.Systems
{
    public class MouseInputEventArgs : EventArgs
    {
        public Vector2 MousePosition { get; set; }
        public Vector2 MouseDownMousePosition { get; set; }
        public Vector2 MouseDownEntityPosition { get; set; }

        public MouseInputEventArgs(Vector2 mousePosition, Vector2 mouseDownMousePosition, Vector2 mouseDownEntityPosition)
        {
            MousePosition = mousePosition;
            MouseDownMousePosition = mouseDownMousePosition;
            MouseDownEntityPosition = mouseDownEntityPosition;
        }
    }
    public enum MouseInputState
    {
        Idle,
        MouseHover,
        MouseDown,
        Dragging,
    }
    public class MouseInputSystem : EntityComponentSystem
    {
        private MouseState mouseState;
        private MouseState mouseStateLast;

        private MouseInputEventArgs MouseInputEventArgs
        {
            get
            {
                return new MouseInputEventArgs(
                    mouseState.Position.ToVector2(),
                    mouseDownMousePosition,
                    mouseDownEntityPosition);
            }
        }

        private readonly float clickDragDistance;
        private Vector2 mouseDownMousePosition;
        private Vector2 mouseDownEntityPosition;

        private Entity mouseoverEntity;

        public MouseInputSystem(Scene scene)
            :base(scene)
        {
            mouseStateLast = Mouse.GetState();
            clickDragDistance = 3f;
            mouseDownMousePosition = Vector2.Zero;
            mouseDownEntityPosition = Vector2.Zero;
            mouseoverEntity = null;
        }

        public override void Go(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            //mouseover detected last frame
            if (mouseoverEntity != null)
            {
                //check if still mouseover
                //if mouseover and has children check children
                //and change check to child
                if ((mouseoverEntity.Graphics.Bounds.Contains(mouseState.Position) ||
                    //change this to mouseoverEntity.Input.MouseInputState.Dragging?
                    (mouseoverEntity.Input.CanMoveOnDrag && mouseState.LeftButton == ButtonState.Pressed)))
                {
                    if (mouseoverEntity.Family.Children.Count > 0)
                    {
                        foreach (Entity child in mouseoverEntity.Family.Children)
                        {
                            if (child.Graphics.Bounds.Contains(mouseState.Position) &&
                                child.Graphics.Visible && child.Input.MouseEnabled)
                            {
                                mouseoverEntity = child;
                                mouseoverEntity.Input.OnMouseEnter(MouseInputEventArgs);
                                //try break;
                                break;
                            }
                        }
                    }
                    if (mouseoverEntity.Input.MouseInputState == MouseInputState.MouseHover)
                    {
                        if (mouseStateLast.LeftButton == ButtonState.Released &&
                            mouseState.LeftButton == ButtonState.Pressed)
                        {
                            mouseDownMousePosition = mouseState.Position.ToVector2();
                            mouseDownEntityPosition = mouseoverEntity.Graphics.Position;
                            mouseoverEntity.Input.OnMouseDownBegin(MouseInputEventArgs);
                        }
                        else
                        {
                            mouseoverEntity.Input.OnMouseHover(MouseInputEventArgs);
                        }
                    }
                    else if (mouseoverEntity.Input.MouseInputState == MouseInputState.MouseDown)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            if (Vector2.Distance(mouseDownMousePosition, mouseState.Position.ToVector2()) > clickDragDistance)
                            {
                                mouseoverEntity.Input.OnDrag(MouseInputEventArgs);
                            }
                            else
                            {
                                mouseoverEntity.Input.OnMouseDown(MouseInputEventArgs);
                            }
                        }
                        else
                        {
                            mouseoverEntity.Input.OnClick(MouseInputEventArgs);
                            if (mouseoverEntity.Family.Parent != null)
                            {
                                mouseoverEntity.Family.Parent.Input.OnClick(MouseInputEventArgs);
                            }
                            //this check is questionable - entity could have focus waiting for more mouse commands not just keycommands
                            if (mouseoverEntity.Input.KeyCommands.Count > 0)
                            {
                                mouseoverEntity.Input.HasFocus = true;
                                Scene.FocusedEntity = mouseoverEntity;
                            }
                        }
                    }
                    else if (mouseoverEntity.Input.MouseInputState == MouseInputState.Dragging)
                    {
                        if (mouseState.LeftButton == ButtonState.Pressed)
                        {
                            mouseoverEntity.Input.OnDrag(MouseInputEventArgs);
                        }
                        else
                        {
                            mouseoverEntity.Input.OnDrop(MouseInputEventArgs);
                        }
                    }
                }
                else
                {
                    mouseoverEntity.Input.OnMouseExit(MouseInputEventArgs);
                    if (mouseoverEntity.Family.Parent != null)
                    {
                        mouseoverEntity = mouseoverEntity.Family.Parent;
                    }
                    else
                    {
                        mouseoverEntity = null;
                    }
                }
            }
            //check if mouseover
            else
            {
                for (int i = 0; i < Scene.Entities.Length; i++)
                {
                    if (mouseoverEntity == null)
                    {
                        foreach (Entity e in Scene.Entities[i])
                        {
                            if (e.Input.MouseEnabled && e.Graphics.Visible && e.Family.Parent == null && e.Graphics.Bounds.Contains(mouseState.Position))
                            {
                                mouseoverEntity = e;
                                e.Input.OnMouseEnter(MouseInputEventArgs);
                                break;
                            }
                        }
                        if (mouseoverEntity != null)
                        {
                            Scene.Entities[i].Remove(mouseoverEntity);
                            Scene.Entities[i].Add(mouseoverEntity);
                            if (mouseoverEntity.Family.Children.Count > 0)
                            {
                                foreach (Entity dependant in mouseoverEntity.Family.Dependants())
                                {
                                    Scene.Entities[i].Remove(dependant);
                                    Scene.Entities[i].Add(dependant);
                                }
                            }
                        }
                    }
                }
            }

            mouseStateLast = mouseState;
        }
    }
}
