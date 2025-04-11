using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaoControlador : MonoBehaviour
{
    private void OnTriggerEnter(Collider colisao)
    {
        //Verificar se o jogador coletou todas as chaves
        if(CanvasGameMng.Instance.ColetouTodasAsChaves() == false) return;

        if(colisao.gameObject.tag == "Player"){
            //Finalizar o jogo
            CanvasGameMng.Instance.DefinirFimJogo();
        }
    }
}
