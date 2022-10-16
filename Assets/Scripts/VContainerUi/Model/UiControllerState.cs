namespace VContainerUi.Model
{
	public class UiControllerState
	{
		public bool IsActive;
		public bool InFocus;
		public int Order;

		public UiControllerState(bool isActive, bool inFocus, int order)
		{
			IsActive = isActive;
			InFocus = inFocus;
			Order = order;
		}
	}
}