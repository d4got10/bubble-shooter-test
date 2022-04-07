namespace BubbleShooter
{
    public class InputSystemProvider
    {
        public static IInputSystem Current { get; private set; }


        public static void Init(IInputSystem system)
        {
            Current = system;
        }
    }
}