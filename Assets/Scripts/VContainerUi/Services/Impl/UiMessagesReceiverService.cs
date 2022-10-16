using MessagePipe;
using VContainerUi.Messages;

namespace VContainerUi.Services.Impl
{
	public class UiMessagesReceiverService : IUiMessagesReceiverService
	{
		public UiMessagesReceiverService(
			ISubscriber<MessageOpenWindow> openWindowSubscriber, 
			ISubscriber<MessageBackWindow> closeWindowSubscriber, 
			ISubscriber<MessageOpenRootWindow> openRootWindowSubscriber)
		{
			OpenWindowSubscriber = openWindowSubscriber;
			BackWindowSubscriber = closeWindowSubscriber;
			OpenRootWindowSubscriber = openRootWindowSubscriber;
		}

		public ISubscriber<MessageOpenWindow> OpenWindowSubscriber { get; }
		public ISubscriber<MessageBackWindow> BackWindowSubscriber { get; }
		public ISubscriber<MessageOpenRootWindow> OpenRootWindowSubscriber { get; }
	}
}