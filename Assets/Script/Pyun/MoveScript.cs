using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string testString = "Test";
    [SerializeField]
    Rigidbody2D tb;

    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        tb.AddForce(new Vector2(0,1));
    }
}
