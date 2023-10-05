using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class SelectableCharacter : MonoBehaviour
{
    ThirdPersonController thirdPersonController;
    CharacterAnimator characterAnimator;
    StarterAssetsInputs input;
    PlayerInput playerInput;
    [SerializeField] ParticleSystem SelectionRing;
    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        characterAnimator = GetComponent<CharacterAnimator>();
        input = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
        input.enabled = false;
        playerInput.enabled = false;
    }
    public void ControlCharacter()
    {
        thirdPersonController.SelectCharacter(true);
        input.enabled = true;
        playerInput.enabled = true;
        ZoomController.singleton.ChangeInputListner(input);
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
        thirdPersonController.SelectCharacter(false);
        thirdPersonController.StopCharacterMovement();
        input.enabled = false;
        playerInput.enabled = false;
    }
    public Transform GetCameraFollowTarget=>thirdPersonController.CinemachineCameraTarget.transform;
    public bool IsCharacterAnimatorBusy => characterAnimator.IsAnimatorInAction;
}
