using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// 
    /// this script manages all gameplay elements and is referenced by objects throughout play
    /// 

    // game mechanics
    public float energyAmount; // how much energy are we producing?

    // UI elements
    [SerializeField] Text energyTextDisplay;

    // tick time management
    public float tickTime; // our tick time in seconds
    
    // start runs at the beginning of the game
    private void Start()
    {
        StartCoroutine(RunTickTime());
    }

    // create a custom tick time for the game to do checks and changes, counts and timing
    public IEnumerator RunTickTime()
    {
        yield return new WaitForSeconds(tickTime);
        TickTime();
        StartCoroutine(RunTickTime());
    }

    public void TickTime()
    {
        // update our UI
        UpdateUI();

    }

    private void UpdateWindDirection()
    {

    }

    // all UI updates go here
    void UpdateUI() 
    {
        // update our energy text
        energyTextDisplay.text = energyAmount.ToString();
    }
}
