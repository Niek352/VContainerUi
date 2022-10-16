using MessagePipe;
using VContainerUi.Messages;

namespace VContainerUi.Services
{
	public interface IUiMessagesReceiverService
	{
		public ISubscriber<MessageOpenWindow> OpenWindowSubscriber { get; }
		public ISubscriber<MessageBackWindow> BackWindowSubscriber { get; }
		public ISubscriber<MessageOpenRootWindow> OpenRootWindowSubscriber { get; }
	}
}