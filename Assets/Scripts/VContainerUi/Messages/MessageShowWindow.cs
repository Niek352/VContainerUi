using VContainerUi.Interfaces;

namespace VContainerUi.Messages
{
	public readonly struct MessageShowWindow
	{
		public readonly IWindow Window;

		public MessageShowWindow(IWindow window)
		{
			Window = window;
		}
	}
}