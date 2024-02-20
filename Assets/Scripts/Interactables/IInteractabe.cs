using UnityEngine;
using System;
public interface IInteractabe
{
    void Interact(CharacterInteract interacter, Action onFinishAction);
    void DisplayInteractButton();
    void HideInteractButton();
}
