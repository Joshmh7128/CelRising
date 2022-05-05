using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecipeManager : MonoBehaviour
{
    /// this script checks the combinations of different objects in slots
    public SlotClass slot1, slot2, slot3;
    [SerializeField] Transform resultSpot; // where our results go
    Generator generator; // our generator

    // our final crafting values
    float finalSand, finalGrass, finalDirt, finalRock;

    private void Start()
    {
        generator = FindObjectOfType<Generator>();
    }

    // recipe checking
    public void RecipeChecker()
    {
        // run our check values for each crafting resource
        finalSand = slot1.hasSand + slot2.hasSand + slot3.hasSand;
        finalDirt = slot1.hasDirt + slot2.hasDirt + slot3.hasDirt;
        finalGrass = slot1.hasGrass + slot2.hasGrass + slot3.hasGrass;
        finalRock = slot1.hasRock + slot2.hasRock + slot3.hasRock;

        // all of our crafting checks
        CraftingCheckWindmill();
    }

    void CraftingCheckWindmill()
    {
        // the recipe for windmill is 2 grass and 1 sand
        if (finalGrass == 2 && finalSand == 1)
        {
            CraftItem(Generator.tileTypes.windmill);
        }

    }

    void CraftItem(Generator.tileTypes tileType)
    {
        // remove our crafting materials
        CleanSlots();
        // spawn our new one
        Instantiate(generator.tileTypeList[(int)tileType], resultSpot.position, Quaternion.identity, null).GetComponent<TileClass>().inSlot = true;

    }

    private void CleanSlots()
    {
        finalSand = 0;
        finalGrass = 0; 
        finalDirt = 0;
        finalRock = 0;

        slot1.CleanSlot();
        slot2.CleanSlot();
        slot3.CleanSlot();
    }
}
