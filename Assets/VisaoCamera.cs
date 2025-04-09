using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisaoCamera : MonoBehaviour
{
    private GameObject alvo; //Variavel que vai armazenar o gameobject do objeto alvo
    public string tagAlvo;//A tag do alvo visto
    public RaycastHit hitAlvo; //Obter os dados do objeto

    public GameObject AlvoVisto{
        get{return alvo;}
    }
    // Start is called before the first frame update
    void Start()
    {
        //Definir que o alvo inicial seja vazio
        alvo = null;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastCamera();
    }

    /// <summary>
    /// Método para emitir um Raycast e identificar o objeto "visto" pela camera
    /// </summary>
    private void RaycastCamera(){
        //Criar o raio que vai partir da camera do jogador
        Ray raio = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));

        //Criar uma variavel que vai armazenar a informação do objeto "visto"
        RaycastHit hit;

        //Verificar se o raio encontrou algo
        if(Physics.Raycast(raio, out hit,Mathf.Infinity)){
            //Desenhar o raio em colisao com o objeto
            Debug.DrawRay(
                transform.position, 
                transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.red
            );

            //Armazenar o alvo que está vendo
            alvo = hit.transform.gameObject;

            //Armazenar a tag do alvo
            tagAlvo = hit.transform.tag;

            //Armazenar o hit alvo 
            hitAlvo = hit;
        }
        else{
            //Caso não encontre nada, zerar as variaveis
            tagAlvo = "";
            alvo = null;
        }
    }
}
