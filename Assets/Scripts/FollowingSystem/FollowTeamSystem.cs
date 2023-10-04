using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTeamSystem : Singleton<FollowTeamSystem>
{
    [SerializeField] List<FollowTeam> followers;
    public void StopFollowing()
    {
    }
    public void FollowSelectedCharacter(Transform selectedCharacter)
    {
        foreach (FollowTeam follower in followers)
            follower.OnSelectedCharacterChanged(selectedCharacter);
    }
}
