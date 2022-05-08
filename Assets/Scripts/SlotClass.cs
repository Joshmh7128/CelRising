using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotClass : MonoBehaviour
{
    public int hasSand, hasRock, hasGrass, hasDirt;
    public TileClass heldTile;
    [SerializeField] RecipeManager recipeManager;

    private void Start()
    {
        recipeManager = FindObjectOfType<RecipeManager>();
    }

    public void CheckSlot()
    {
        switch ((int)heldTile.tileType)
        {
            case ((int)Generator.tileTypes.dirt):
                ResetValues();
                hasDirt = 1;
                break;
            case ((int)Generator.tileTypes.sand):
                ResetValues();
                hasSand = 1;
                break;
            case ((int)Generator.tileTypes.grass):
                ResetValues();
                hasGrass = 1;
                break;
            case ((int)Generator.tileTypes.rock):
                ResetValues();
                hasRock = 1;
                break;
        }

        // check our recipe
        recipeManager.RecipeChecker();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CleanSlot();
        }
    }

    void ResetValues()
    {
        hasSand = 0; hasRock = 0; hasGrass = 0; hasDirt = 0;
    }

    public void CleanSlot()
    {
        Destroy(heldTile.gameObject);
        heldTile = null;
        ResetValues();
    }
}
