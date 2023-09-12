using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CharacterSelectionHandler : MonoBehaviour
{
    ThirdPersonController thirdPersonController;
    StarterAssetsInputs input;
    PlayerInput playerInput;
    [SerializeField] GameObject SelectionRing;
    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        input = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<PlayerInput>();
        SelectionRing.SetActive(false);
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
    public void HighlightSelectionRing(bool enabled)
    {
        SelectionRing.SetActive(enabled);
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
