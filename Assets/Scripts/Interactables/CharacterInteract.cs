using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation.Samples;
using StarterAssets;

public class CharacterInteract : MonoBehaviour
{
    [SerializeField] StarterAssetsInputs input;
    AgentLinkMover agentLinkMover;
    NavMeshAgent agent;
    
    IInteractabe currentInteractable;
    private void Awake()
    {
        TryGetComponent(out NavMeshAgent agentTry);
        if (agentTry)
        {
            agent = agentTry;
        }
        TryGetComponent(out AgentLinkMover agentLinkMoverTry);
        if (agentLinkMoverTry)
        {
            agentLinkMover = agentLinkMoverTry;
            agentLinkMover.StartMovingBetweenSurfaces += InteractAi;
        }
    }
    void Start()
    {
        input.Interact += Interact;
        if (agentLinkMover)
            agentLinkMover.StartMovingBetweenSurfaces += InteractAi;
    }
    private void InteractAi()
    {
        agent.isStopped = true;
        Interact();

    }
    private void Interact()
    {
        if (isInteracting) return;
        if (currentInteractable==null)
        {
            //Play warining sound that there is nothing to interact with
            return;
        }
        currentInteractable.Interact(gameObject,FinishInteraction);
        StartInteraction();
    }
    bool isInteracting;
    private void StartInteraction()
    {
        isInteracting = true;
        input.enabled = false;
        FollowCaptainManager.singleton.StopFollowing();
    }
    private void FinishInteraction()
    {
        isInteracting = false;
        input.enabled = true;
        if (agent)
        {
            agent.isStopped = false;
        }
        FollowCaptainManager.singleton.ResumeFollowing();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagsManager.singleton.interactableTag))
        {
            other.GetComponent<IInteractabe>().DisplayInteractButton();
            currentInteractable = other.GetComponent<IInteractabe>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagsManager.singleton.interactableTag))
        {
            other.GetComponent<IInteractabe>().HideInteractButton();
            currentInteractable = null;
        }
    }
}
