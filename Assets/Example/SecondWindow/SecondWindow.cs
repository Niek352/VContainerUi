using VContainer;
using VContainerUi.Abstraction;

namespace Example.SecondWindow
{
	public class SecondWindow : WindowBase
	{
		public SecondWindow(IObjectResolver container) : base(container)
		{
			
		}
		
		public override string Name => "SecondWindow";
		
		protected override void AddControllers()
		{
			AddController<SecondController>();
		}
	}
}