using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InimigoControlador : MonoBehaviour
{
    public float velocidade;
    public float distanciaParaAtacar;//Distancia do inimigo em relação ao player para ataca-lo
    public float distanciaPerseguicao;//Distancia para começar a perseguir o player
    public bool estaPerseguindo; //Define se o inimigo está perseguindo o player
    public LayerMask layerMask; //Os layers que o inimigo vai poder ver
    public int danoAoPlayer;//Valor de dano que o inimigo irá gerar no player
    public float vida; //vida do inimigo
    private NavMeshAgent agent; //IA do inimigo
    private CapsuleCollider capsuleCollider; //Referencia da capsula do inimigo
    private bool estaMorto;//Define se o inimigo morreu
    private bool estaVendoPlayer;//Define se o inimigo está vendo o player
    private SuporteAnimacaoInimigo animacaoInimigo; //Controlar as animações do inimigo


    // Start is called before the first frame update
    void Start()
    {
        //Referenciar a IA do inimigo
        agent = GetComponent<NavMeshAgent>();

        //Definir a velocidade de movimentação da IA do inimigo
        agent.speed = velocidade;

        //Definir que o inimigo não está vendo o player no inicio do jogo
        estaVendoPlayer = false;

        //Definir que o inimigo não está morto
        estaMorto = false;

        //Referenciar a capsula do inimigo
        capsuleCollider = GetComponent<CapsuleCollider>();

        //Referenciar o suporte da animação
        animacaoInimigo = GetComponentInChildren<SuporteAnimacaoInimigo>();
    }

    // Update is called once per frame
    void Update()
    {
        //Verificar se o inimigo está morto
        if(estaMorto == true) return;
        
        VisaoInimigo();
        PerseguirJogador();
    }

    /// <summary>
    /// Método para fazer a IA ir até o jogador
    /// </summary>
    private void PerseguirJogador(){
        //Calcular a distancia entre o jogador e o inimigo
        float distancia = Vector3.Distance(
            transform.position, 
            PlayerMng.Instance.transform.position
        );

        //Verificar se a distancia é a permitida para perseguir o jogador e se pode perseguir
        if(distancia < distanciaPerseguicao || estaPerseguindo == true){
            //Habilitar a perseguição do player
            estaPerseguindo = true;

            //Verificar se a distancia entre o inimigo e o jogador é maior que a distancia minima
            if(distancia > distanciaParaAtacar){
                //Fazer o inimigo ir até o jogador
                agent.destination = PlayerMng.Instance.transform.position;

                //Ativar a animação de corrida
                animacaoInimigo.PlayCorrendo();
            } 
            else{
                //Parar a movimentação do inimigo ao estiver muito próximo do player
                agent.destination = transform.position;
                
                //Definir a posição para onde o inimigo deve "Olhar"
                Vector3 posicaoJogador = new Vector3(
                    PlayerMng.Instance.transform.position.x,
                    transform.position.y,
                    PlayerMng.Instance.transform.position.z
                );

                //Fazer o inimigo olhar para o player
                transform.LookAt(posicaoJogador);

                //Ativar a animação de ataque
                animacaoInimigo.PlayAtacando();
            }
        }
        else{
            //Deixar o inimigo parado 
            agent.destination = transform.position;
            
            //Ativar a animação de parado
            animacaoInimigo.PlayParado();
        }
    }

    /// <summary>
    /// Método para definir se o inimigo está vendo o player
    /// </summary>
    public void VisaoInimigo(){
        RaycastHit hit;

        //Definir a posição onde o raio será emitido
        Vector3 posicaoVisibilidade = new Vector3(
            transform.position.x,
            1,
            transform.position.z
        );

        //Emitir o raio e obter as informações do objeto colidido
        if(Physics.Raycast(
            posicaoVisibilidade,
            transform.TransformDirection(Vector3.forward),
            out hit,
            10,
            layerMask)){
                //Desenhar a linha
                Debug.DrawRay(
                    posicaoVisibilidade,
                    transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow
                );

                //Definir que o inimigo está vendo o player
                estaVendoPlayer = true;
        }
        else{
            //Definir que o inimigo não está vendo o player
            estaVendoPlayer = false;
        }
    }

    public void DanoAoPlayer(){
        //Verificar se o inimigo está vendo o jogador para poder efetuar um dano
        if(estaVendoPlayer == true){
            //Efetuar um dano ao jogador
            CanvasGameMng.Instance.DecrementarVidaJogador(danoAoPlayer);
        }
    }

    public void DecrementarVida(float dano){
        //Verificar se o inimigo morreu
        if(estaMorto == true) return;

        //Decrementar a vida do inimigo
        vida -= dano;

        //Definir que o inimigo persiga o jogador
        estaPerseguindo = true;

        //Verificar se acabou a vida do inimigo
        if(vida <=0){
            //Definir que o inimigo morreu
            estaMorto = true;

            //Incrementar na contagem de inimigos mortos
            //...

            //Decrementar a quantidade de inimigos no jogo para instanciar novos
            //...

            //Definir que o inimigo fique na posição que ele morreu
            agent.destination = transform.position;

            //Ativar a animação de morte
            animacaoInimigo.PlayMorte();

            //Remover a capsula do inimigo
            Destroy(capsuleCollider);

            //Destruir o inimigo depois de um tempo
            Destroy(gameObject,5f);
        }
    }
}
