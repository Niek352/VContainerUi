using UnityEngine;
using VContainerUi.Interfaces;

namespace VContainerUi.Abstraction
{
    public abstract class UiElement : MonoBehaviour, IUiElement
    {
        public abstract string Name { get; }
        public abstract int Id { get; }
        public abstract void Highlight();
        public abstract void Reset();
    }
}