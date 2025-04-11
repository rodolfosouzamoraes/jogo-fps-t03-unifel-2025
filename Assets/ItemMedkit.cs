using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMedkit : MonoBehaviour
{
    private void OnTriggerEnter(Collider colisao)
    {
        if(colisao.gameObject.tag == "Player"){
            //Incrementar a vida no jogador
            CanvasGameMng.Instance.IncrementarVidaJogador();

            //Destruir o objeto
            Destroy(gameObject);
        }
    }
}
