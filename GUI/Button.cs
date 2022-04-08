namespace EdGeLib.GUI
{
    public class Button : Label
    {
        public Button(string text)
            : base(text, 128, true)
        {
            Input.MouseEnabled = true;
        }

        public Button(string text, int width, bool autoSize)
            : base(text, width, autoSize)
        {
            Input.MouseEnabled = true;
        }
    }
}
