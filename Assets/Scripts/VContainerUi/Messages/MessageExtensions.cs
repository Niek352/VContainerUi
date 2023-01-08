using MessagePipe;
using VContainerUi.Abstraction;

namespace VContainerUi.Messages
{
	public static class MessageExtensions
	{
		public static void OpenWindow<TWindow>(this IPublisher<MessageOpenWindow> publisher, UiScope scope = UiScope.Local)
			where TWindow : Window
			=> publisher.Publish(new MessageOpenWindow(typeof(TWindow), scope));
		
		public static void BackWindow<TWindow>(this IPublisher<MessageBackWindow> publisher)
			where TWindow : Window
			=> publisher.Publish(new MessageBackWindow());
	}
}