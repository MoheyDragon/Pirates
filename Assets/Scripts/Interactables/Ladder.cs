using System;
using UnityEngine;
using UnityEngine.AI;
public class Ladder : MonoBehaviour,IInteractabe
{
    [SerializeField] GameObject PlayerPressHint;
    [SerializeField] float fullClimbDuration;
    Vector3 bottomPoint, topPoint;

    public Vector3 Position => transform.position;

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
    public void Interact(CharacterInteract interacter,Action onFinishAction)
    {
        interacter.GetBehavorsController.characterAnimator.Climbing(true);
        onFinishAction+= ()=>interacter.GetBehavorsController.characterAnimator.Climbing(false);
        if (IsDirectionUp(interacter.transform.position))
            ClimbUp(interacter, onFinishAction);
        else
            ClimbDown(interacter, onFinishAction);
    }
    public void ClimbUp(CharacterInteract interacter,Action onFinishAction)
    {
        LeanTween.moveY(interacter.gameObject, topPoint.y,GetClimbDuration(interacter,topPoint)).setOnComplete(onFinishAction);
    }
    public void ClimbDown(CharacterInteract interacter, Action onFinishAction)
    {
        LeanTween.moveY(interacter.gameObject, bottomPoint.y, GetClimbDuration(interacter,bottomPoint)).setOnComplete(onFinishAction);
    }
    private float GetClimbDuration(CharacterInteract interacter,Vector3 destination)
    {
        float time= Mathf.Abs(destination.y-interacter.transform.position.y) * fullClimbDuration/destination.y;
        print("current = " + interacter.gameObject.transform.position.y + " , top point = " + topPoint.y + " \n climb duration = " + time);
        return time;
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
