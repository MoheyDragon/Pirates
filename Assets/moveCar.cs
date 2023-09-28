using System.Collections;
using UnityEngine;
using System;
public class carMovment : MonoBehaviour
{
    [SerializeField] Point[] points;
    [SerializeField] int barrierPointIndex;
    int currentPointIndex;

    void Start()
    {
        currentPointIndex = -1;
        MoveCarToNextPoint();
    }
    public void MoveCarToNextPoint()
    {
        if (currentPointIndex + 1 == barrierPointIndex) return;
        else if(currentPointIndex + 1 == points.Length)
        {
            HandleReachedEndPoint();
            return;
        }
        currentPointIndex++;
        gameObject.LeanMoveLocalX(points[currentPointIndex].xPosition, points[currentPointIndex].duration).setOnComplete(
            () => StartCoroutine(ShowMassage()));
    }
    private void HandleReachedEndPoint()
    {

    }
    public void MoveCarAfterBarrier()
    {
        currentPointIndex++;
        gameObject.LeanMoveLocalX(points[currentPointIndex].xPosition, points[currentPointIndex].duration).setOnComplete(
            () => StartCoroutine(ShowMassage()));
    }
    IEnumerator ShowMassage()
    {
        points[currentPointIndex].massage.SetActive(true);
        yield return new WaitForSeconds(points[currentPointIndex].massageDisplayDuration);
        StartCoroutine(HideMassage());
    }
    IEnumerator HideMassage()
    {
        points[currentPointIndex].massage.SetActive(false);
        yield return new WaitForSeconds(points[currentPointIndex].massageDisplayDuration);
        MoveCarToNextPoint();
    }
}
[Serializable]
public class Point
{
    public float xPosition;
    public float duration;
    public GameObject massage;
    public float massageDisplayDuration;
}