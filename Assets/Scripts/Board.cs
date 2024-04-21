using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Create table")]
    [SerializeField] GameObject tilePrefab;
    [SerializeField] int boardWidth;
    [SerializeField] int boardHeight;
    [SerializeField] float timeBetweenSpawns;

    [Header("Tile animation")]
    [SerializeField] float timeBetweenAnimations;
    [SerializeField] float rotationSpeedX;
    [SerializeField] float rotationSpeedY;
    [SerializeField] float colorSpeedX;
    [SerializeField] float colorSpeedY;


    List<Tile> tiles = new List<Tile>();

    Vector3 position;

    void Start()
    {
        StartCoroutine(CreateBoard());
        StartCoroutine(TileAnimation());
        //StartCoroutine(BeatAnimation());
    }

    IEnumerator CreateBoard()
    {
        for (int i = 0; i <= boardWidth; i++)
            for (int j = 0; j <= boardHeight; j++)
            {
                position = new Vector3(i, 0, j);
                InstantiateAt(position);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
    }

    IEnumerator TileAnimation()
    {
        int current = 0;

        while (tiles.Count == 0)
            yield return null;

        while (true)
        {
            tiles[current].Animation();
            current = ++current % tiles.Count;

            yield return new WaitForSeconds(timeBetweenAnimations);
        }
    }

    void InstantiateAt(Vector3 position)
    {

        GameObject instantiated = Instantiate(
           tilePrefab,
           transform
           );

        instantiated.transform.localPosition = position;

        Tile ts = instantiated.GetComponent<Tile>();

        if (ts == null)
        {
            ts = instantiated.AddComponent<Tile>();
        }

        ts.rotationSpeed = new Vector3(
            rotationSpeedX,
            rotationSpeedY,
            0);

        ts.initialX = (int)position.x;
        ts.initialY = (int)position.z;
        //ts.width = boardWidth;
        //ts.height = boardHeight;
        //ts.rSpeed = colorSpeedX;
        //ts.gSpeed = colorSpeedY;


        tiles.Add(ts);
    }


    //IEnumerator BeatAnimation()
    //{
    //    while (true)
    //    {
    //        for (int i = 0; i < tiles.Count; i++)
    //        {
    //            tiles[i].TriggerAnimation();
    //            yield return null;
    //        }

    //        yield return new WaitForSeconds(beatDuration);
    //    }
    //}
}