using UnityEngine;
using Cinemachine;
using StarterAssets;
public class CharacterSelector : MonoBehaviour
{
    int currentCharacterIndex;
                     CharacterSelectionHandler currentSelectedCharacter;
    [SerializeField] CharacterSelectionHandler[] characters;
    [SerializeField] CinemachineVirtualCamera vCam;
    private void Start()
    {
        SelectCharacter(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
            NextCharacter();
        if (Input.GetKeyDown(KeyCode.R))
            PreviousCharacter();
    }
    private void NextCharacter()
    {
        CycleCharacterIndex(1);   
        SelectCharacter(currentCharacterIndex);
    }
    private void PreviousCharacter()
    {
        CycleCharacterIndex(-1);
        SelectCharacter(currentCharacterIndex);
    }
    private void CycleCharacterIndex(int direction)
    {
        currentCharacterIndex+=direction;
        if (currentCharacterIndex == characters.Length)
            currentCharacterIndex = 0;
        else if (currentCharacterIndex == -1)
            currentCharacterIndex = characters.Length-1;
    }
    public void SelectCharacter(int characterIndex)
    {
        DeControlPreviousCharacter();
        SetSelectedCharacter(characterIndex);
        ChangeCameraFollowTarget();
        ControlSelectedCharacter();
    }
    private void SetSelectedCharacter(int characterIndex)
    {
        currentCharacterIndex = characterIndex;
        currentSelectedCharacter = characters[characterIndex];

    }
    private void ChangeCameraFollowTarget()
    {
        vCam.Follow = currentSelectedCharacter.GetCameraFollowTarget;
    }
    private void DeControlPreviousCharacter()
    {
        if(currentSelectedCharacter!=null)
        currentSelectedCharacter.ReleaseCharacter();
    }
    private void ControlSelectedCharacter()
    {
        currentSelectedCharacter.ControlCharacter();
    }
}
