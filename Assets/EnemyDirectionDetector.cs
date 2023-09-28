using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectionDetector : MonoBehaviour
{
    //Put this script on player game object 
    public bool IsEnemyInFrontOfPlayer(Transform enemy)
    {
        Vector3 playerForward = transform.forward;
        Vector3 playerToEnemeyDirection = (enemy.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(playerForward, playerToEnemeyDirection);
        if (dotProduct >= 0)
            return true;
        else
            return false;
    }
}
