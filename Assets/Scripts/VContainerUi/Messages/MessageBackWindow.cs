using VContainerUi.Interfaces;

namespace VContainerUi.Messages
{
	public readonly struct MessageBackWindow : IUiMessage
	{
		public UiScope UiScope { get; }
		public MessageBackWindow(UiScope uiScope = UiScope.Local)
		{
			UiScope = uiScope;
		}
	}
}