using System;
using UnityEngine;

public class Ladder : MonoBehaviour,IInteractabe
{
    [SerializeField] GameObject PlayerPressHint;
    [SerializeField] float ladderHeight, climbDuration;
    
    public void Interact(GameObject player,Action onFinishAction)
    {
        ClimbUp(player,onFinishAction);
    }
    public void ClimbUp(GameObject player,Action onFinishAction)
    {
        LeanTween.moveLocalY(player, player.transform.localPosition.y + ladderHeight, climbDuration).setOnComplete(onFinishAction);
    }
    public void DisplayInteractButton()
    {
        PlayerPressHint.SetActive(true);
    }

    public void HideInteractButton()
    {
        PlayerPressHint.SetActive(false);
    }
}
