using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

public class InstanciarInimigos : MonoBehaviour
{
    public GameObject[] inimigos;
    public float tempoDeEspera;
    public int maximoInimigosNaFase; //Definir o máximo de inimigos que pode ter no jogo
    public float distanciaInicialParaNovoInimigo; //Distancia quando o jogo começar 
    public float distanciaParaNovoInimigo; //Distancia para novos inimigos que surgirem após o inicio do jogo
    private float tempoProximoInimigo;
    private int totalInimigosInstanciados; //Armazena o total de inimigos instanciados em tempo real no jogo

    //Variavel de audios dos zumbis
    //...

    // Start is called before the first frame update
    void Start()
    {
        //Definir 0 como numero inicial da variavel de quantidade de inimigos será zero
        totalInimigosInstanciados = 0;

        //Definir um tempo para a instancia de um próximo inimigo
        tempoProximoInimigo = tempoDeEspera + Time.time;

        //Instanciar os primeiros inimigos
        for(int i = 0; i < maximoInimigosNaFase;i++){
            InstanciarInimigo(distanciaInicialParaNovoInimigo,distanciaInicialParaNovoInimigo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Verificar se é possível instanciar novos inimigos
        if((totalInimigosInstanciados < maximoInimigosNaFase) && Time.time > tempoProximoInimigo){
            //Atualizar o tempo de espera para o próximo inimigo
            tempoProximoInimigo = Time.time + tempoDeEspera;

            //Instanciar o inimigo
            InstanciarInimigo(distanciaParaNovoInimigo, distanciaParaNovoInimigo);
        }
    }

    private void InstanciarInimigo(float distanciaMaxX, float distanciaMaxZ){
        //Definir a posição Z que o inimigo irá surgir
        float posicaoZ = Random.Range(
            PlayerMng.Instance.transform.position.z - distanciaMaxZ,
            PlayerMng.Instance.transform.position.z + distanciaMaxZ
        );

        //Definir a posição do X que o inimigo irá surgir
        float posicaoX = Random.Range(
            PlayerMng.Instance.transform.position.x - distanciaMaxX,
            PlayerMng.Instance.transform.position.x + distanciaMaxX
        );

        //Localizar o inimigo na area azul do NavMesh
        NavMeshHit posicaoFinal;
        NavMesh.SamplePosition(
            new Vector3(posicaoX, 0, posicaoZ),
            out posicaoFinal,
            Mathf.Infinity,
            1
        );

        //Sortear inimigo a ser instanciado
        int inimigoSorteado = new System.Random().Next(0,inimigos.Length);

        //Instanciar o inimigo
        var novoInimigo = Instantiate(inimigos[inimigoSorteado]);

        //Referenciar o novo inimigo com o script InstanciarInimigos
        novoInimigo.GetComponent<InimigoControlador>().ReferenciarInimigo(this);

        //Configurar o audio do inimigo sorteado
        //...

        //Posicionar o zumbi na posição sorteada
        var agent = novoInimigo.GetComponent<NavMeshAgent>();

        //Desativar o agent antes de posicionar o inimigo
        agent.enabled = false;

        //Posicionar o inimigo na posição correta
        novoInimigo.transform.position = posicaoFinal.position;

        //Ativar novamente o agent
        agent.enabled = true;

        //Sortear uma rotação para o inimigo
        var rotacaoSorteada = Quaternion.Euler(0,new System.Random().Next(0,361),0);

        //Definir a rotação do inimigo
        novoInimigo.transform.rotation = rotacaoSorteada;

        //Incrementar inimigos na variavel
        totalInimigosInstanciados++;
    }

    public void DecrementarInimigosInstanciados(){
        totalInimigosInstanciados--;
    }
}
