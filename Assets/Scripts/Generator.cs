using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    // this script generates a map
    [SerializeField] GameObject[,] tiles; // our 2d tileclass array
    [SerializeField] int xGenLimit, zGenLimit; // how far to the right and forward can we go?
    [SerializeField] List<GameObject> tileTypeList; // our gameobject tile types
    [SerializeField] float yFacOffset, yFacIncrease; // the offset of our sin wave and the increase overtime
    [SerializeField] float yFacOffsetMin, yFacOffsetMax;
    [SerializeField] float yFacIncreaseMin, yFacIncreaseMax;
    [SerializeField] float xAdditorial, zAdditorial, yAdditorial;
    [SerializeField] float xAdditorialMin, xAdditorialMax;
    [SerializeField] float zAdditorialMin, zAdditorialMax;
    [SerializeField] float yAdditorialMin, yAdditorialMax;
    [SerializeField] float lowPoint, highPoint, midPoint, lowQuarterPoint, highQuarterPoint, highLowDistance, waterOffset; // our lowest and highest points in the objects
    Transform highTrans, lowTrans; Vector3 lowPos, highPos;
    public enum tileTypes
    {
        empty, 
        sand,
        grass,
        dirt,
        rock,
        water
    }

    private void Start()
    {
        ArraySetup();
    }

    void ArraySetup()
    {
        // setup our tile array
        tiles = new GameObject[xGenLimit, zGenLimit];
        // then generate the map
        GenerateMap();
    }

    private void Update()
    {
        // regenerate if we press space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        // set our minimums and maximums 
        yFacOffset = Random.Range(yFacOffsetMin, yFacOffsetMax);
        yFacIncrease = Random.Range(yFacIncreaseMin, yFacIncreaseMax);
        xAdditorial = Random.Range(xAdditorialMin, xAdditorialMax);
        zAdditorial = Random.Range(zAdditorialMin, zAdditorialMax);
        lowPoint = 0; highPoint = 0; midPoint = 0;

        // check the array to make sure there are no objects in it
        foreach (GameObject tile in tiles)
        {
            if (tile != null)
            {   // destroy each one
                Destroy(tile);
            }
        }

        // place prefab objects from array
        // loop X
        for (int x = 0; x < xGenLimit; x++)
        {
            // loop z
            for (int z = 0; z < zGenLimit; z++)
            {
                // set y additorial per tile
                yAdditorial = Random.Range(yAdditorialMin, yAdditorialMax);
                // get our Y position depending on the x and z
                float yPos = (
                    Mathf.Sin(x + xAdditorial) * (yFacOffset += (yFacIncrease * Random.Range(-1,1)))
                    +
                    Mathf.Cos(z + zAdditorial) * (yFacOffset += (yFacIncrease * Random.Range(-1, 1)))
                    ) + yAdditorial;

                // spawn a random tile at the correct position
                tiles[x, z] = Instantiate(tileTypeList[(int)tileTypes.empty], new Vector3(x, yPos, z), Quaternion.identity);

                // set low and high points
                if (yPos < lowPoint)
                {
                    lowPoint = yPos;
                    lowTrans = tiles[x, z].transform;
                }

                if (yPos > highPoint)
                {
                    highPoint = yPos;
                    highTrans = tiles[x, z].transform;
                }

                highLowDistance = Mathf.Abs(lowPoint - highPoint);

                // calculate points from the high point
                midPoint = highPoint - (highLowDistance / 2);
                lowQuarterPoint = lowPoint + (highLowDistance / 4);
                highQuarterPoint = highPoint - (highLowDistance / 4);
            }
        }

        // replace tiles with their new tiles
        for (int i = 0; i < xGenLimit; i++)
        {
            // loop j
            for (int j = 0; j < zGenLimit; j++)
            {
                // get the distance from the highest thing to the lowest thing

                // 0 to quarter
                if (tiles[i, j].transform.position.y <= lowQuarterPoint)
                {
                    tiles[i, j] = Instantiate(tileTypeList[(int)tileTypes.sand], tiles[i, j].transform);
                    // then make water above
                    GameObject water = Instantiate(tileTypeList[(int)tileTypes.water], new Vector3(i, lowPos.y + waterOffset, j), Quaternion.identity);
                    water.transform.parent = tiles[i, j].transform;
                } else 
                // quater to mid
                if (tiles[i, j].transform.position.y >= lowQuarterPoint && tiles[i, j].transform.position.y < midPoint)
                {
                    tiles[i, j] = Instantiate(tileTypeList[(int)tileTypes.dirt], tiles[i, j].transform);
                } else
                // mid to high quarter
                if (tiles[i, j].transform.position.y >= midPoint && tiles[i, j].transform.position.y < highQuarterPoint)
                {
                    tiles[i, j] = Instantiate(tileTypeList[(int)tileTypes.grass], tiles[i, j].transform);
                }
                else
                // high and above
                if (tiles[i, j].transform.position.y >= highQuarterPoint)
                {
                    tiles[i, j] = Instantiate(tileTypeList[(int)tileTypes.rock], tiles[i, j].transform);
                }
                
            }
        }
    }
}
