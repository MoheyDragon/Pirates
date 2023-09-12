using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CharacterSelectionHandler : MonoBehaviour
{
    ThirdPersonController thirdPersonController;
    StarterAssetsInputs input;
    PlayerInput playerInput;
    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
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
    public void ReleaseCharacter()
    {
        thirdPersonController.SelectCharacter(false);
        thirdPersonController.StopCharacterMovement();
        input.enabled = false;
        playerInput.enabled = false;
    }
    public Transform GetCameraFollowTarget=>thirdPersonController.CinemachineCameraTarget.transform;
}
