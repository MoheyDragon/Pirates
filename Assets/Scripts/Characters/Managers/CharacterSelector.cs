using UnityEngine;
using Cinemachine;
using StarterAssets;
public class CharacterSelector : Singleton<CharacterSelector>
{
    int currentCharacterIndex;
    SelectableCharacter currentSelectedCharacter;
    SelectableCharacter[] characters;
    private void Start()
    {
        AssignCharacters();
        SelectCharacter(0);
    }
    private void AssignCharacters()
    {
        characters = new SelectableCharacter[CharactersManager.singleton.Characters.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = CharactersManager.singleton.Characters[i].selectableCharacter;
        }
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
    private void SetSelectedCharacter(int characterIndex)
    {
        currentCharacterIndex = characterIndex;
        currentSelectedCharacter = characters[characterIndex];

    }
    private void ChangeCameraFollowTarget()
    {
        ZoomController.singleton.vCam.Follow = currentSelectedCharacter.GetCameraFollowTarget;
    }
    private void ControlSelectedCharacter()
    {
        currentSelectedCharacter.ControlCharacter();
    }
    private void HighlightSelectionRing()
    {
        currentSelectedCharacter.HighlightSelectionRing();
    }
    private void FollowSelectedCharacter()
    {
        FollowSelectedCharacterSystem.singleton.FollowSelectedCharacter(currentSelectedCharacter.transform);
    }
    public SelectableCharacter GetSelectedCharacter => currentSelectedCharacter;
}
