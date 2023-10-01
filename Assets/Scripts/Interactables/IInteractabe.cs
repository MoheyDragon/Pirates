using UnityEngine;
using System;
public interface IInteractabe
{
    void Interact(GameObject player, Action onFinishAction);
    void DisplayInteractButton();
    void HideInteractButton();
}
