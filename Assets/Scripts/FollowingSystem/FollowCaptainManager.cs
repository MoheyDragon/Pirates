using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCaptainManager : Singleton<FollowCaptainManager>
{
    [SerializeField] List<FollowCaptain> followers;
    public void StopFollowing()
    {
        foreach (FollowCaptain follower in followers)
            follower.DoFollow(false);
    }
    public void ResumeFollowing()
    {
        foreach (FollowCaptain follower in followers)
            follower.DoFollow(true);
    }
}
