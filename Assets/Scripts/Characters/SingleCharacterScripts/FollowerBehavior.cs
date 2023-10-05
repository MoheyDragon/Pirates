using UnityEngine.AI;
using UnityEngine;

public class FollowerBehavior : MonoBehaviour
{
    bool isCharacterSelected;
    NavMeshAgent agent;
    CharacterAnimator characterAnimator;
    Transform selectedCharacter;
    CharacterInteract interact;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        characterAnimator = GetComponent<CharacterAnimator>();
        interact = GetComponent<CharacterInteract>();
    }
    void Update()
    {
        if (isCharacterSelected) return;
        if (!agent.enabled) return;
        {

            agent.SetDestination(selectedCharacter.position);
            if (agent.isOnOffMeshLink)
            {
                interact.Interact();
                agent.CompleteOffMeshLink();
            }
            if (ReachedTarget())
                StopAnimation();
            else
                MoveAnimation();
        }
    }
    public void OnSelectedCharacterChanged(Transform newlySelectedCharacter)
    {
        selectedCharacter = newlySelectedCharacter;
        if (newlySelectedCharacter == transform)
            ActiviateFollower(false);
        else
            ActiviateFollower(true);
    }
    private void ActiviateFollower(bool activiate)
    {
        agent.enabled = activiate;
        isCharacterSelected = !activiate;
    }
    public void PauseFollow()
    {
        if (isCharacterSelected) return;
        agent.isStopped = true;
    }
    public void ResumeFollow()
    {
        if (isCharacterSelected) return;
        agent.isStopped = false;
    }
    private void MoveAnimation()
    {
        characterAnimator.ControlAnimationSpeed(agent.speed);
    }
    private void StopAnimation()
    {
        characterAnimator.ControlAnimationSpeed(0);
    }
    private bool ReachedTarget()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }
    
}
