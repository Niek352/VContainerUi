using MessagePipe;
using VContainerUi.Messages;

namespace VContainerUi.Services
{
	public interface IUiMessagesPublisherService
	{
		IPublisher<MessageOpenWindow> OpenWindowPublisher { get; }
		IPublisher<MessageBackWindow> BackWindowPublisher { get; }
		IPublisher<MessageOpenRootWindow> OpenRootWindowPublisher { get; }
		IPublisher<MessageActiveWindow> MessageActiveWindowPublisher { get; }
		IPublisher<MessageFocusWindow> MessageFocusWindowPublisher { get; }
		IPublisher<MessageShowWindow> MessageShowWindowPublisher { get; }
		IPublisher<MessageCloseWindow> MessageCloseWindowPublisher { get; }
	}
}