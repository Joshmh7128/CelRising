using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// 
    /// this script manages all gameplay elements and is referenced by objects throughout play
    /// 


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
    }

    private void UpdateWindDirection()
    {

    }

}
