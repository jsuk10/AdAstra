using UnityEngine;
using System.Collections;

public class UIList : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            UIManager.Instance.SetDirctionary(go.name, go);


        }
    }
}
