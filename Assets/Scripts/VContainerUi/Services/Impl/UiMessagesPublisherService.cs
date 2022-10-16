using MessagePipe;
using VContainerUi.Abstraction;
using VContainerUi.Messages;

namespace VContainerUi.Services.Impl
{
	public class UiMessagesPublisherService : IUiMessagesPublisherService
	{
		public UiMessagesPublisherService(
			IPublisher<MessageOpenWindow> openWindowPublisher,
			IPublisher<MessageBackWindow> backWindowPublisher, 
			IPublisher<MessageOpenRootWindow> openRootWindowPublisher, 
			IPublisher<MessageActiveWindow> messageActiveWindowPublisher, 
			IPublisher<MessageFocusWindow> messageFocusWindowPublisher, 
			IPublisher<MessageShowWindow> messageShowWindowPublisher, 
			IPublisher<MessageCloseWindow> messageCloseWindowPublisher)
		{
			OpenWindowPublisher = openWindowPublisher;
			BackWindowPublisher = backWindowPublisher;
			OpenRootWindowPublisher = openRootWindowPublisher;
			MessageActiveWindowPublisher = messageActiveWindowPublisher;
			MessageFocusWindowPublisher = messageFocusWindowPublisher;
			MessageShowWindowPublisher = messageShowWindowPublisher;
			MessageCloseWindowPublisher = messageCloseWindowPublisher;
		}
		public IPublisher<MessageOpenWindow> OpenWindowPublisher { get; }
		public IPublisher<MessageBackWindow> BackWindowPublisher { get; }
		public IPublisher<MessageOpenRootWindow> OpenRootWindowPublisher { get; }
		public IPublisher<MessageActiveWindow> MessageActiveWindowPublisher { get; }
		public IPublisher<MessageFocusWindow> MessageFocusWindowPublisher { get; }
		public IPublisher<MessageShowWindow> MessageShowWindowPublisher { get; }
		public IPublisher<MessageCloseWindow> MessageCloseWindowPublisher { get; }
	}
}