using VContainer;
using VContainerUi.Abstraction;
using VContainerUi.Runtime.Empty;

namespace Example.Empty
{
	public class EmptyWindow : WindowBase
	{
		public EmptyWindow(IObjectResolver container) : base(container)
		{
		}
		
		public override string Name => "Empty";
		
		protected override void AddControllers()
		{
			AddController<EmptyController>();
		}
	}
}