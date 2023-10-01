using UnityEngine.AI;
using UnityEngine;

public class FollowCaptain : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Transform Captain;
    bool isFollowingCaptain;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isFollowingCaptain = true;
    }
    void Update()
    {
        if (isFollowingCaptain)
        {
            agent.SetDestination(Captain.position);
        }
    }
    public void DoFollow(bool follow) => isFollowingCaptain = follow;
}
