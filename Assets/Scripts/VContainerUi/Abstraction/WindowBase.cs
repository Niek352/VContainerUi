using System.Collections.Generic;
using VContainer;
using VContainerUi.Interfaces;
using VContainerUi.Model;

namespace VContainerUi.Abstraction
{
	public abstract class WindowBase : Window
	{
		private readonly List<IUiController> _controllers = new List<IUiController>();

		private readonly IObjectResolver _container;
		
		protected WindowBase(IObjectResolver container)
		{
			_container = container;
		}

		[Inject]
		protected abstract void AddControllers();

		protected void AddController<TController>()
			where TController : IUiController
		{
			var controller = _container.Resolve<TController>();
			_controllers.Add(controller);
		}

		public override void SetState(UiWindowState state)
		{
			for (var i = 0; i < _controllers.Count; i++)
				_controllers[i].SetState(new UiControllerState(state.IsActive, state.InFocus, i));
			ProcessState();
		}

		public override void Back()
		{
			foreach (var c in _controllers)
				c.Back();
			ProcessState();
		}

		private void ProcessState()
		{
			foreach (var c in _controllers)
				c.ProcessStateOrder();
			foreach (var с in _controllers)
				с.ProcessState();
		}

		public override IUiElement[] GetUiElements()
		{
			var list = new List<IUiElement>();
			foreach (var с in _controllers)
				list.AddRange(с.GetUiElements());

			return list.ToArray();
		}
	}
}