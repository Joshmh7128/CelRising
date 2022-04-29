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
    [SerializeField] float lowPoint, highPoint; // our lowest and highest points in the mesh
    public enum tileTypes
    {
        empty, 
        sand,
        grass,
        dirt,
        rock
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

                // set low and high points
                if (yPos < lowPoint)
                { lowPoint = yPos;  }   
                
                if (yPos > highPoint)
                { highPoint = yPos;  }

                // spawn a random tile at the correct position
                tiles[x, z] = Instantiate(tileTypeList[(int)tileTypes.empty], new Vector3(x, yPos, z), Quaternion.identity);
            }
        }

        // replace tiles with their new tiles
        for (int i = 0; i < xGenLimit; i++)
        {
            // loop j
            for (int j = 0; j < zGenLimit; j++)
            {
                float midpoint = (lowPoint + highPoint) / 2;

                if (tiles[i, j].transform.position.y > midpoint)
                {
                    // destroy each one
                    tiles[i, j] = Instantiate(tileTypeList[(int)tileTypes.sand], tiles[i, j].transform);
                }
                else
                {
                    tiles[i, j] = Instantiate(tileTypeList[(int)tileTypes.grass], tiles[i, j].transform);
                }
            }
        }
    }
}
