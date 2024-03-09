using UnityEngine;
using System;
public interface IInteractabe
{
    Vector3 Position { get; }
    void Interact(CharacterInteract interacter, Action onFinishAction);
    void DisplayInteractButton();
    void HideInteractButton();
}
