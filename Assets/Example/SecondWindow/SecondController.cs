using UnityEngine;
using VContainerUi.Abstraction;
using VContainerUi.Interfaces;

namespace Example.SecondWindow
{
	public class SecondController : UiController<SecondView>, IUiInitializable
	{

		public void Initialize()
		{
			Debug.Log("Init");
		}
	}
}