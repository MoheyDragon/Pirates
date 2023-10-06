using UnityEngine;

public class SelectableCharacter : MonoBehaviour
{
    CharacterBehaviorsController characterBehaviorsController;
    [SerializeField] ParticleSystem SelectionRing;
    private void Awake()
    {
        characterBehaviorsController = GetComponent<CharacterBehaviorsController>();
    }
    void Start()
    {
        characterBehaviorsController.input.enabled = false;
        characterBehaviorsController.playerInput.enabled = false;
    }
    public void ControlCharacter()
    {
        characterBehaviorsController.thirdPersonController.isCharacterSelected = true;
        characterBehaviorsController.input.enabled = true;
        characterBehaviorsController.playerInput.enabled = true;
        ZoomController.singleton.ChangeInputListner(characterBehaviorsController.input);
    }

    public void HighlightSelectionRing()
    {
        SelectionRing.Play(true);
    }
    public void StopHighlightSelectionRing()
    {
        SelectionRing.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
    }
    public void ReleaseCharacter()
    {
        characterBehaviorsController.thirdPersonController.isCharacterSelected = false;
        characterBehaviorsController.thirdPersonController.StopCharacterMovement();
        characterBehaviorsController.input.enabled = false;
        characterBehaviorsController.playerInput.enabled = false;
    }
    public Transform GetCameraFollowTarget=> characterBehaviorsController.thirdPersonController.CinemachineCameraTarget.transform;
    public bool IsCharacterAnimatorBusy => characterBehaviorsController.characterAnimator.IsAnimatorInAction;
}
