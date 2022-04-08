using Microsoft.Xna.Framework;
using EdGeLib.Scenes;
using EdGeLib.Systems;

namespace EdGeLib.GUI
{
    public class SkinSystem : EntityComponentSystem
    {
        public Color BackgroundColorIdle { get; set; }
        public Color BackgroundColorMouseover { get; set; }
        public Color BackgroundColorMouseDown { get; set; }
        public Color TextColorIdle { get; set; }
        public Color TextColorMouseover { get; set; }
        public Color TextColorMouseDown { get; set; }

        public SkinSystem(Scene scene)
            : base(scene)
        {
            BackgroundColorIdle = Color.SlateBlue;
            BackgroundColorMouseover = Color.LightSkyBlue;
            BackgroundColorMouseDown = Color.DarkSlateBlue;
            TextColorIdle = Color.LightGray;
            TextColorMouseover = Color.White;
            TextColorMouseDown = Color.Green;
        }

        public override void Go(GameTime gameTime)
        {
            foreach (Entity e in Scene.Entities[(int)SortingLayer.HUD])
            {
                if (e is Control c)
                {
                    if (c.Background.Visible)
                    {
                        c.Background.Color = BackgroundColorIdle;
                        if (c.Input.MouseEnabled && c.Family.Children.Count == 0)
                        {
                            if (c.Input.MouseInputState == MouseInputState.MouseHover ||
                                c.Input.HasFocus)
                            {
                                c.Background.Color = BackgroundColorMouseover;
                            }
                            else if (c.Input.MouseInputState == MouseInputState.MouseDown ||
                                c.Input.MouseInputState == MouseInputState.Dragging)
                            {
                                c.Background.Color = BackgroundColorMouseDown;
                            }
                        }
                    }
                    if (c is Label l)
                    {
                        l.TextSprite.Color = TextColorIdle;
                        if (l.Input.MouseInputState == MouseInputState.MouseHover)
                        {
                            l.TextSprite.Color = TextColorMouseover;
                        }
                        else if (l.Input.MouseInputState == MouseInputState.MouseDown ||
                            l.Input.MouseInputState == MouseInputState.Dragging)
                        {
                            l.TextSprite.Color = TextColorMouseDown;
                        }
                    }
                }
            }
        }
    }
}
