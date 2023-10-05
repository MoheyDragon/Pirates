using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSelectedCharacterSystem : Singleton<FollowSelectedCharacterSystem>
{
    [SerializeField] List<FollowerBehavior> followers;
    public void StopFollowing()
    {
    }
    public void FollowSelectedCharacter(Transform selectedCharacter)
    {
        foreach (FollowerBehavior follower in followers)
            follower.OnSelectedCharacterChanged(selectedCharacter);
    }
}
