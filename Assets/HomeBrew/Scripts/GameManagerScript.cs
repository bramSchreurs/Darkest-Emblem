using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    PlayerMovementScript selectedCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void HandleOnSelectedCharacterChanged(int xVal, int yVal) {
    // You can do anything with the value
    // that was passed in with the event
    UpdateMapVisuals(xVal, yVal);
    }

    private void UpdateMapVisuals(int xVal, int yVal) {

    }
}
