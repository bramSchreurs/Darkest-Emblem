using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBroker.CharacterSelected += SelectCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectCharacter(GameObject character) {
        Debug.Log("do we print this? we do btw");
    }
}
