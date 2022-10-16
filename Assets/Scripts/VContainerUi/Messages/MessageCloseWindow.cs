using VContainerUi.Interfaces;

namespace VContainerUi.Messages
{
	public readonly struct MessageCloseWindow
	{
		public readonly IWindow Window;

		public MessageCloseWindow(IWindow window)
		{
			Window = window;
		}
	}
}