using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMedkit : MonoBehaviour
{
    public AudioClip audioMedkit;
    private void OnTriggerEnter(Collider colisao)
    {
        if(colisao.gameObject.tag == "Player"){
            //Tocar o audio do medkit
            AudioMng.Instance.PlayAudioVFX(audioMedkit);

            //Incrementar a vida no jogador
            CanvasGameMng.Instance.IncrementarVidaJogador();

            //Destruir o objeto
            Destroy(gameObject);
        }
    }
}
