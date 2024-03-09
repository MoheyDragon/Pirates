using UnityEngine;
using StarterAssets;

public class CharacterInteract : MonoBehaviour
{
    CharacterBehaviorsController characterBehaviorsController;
    IInteractabe currentInteractable;
    private void Awake()
    {
        characterBehaviorsController = GetComponent<CharacterBehaviorsController>();
    }
    void Start()
    {
        _SubscribeListeners();
    }
    private void _SubscribeListeners()
    {
        characterBehaviorsController.input.Interact += Interact;
    }
    public void Interact()
    {
        if (isInteracting) return;
        if (currentInteractable==null)
        {
            //Play warining sound that there is nothing to interact with
            return;
        }
        FaceInteractable();
        currentInteractable.Interact(this,FinishInteraction);
        StartInteraction();
    }
    private void FaceInteractable()
    {
        Vector3 direction = currentInteractable.Position - transform.position;
        direction.y = 0f; // Ignore height difference
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = targetRotation;
    }
    bool isInteracting;
    private void StartInteraction()
    {
        isInteracting = true;
        characterBehaviorsController.followerBehavior.PauseFollow();
    }
    private void FinishInteraction()
    {
        isInteracting = false;
        characterBehaviorsController.followerBehavior.ResumeFollow();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagsManager.singleton.interactableTag))
        {
            currentInteractable = other.GetComponent<IInteractabe>();
            if (CharacterSelector.singleton.GetSelectedCharacter == characterBehaviorsController.selectableCharacter)
                other.GetComponent<IInteractabe>().DisplayInteractButton();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagsManager.singleton.interactableTag))
        {
            currentInteractable = null;
            if (CharacterSelector.singleton.GetSelectedCharacter == characterBehaviorsController.selectableCharacter)
                other.GetComponent<IInteractabe>().HideInteractButton();
        }
    }
    public CharacterBehaviorsController GetBehavorsController => characterBehaviorsController;
}
