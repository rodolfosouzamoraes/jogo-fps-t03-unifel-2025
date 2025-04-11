using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChave : MonoBehaviour
{
    private void OnTriggerEnter(Collider colisao)
    {
        if(colisao.gameObject.tag == "Player"){
            //Incrementar uma chave
            CanvasGameMng.Instance.IncrementarChave();

            //Destruir objeto
            Destroy(gameObject);
        }
    }
}
