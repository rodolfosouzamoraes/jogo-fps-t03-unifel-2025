using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuporteAnimacaoInimigo : MonoBehaviour
{
    private Animator animator;
    private InimigoControlador inimigoControlador;
    private InimigoDistancia inimigoDistancia;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inimigoControlador = GetComponentInParent<InimigoControlador>();

        //Sortear a animação de parado
        int idIdle = new System.Random().Next(1,6);
        animator?.SetFloat("id_parado",idIdle);

        //Sortear a animação de morte
        int idMorte = new System.Random().Next(1,3);
        animator?.SetFloat("id_morte",idMorte);

        //Referenciar o script do inimigo distancia
        try{
            inimigoDistancia = GetComponentInParent<InimigoDistancia>();
        }catch{}
    }

    public void PlayParado(){
        animator?.SetBool("Correndo", false);
        animator?.SetBool("Atacando", false);
        animator?.SetBool("Parado", true);
    }

    public void PlayCorrendo(){
        animator?.SetBool("Correndo", true);
        animator?.SetBool("Atacando", false);
        animator?.SetBool("Parado", false);
    }

    public void PlayAtacando(){
        animator?.SetBool("Correndo", false);
        animator?.SetBool("Atacando", true);
        animator?.SetBool("Parado", false);
    }

    public void PlayMorte(){
        animator?.SetTrigger("Morte");
    }

    /// <summary>
    /// Método acionado na animação de dano
    /// </summary>
    public void DanoAoPlayer(){
        //Efetuar o dano ao jogador durante a animação de ataque
        inimigoControlador.DanoAoPlayer();
    }

    public void AtaqueDistancia(){
        inimigoDistancia.AtaqueDistancia();
    }

}
