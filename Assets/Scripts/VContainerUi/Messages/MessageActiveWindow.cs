using VContainerUi.Interfaces;

namespace VContainerUi.Messages
{
	public readonly struct MessageActiveWindow
	{
		public readonly IWindow Window;

		public MessageActiveWindow(IWindow window)
		{
			Window = window;
		}
	}
}