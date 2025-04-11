using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemMunicao : MonoBehaviour
{
    public GameObject pentePistola; 
    public GameObject penteFuzil;
    public TextMeshProUGUI txtQtdMunicao;
    private int municaoParaPistola;
    private int municaoParaFuzil;
    private int idArma; //1 - Pistola / 2 - Fuzil
    // Start is called before the first frame update
    void Start()
    {
        //Desativar os gameObjects das armas
        pentePistola.SetActive(false);
        penteFuzil.SetActive(false);

        //Sortear a munição a ser gerada
        idArma = new System.Random().Next(1,3);
        //Verificar qual arma foi sorteada
        switch(idArma){
            case 1:
                //Gerar munição aleatória para a pistola
                municaoParaPistola = new System.Random().Next(5,31);

                //Exibir o valor da munição no texto
                txtQtdMunicao.text = $"x{municaoParaPistola}";

                //Ativar o gameobject da pistola
                pentePistola.SetActive(true);
            break;
            case 2:
                //Gerar munição aleatória para a fuzil
                municaoParaFuzil = new System.Random().Next(15,61);

                //Exibir o valor da munição no texto
                txtQtdMunicao.text = $"x{municaoParaFuzil}";

                //Ativar o gameobject da fuzil
                penteFuzil.SetActive(true);
            break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Fazer o texto "Olhar" para o jogador
        txtQtdMunicao.transform.LookAt(
            //Definir a coordenada do objeto a ser "Olhado"
            new Vector3(
                PlayerMng.Instance.transform.position.x,
                txtQtdMunicao.transform.position.y,
                PlayerMng.Instance.transform.position.z
            )
        );
    }

    private void OnTriggerEnter(Collider colisao)
    {
        //Verificar se o player colidiu com a munição
        if(colisao.gameObject.tag == "Player"){
            //Verificar qual pente é a do objeto para poder incrementar no player
            switch(idArma){
                case 1:
                    PlayerMng.DisparoPlayer.IncrementarMunicaoPistola(municaoParaPistola);
                break;
                case 2:
                    PlayerMng.DisparoPlayer.IncrementarMunicaoFuzil(municaoParaFuzil);
                break;
            }

            //Destruir o objeto
            Destroy(gameObject);
        }
    }
}
