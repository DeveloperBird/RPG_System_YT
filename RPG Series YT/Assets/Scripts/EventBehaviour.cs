using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventBehaviour : ScriptableObject {

    public void TestEvent()
    {
        Debug.Log("Test event successful!");
        //any logic here
    }
}
