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
        if (slot1.hasDirt + slot2.hasDirt + slot3.hasDirt > 2)
        {
            // remove our crafting materials
            CleanSlots(); 
            // spawn our new one
            Instantiate(generator.tileTypeList[(int)Generator.tileTypes.sand], resultSpot.position, Quaternion.identity, null);
        }
    }

    private void CleanSlots()
    {
        slot1.CleanSlot();
        slot2.CleanSlot();
        slot3.CleanSlot();
    }
}
