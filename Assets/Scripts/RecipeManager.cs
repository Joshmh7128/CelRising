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

    private void Start()
    {
        generator = FindObjectOfType<Generator>();
    }

    // recipe checking
    public void RecipeChecker()
    {
        // simple dirt to sand
        if (slot1.hasDirt + slot2.hasDirt + slot3.hasDirt == 3)
        {
            // remove our crafting materials
            CraftItem(Generator.tileTypes.sand); 
        }

        // 2 dirt 1 rock to grass
        if ((slot1.hasDirt + slot2.hasDirt + slot3.hasDirt == 2) && (slot1.hasRock + slot2.hasRock + slot3.hasRock == 1))
        {
            // remove our crafting materials
            CraftItem(Generator.tileTypes.grass);
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
        slot1.CleanSlot();
        slot2.CleanSlot();
        slot3.CleanSlot();
    }
}
