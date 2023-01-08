using UnityEngine.UI;
using VContainerUi.Interfaces;

namespace VContainerUi.Abstraction
{
    public abstract class UiMainPageView : UiView, IUiView
    {
        public ScrollRect Scroll;
    }
}