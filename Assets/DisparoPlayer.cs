using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPlayer : MonoBehaviour
{
    public ArmaControlador pistolaControlador; //Variavel para manipular a pistola do jogador
    public ArmaControlador fuzilControlador;
    public int idArmaAtiva = 1; //Diz qual arma está ativa, 1 - Pistola, 2 - Fuzil.
    public GameObject impactoBalaInimigo;
    public GameObject impactoBalaObjetos;
    private ArmaControlador armaAtiva;//O controlador da arma que está ativa

    public ArmaControlador ArmaAtiva {
        get {return armaAtiva;}
    }
    // Start is called before the first frame update
    void Start()
    {
        //Definir a arma inicial
        AtivarArma(idArmaAtiva);
    }

    // Update is called once per frame
    void Update()
    {
        SelecionarArma();
        DispararArma();
        RecarregarArma();
    }

    /// <summary>
    /// Método para simular o tiro da arma
    /// </summary>
    private void DispararArma(){
        //Verificar se a arma ativa é valida
        if(armaAtiva == null) return;

        //Verificar se o botão de atirar foi clicado
        if(Input.GetKey(KeyCode.Mouse0)){
            //Disparo a arma
            armaAtiva.Disparar();
        }
        //Verificar se o botão de atirar foi desclicado
        else if(Input.GetKeyUp(KeyCode.Mouse0)){
            //Para de atirar
            armaAtiva.CancelarDisparo();
        }
    }

    private void RecarregarArma(){
        //Verificar se o botão de recarregar foi clicado
        if(Input.GetKeyDown(KeyCode.R)){
            //Ativa a animação de recarregamento
            armaAtiva.RecarregarArma();
        }
    }

    /// <summary>
    /// Método para poder ativar a arma informada
    /// </summary>
    private void AtivarArma(int idArma){
        //Ativa ou desativa pistola
        pistolaControlador.gameObject.SetActive(idArma == 1);

        //Ativar ou desativar o fuzil
        fuzilControlador.gameObject.SetActive(idArma == 2);

        //Armazenar o script da arma que está ativa
        armaAtiva = idArma == 1 ? pistolaControlador : idArma == 2 ? fuzilControlador : null;

        //Atualiza o id da arma ativa
        idArmaAtiva = idArma;
    }

    private void SelecionarArma(){
        //Selecionar qual arma usar
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            //Ativar a pistola
            AtivarArma(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            //Ativar o fuzil
            AtivarArma(2);
        }
    }

    /// <summary>
    /// Método para desativar as armas após o jogador morrer ou o jogo acabar
    /// </summary>
    public void DesabilitarArmas(){
        pistolaControlador.gameObject.SetActive(false);
        fuzilControlador.gameObject.SetActive(false);
    }

    /// <summary>
    /// Método para dar dano aos inimigos e instanciar os efeitos de impacto
    /// </summary>
    public void DanoAoObjeto(){
        //Verificar se camera está vendo algum objeto
        if(PlayerMng.VisaoCamera.AlvoVisto != null){
            //Calcular a rotação inversa da colisão
            Quaternion rotacaoImpacto = Quaternion.FromToRotation(
                Vector3.forward,
                PlayerMng.VisaoCamera.hitAlvo.normal
            );

            //Verificar se está vendo o inimigo
            if(PlayerMng.VisaoCamera.tagAlvo == "Inimigo"){
                //Emitir o efeito de impacto com o inimigo
                Instantiate(
                    impactoBalaInimigo,
                    PlayerMng.VisaoCamera.hitAlvo.point,
                    rotacaoImpacto
                );

                //Decrementar a vida do inimigo
            }
            else{
                //Emitir o efeito de impacto com objetos que não sejam inimigos
                Instantiate(
                    impactoBalaObjetos,
                    PlayerMng.VisaoCamera.hitAlvo.point,
                    rotacaoImpacto
                );
            }
        }
    }

    public void IncrementarMunicaoPistola(int municao){
        //Acessar a pistola e incrementar a municao
        pistolaControlador.IncrementarMunicao(municao);
    }

    public void IncrementarMunicaoFuzil(int municao){
        //Acessar a fuzil e incrementar a municao
        fuzilControlador.IncrementarMunicao(municao);
    }
}
