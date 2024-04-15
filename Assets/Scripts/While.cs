using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class While : MonoBehaviour
{
    [SerializeField] int animationDuration;
    float seconds;
    [SerializeField] Transform objectToMove;
    [SerializeField] List<Transform> points;
    [SerializeField] List<Color> colors;
    int startPointList = 0;
    int endPointList = 1;
    void Start()
    {
        StartCoroutine(Letter());
        StartCoroutine(Seconds());
        seconds = 0;
    }
    IEnumerator Letter()
    {
        int frameCount = 0;

        while (frameCount <= animationDuration)
        {
            frameCount++;
            print("a " + frameCount);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Seconds()
    {
        while (true)
        {
            seconds = 0;

            while (seconds <= animationDuration)
            {
                seconds += Time.deltaTime;
                objectToMove.position = Vector3.Lerp (points[startPointList].position, points[endPointList].position, seconds / animationDuration);
                objectToMove.GetComponent<Renderer>().material.color = Color.Lerp(colors[startPointList], colors[endPointList], seconds / animationDuration);
                yield return new WaitForEndOfFrame();
            }

            UpdateIndex();
        }
    }

    void UpdateIndex()
    {
        startPointList = endPointList;
        endPointList = (startPointList + 1) % points.Count;
    }
}
            /*else
            {
                while (seconds <= animationDuration)
                {
                    seconds += Time.deltaTime;
                    transform.position = Vector3.Lerp(pointB.position, pointA.position, seconds / animationDuration);
                    up = true;
                    yield return new WaitForEndOfFrame();
                }
            }*/