using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] float targetScale;
    [SerializeField] float animationDuration;
    [SerializeField] AnimationCurve ease;

    [HideInInspector] public Vector3 rotationSpeed = Vector3.zero;
    [HideInInspector] public int initialX;
    [HideInInspector] public int initialY;
    //[HideInInspector] public float rSpeed;
    //[HideInInspector] public float gSpeed;
    //[HideInInspector] public int width;
    //[HideInInspector] public int height;


    Material material;

    private void Awake()
    {
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    void Start()
    {
        if (rotationSpeed != Vector3.zero)
            StartCoroutine(RotationCoroutine());
        //if (rSpeed != 0 || gSpeed != 0)
        //    StartCoroutine(ColorAnimation());
    }

    public void Animation()
    {
        StartCoroutine(ScaleAnimation());
    }

    IEnumerator ScaleAnimation()
    {
        Vector3 scaleVector = new Vector3(targetScale, targetScale, targetScale);

        float elapsedTime = 0;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;

            transform.localScale = Vector3.LerpUnclamped(Vector3.one, scaleVector, ease.Evaluate(elapsedTime / animationDuration));
            yield return null;
        }
    }

    IEnumerator RotationCoroutine()
    {
        transform.Rotate(initialX * 5, initialY * 5, 0);

        while (true)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    //IEnumerator ColorAnimation() 
    //{
    //    float r = initialX, g = initialY, b = 0;
    //    float rDir = 1, gDir = 1, bDir = 1;
    //    float bCap = (width + height) * 0.5f;

    //    while (true)
    //    {
    //        r += Time.deltaTime * rSpeed * rDir;
    //        if (r > width && rDir > 0)
    //            rDir = -1;
    //        else if (r < 0 && rDir < 0)
    //            rDir = 1;

    //        g += Time.deltaTime * gSpeed * gDir;
    //        if (g > height && gDir > 0)
    //            gDir = -1;
    //        else if (g < 0 && gDir < 0)
    //            gDir = 1;

    //        b += Time.deltaTime * bDir;
    //        if (b > height && bCap > 0)
    //            bDir = -1;
    //        else if (b < 0 && bDir < 0)
    //            bDir = 1;

    //        material.color = new Color(
    //            r / width,
    //            g / height,
    //            b / bCap * 1.5f
    //        );

    //        yield return null;
    //    }
    //}
}