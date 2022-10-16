using VContainerUi.Interfaces;

namespace VContainerUi.Messages
{
	public readonly struct MessageFocusWindow
	{
		public readonly IWindow Window;

		public MessageFocusWindow(IWindow window)
		{
			Window = window;
		}
	}
}