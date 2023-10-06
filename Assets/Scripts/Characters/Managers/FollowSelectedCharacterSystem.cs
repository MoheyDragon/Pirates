using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSelectedCharacterSystem : Singleton<FollowSelectedCharacterSystem>
{
    FollowerBehavior[] characters;
    private void Start()
    {
        AssignCharacters();
    }
    private void AssignCharacters()
    {
        characters = new FollowerBehavior[CharactersManager.singleton.Characters.Length];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = CharactersManager.singleton.Characters[i].followerBehavior;
        }
    }
    public void StopFollowing()
    {
    }
    public void FollowSelectedCharacter(Transform selectedCharacter)
    {
        foreach (FollowerBehavior follower in characters)
            follower.OnSelectedCharacterChanged(selectedCharacter);
    }
}
