using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string testString = "Test";

    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(testString);
        Debug.Log("Update");
    }
}
