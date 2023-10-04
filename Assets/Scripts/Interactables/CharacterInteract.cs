using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation.Samples;
using StarterAssets;

public class CharacterInteract : MonoBehaviour
{
    [SerializeField] StarterAssetsInputs input;
    AgentLinkMover agentLinkMover;
    FollowTeam followTeam;
    IInteractabe currentInteractable;
    CharacterSelectionHandler characterSelectionHandler;
    private void Awake()
    {
        _SetupReferences();
    }
    void Start()
    {
        _SubscribeListeners();
    }
    private void _SetupReferences()
    {
        followTeam = GetComponent<FollowTeam>();
        agentLinkMover = GetComponent<AgentLinkMover>();
        characterSelectionHandler = GetComponent<CharacterSelectionHandler>();
    }
    private void _SubscribeListeners()
    {
        input.Interact += Interact;
        agentLinkMover.StartMovingBetweenSurfaces += Interact;
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
        followTeam.PauseFollow();
    }
    private void FinishInteraction()
    {
        isInteracting = false;
        followTeam.ResumeFollow();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagsManager.singleton.interactableTag))
        {
            currentInteractable = other.GetComponent<IInteractabe>();
            if (CharacterSelectorSystem.singleton.GetSelectedCharacter == characterSelectionHandler)
                other.GetComponent<IInteractabe>().DisplayInteractButton();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagsManager.singleton.interactableTag))
        {
            currentInteractable = null;
            if (CharacterSelectorSystem.singleton.GetSelectedCharacter == characterSelectionHandler)
                other.GetComponent<IInteractabe>().HideInteractButton();
        }
    }
}
