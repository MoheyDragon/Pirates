using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;
public class FollowerBehavior : MonoBehaviour
{
    CharacterBehaviorsController characterBehaviorsController;
    NavMeshAgent agent;
    void Awake()
    {
        characterBehaviorsController = GetComponent<CharacterBehaviorsController>();
        agent = GetComponent<NavMeshAgent>();
    }
    Transform selectedCharacter;
    void Update()
    {
        if (characterBehaviorsController.thirdPersonController.isCharacterSelected) return;
        if (!agent.enabled) return;
        {

            agent.SetDestination(selectedCharacter.position);
            if (agent.isOnOffMeshLink)
            {
                characterBehaviorsController.characterInteract.Interact();
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
    }
    public void PauseFollow()
    {
        if (characterBehaviorsController.thirdPersonController.isCharacterSelected) return;
        agent.isStopped = true;
    }
    public void ResumeFollow()
    {
        if (characterBehaviorsController.thirdPersonController.isCharacterSelected) return;
        agent.isStopped = false;
    }
    private void MoveAnimation()
    {
        characterBehaviorsController.characterAnimator.ControlAnimationSpeed(agent.speed);
    }
    private void StopAnimation()
    {
        characterBehaviorsController.characterAnimator.ControlAnimationSpeed(0);
    }
    private bool ReachedTarget()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }
    
}
