using System;
using UnityEngine;
using UnityEngine.AI;
public class Ladder : MonoBehaviour,IInteractabe
{
    [SerializeField] GameObject PlayerPressHint;
    [SerializeField] float ladderHeight, climbDuration;
    Vector3 bottomPoint, topPoint;
    private void Awake()
    {
        _SetLadderPoints();
    }
    private void _SetLadderPoints()
    {
        OffMeshLink offMeshLink = GetComponent<OffMeshLink>();
        if (offMeshLink.startTransform.position.y > offMeshLink.endTransform.position.y)
        {
            topPoint = offMeshLink.startTransform.position;
            bottomPoint = offMeshLink.endTransform.position;
        }
        else
        {
            topPoint = offMeshLink.endTransform.position;
            bottomPoint = offMeshLink.startTransform.position;
        }
    }
    public void Interact(GameObject player,Action onFinishAction)
    {
        if (IsDirectionUp(player.transform.position))
            ClimbUp(player, onFinishAction);
        else
            ClimbDown(player, onFinishAction);
    }
    public void ClimbUp(GameObject player,Action onFinishAction)
    {
        LeanTween.moveLocalY(player, player.transform.localPosition.y + ladderHeight, climbDuration).setOnComplete(onFinishAction);
    }
    public void ClimbDown(GameObject player, Action onFinishAction)
    {
        LeanTween.moveLocalY(player, player.transform.localPosition.y - ladderHeight, climbDuration).setOnComplete(onFinishAction);
    }
    private bool IsDirectionUp(Vector3 playerPosition)
    {
        if (Vector3.Distance(playerPosition, topPoint) > Vector3.Distance(playerPosition, bottomPoint))
        {
            return true;
        }
        else
            return false;
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
