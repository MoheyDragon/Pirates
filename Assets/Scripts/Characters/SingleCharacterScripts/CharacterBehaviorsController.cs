using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class CharacterBehaviorsController : MonoBehaviour
{
    [HideInInspector] public SelectableCharacter selectableCharacter;
    [HideInInspector] public CharacterInteract characterInteract;
    [HideInInspector] public FollowerBehavior followerBehavior;
    [HideInInspector] public CharacterAnimator characterAnimator;
    [HideInInspector] public StarterAssetsInputs input;
    [HideInInspector] public UnityEngine.InputSystem.PlayerInput playerInput;
    [HideInInspector] public ThirdPersonController thirdPersonController;
    private void Awake()
    {
        selectableCharacter = GetComponent<SelectableCharacter>();
        characterInteract = GetComponent<CharacterInteract>();
        followerBehavior = GetComponent<FollowerBehavior>();
        characterAnimator = GetComponent<CharacterAnimator>();
        input = GetComponent<StarterAssetsInputs>();
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }
}