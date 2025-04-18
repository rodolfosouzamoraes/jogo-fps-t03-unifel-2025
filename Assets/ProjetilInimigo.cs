using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilInimigo : MonoBehaviour
{
    public float velocidade;
    private int danoAoJogador;
    // Start is called before the first frame update
    void Start()
    {
        //Destruir o objeto depois de um tempo
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //Movimentar o objeto para frente
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }

    public void AtualizaDanoJogador(int dano){
        danoAoJogador = dano;
    }

    void OnTriggerEnter(Collider colisao)
    {
        //Verificar se colidiu com o player
        if(colisao.gameObject.tag == "Player"){
            //Decrementar a vida do jogador
            CanvasGameMng.Instance.DecrementarVidaJogador(danoAoJogador);
        }
        Destroy(gameObject);
    }
}
