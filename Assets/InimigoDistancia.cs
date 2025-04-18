using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDistancia : InimigoControlador
{
    public GameObject projetilZumbi;

    public void AtaqueDistancia(){
        //Instanciar o projetil
        GameObject novoProjetil = Instantiate(projetilZumbi);

        //Definir o dano do projetil
        novoProjetil.GetComponent<ProjetilInimigo>().AtualizaDanoJogador(danoAoPlayer);

        //Posicionar o projetil na posição e rotação
        novoProjetil.transform.position = transform.position + Vector3.up;
        novoProjetil.transform.rotation = transform.rotation;
    }
}
