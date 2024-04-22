using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examen : MonoBehaviour
{
    [SerializeField] float maxXRotationSpeed;
    [SerializeField] float maxYRotationSpeed;
    [SerializeField] float maxZRotationSpeed;

    float minXRotationSpeed;
    float minYRotationSpeed;
    float minZRotationSpeed;

    [SerializeField] float XAnimationDuration;
    [SerializeField] float YAnimationDuration;
    [SerializeField] float ZAnimationDuration;

    [SerializeField] AnimationCurve ease;

    void Start()
    {
        StartCoroutine(XRotation());
        StartCoroutine(YRotation());
        StartCoroutine(ZRotation());
    }

    IEnumerator XRotation()
    {
        while (true)
        {
            float count = 0f;
            while (count < XAnimationDuration)
            {
                float speedX = Mathf.Lerp(minXRotationSpeed, maxXRotationSpeed, ease.Evaluate(count / XAnimationDuration));
                transform.Rotate(Vector3.right * speedX * Time.deltaTime);
                count += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator YRotation()
    {
        while (true)
        {
            float count = 0f;
            while (count < YAnimationDuration)
            {
                float speedY = Mathf.Lerp(minYRotationSpeed, maxYRotationSpeed, ease.Evaluate(count / YAnimationDuration));
                transform.Rotate(Vector3.up * speedY * Time.deltaTime);
                count += Time.deltaTime;
                yield return null;
            }
        }
    }

    IEnumerator ZRotation()
    {
        while (true)
        {
            float count = 0f;
            while (count < ZAnimationDuration)
            {
                float speedZ = Mathf.Lerp(minZRotationSpeed, maxZRotationSpeed, ease.Evaluate(count / ZAnimationDuration));
                transform.Rotate(Vector3.forward * speedZ * Time.deltaTime);
                count += Time.deltaTime;
                yield return null;
            }
        }
    }
}