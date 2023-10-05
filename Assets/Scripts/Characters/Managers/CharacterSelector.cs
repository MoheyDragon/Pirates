using UnityEngine;
using Cinemachine;
using StarterAssets;
public class CharacterSelector : Singleton<CharacterSelector>
{
    int currentCharacterIndex;
                     SelectableCharacter currentSelectedCharacter;
    [SerializeField] SelectableCharacter[] characters;
    [SerializeField] CinemachineVirtualCamera vCam;
    private void Start()
    {
        SelectCharacter(0);
    }
    public void NextCharacter()
    {
        if (currentSelectedCharacter.IsCharacterAnimatorBusy) return;
        CycleCharacterIndex(1);   
        SelectCharacter(currentCharacterIndex);
    }
    public void PreviousCharacter()
    {
        if (currentSelectedCharacter.IsCharacterAnimatorBusy) return;
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
        StopHiglightingPreviousCharacterSelection();
        SetSelectedCharacter(characterIndex);
        ChangeCameraFollowTarget();
        ControlSelectedCharacter();
        HighlightSelectionRing();
        FollowSelectedCharacter();
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
    private void StopHiglightingPreviousCharacterSelection()
    {
        if(currentSelectedCharacter!=null)
        currentSelectedCharacter.StopHighlightSelectionRing();
    }
    private void HighlightSelectionRing()
    {
        currentSelectedCharacter.HighlightSelectionRing();
    }
    private void FollowSelectedCharacter()
    {
        FollowSelectedCharacterSystem.singleton.FollowSelectedCharacter(currentSelectedCharacter.transform);
    }
    private void ControlSelectedCharacter()
    {
        currentSelectedCharacter.ControlCharacter();
    }
    public SelectableCharacter GetSelectedCharacter => currentSelectedCharacter;
}
