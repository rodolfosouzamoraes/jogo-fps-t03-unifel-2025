using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChave : MonoBehaviour
{
    public AudioClip audioChave;
    private void OnTriggerEnter(Collider colisao)
    {
        if(colisao.gameObject.tag == "Player"){
            AudioMng.Instance.PlayAudioVFX(audioChave);

            //Incrementar uma chave
            CanvasGameMng.Instance.IncrementarChave();

            //Destruir objeto
            Destroy(gameObject);
        }
    }
}
