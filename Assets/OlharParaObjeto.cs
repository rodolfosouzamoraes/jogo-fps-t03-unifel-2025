using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlharParaObjeto : MonoBehaviour
{
    public GameObject alvo;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(alvo.transform);
    }
}
