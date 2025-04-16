using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaControlador : MonoBehaviour
{
    public float danoInimigo;
    public int municaoPorPente; //Munição máxima que o pente da arma pode ter
    public int municaoMaxima;//Total de munição máxima que arma pode ter
    public GameObject muzzleFlash; //Fogo que sai após atirar
    public GameObject capsula; //Capusa que será ejetada ao atirar
    public Transform posicaoCapsula; //Posição da casula a ser ejeta
    [SerializeField] private int pente; //A munção atual no pente da arma
    [SerializeField] private int municaoAtual;//Munição atual que a arma tem

    private Animator animator;

    public int Pente{
        get{return pente;}
    }

    public int MunicaoAtual{
        get{return municaoAtual;}
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //Definir o pente inicial da arma
        pente = municaoPorPente;

        //Definir a munição atual
        municaoAtual = municaoMaxima;
    }

    private void PlayDisparo(){
        //Configura a animação para poder simular o tiro
        animator.SetBool("Atirando", true);
        animator.SetBool("Parado", true);
        animator.SetBool("Recarregando", false);
    }

    private void PlayCancelarDisparo(){
        //Configura a animação para poder não atirar mais
        animator.SetBool("Atirando", false);
        animator.SetBool("Parado", true);
        animator.SetBool("Recarregando", false);
    }

    private void PlaySemMunicao(){
        //Configura a animação para mostrar a arma sem munição
        animator.SetBool("Atirando", false);
        animator.SetBool("Parado", false);
        animator.SetBool("Vazio", true);
    }

    private void PlayRecarregar(){
        //Configura a animação de recarregamento da arma
        animator.SetBool("Recarregando", true);
        animator.SetBool("Parado", true);
        animator.SetBool("Vazio", false);
    }

    /// <summary>
    /// Método acionado pelo player para poder disparar uma bala da arma
    /// </summary>
    public void Disparar(){
        //Verificar se tem bala no pente para atirar
        if(pente > 0){
            //Ativar animação de disparo
            PlayDisparo();
        }        
    }

    /// <summary>
    /// Método para poder parar de atirar
    /// </summary>
    public void CancelarDisparo(){
        //Ativar animação de parado
        PlayCancelarDisparo();
    }

    /// <summary>
    /// Método para poder recarregar a arma
    /// </summary>
    public void RecarregarArma(){
        //Verificar se a arma tem munição para recarregar e se o pente está sem bala
        if(municaoAtual > 0 && pente < municaoPorPente){
            //Obter a quantidade de balas que a arma precisa para preencher o pente
            int diferenca = municaoPorPente - pente;

            //Verificar se a diferença é menor que a quantidade de munição atual
            if(diferenca < municaoAtual){
                //Incrementar essa diferença no pente da arma
                pente += diferenca;
                
                //Decrementar essa diferença da munição atual
                municaoAtual -= diferenca;
            }
            else{
                //Incrementar a munição que possui no pente
                pente += municaoAtual;

                //Zerar a munição atual
                municaoAtual = 0;
            }
            //Ativar animação de recarregamento
            PlayRecarregar();
        }        
    }

    private void InstanciarBala(){
        //Emitir o dano ao objeto
        PlayerMng.DisparoPlayer.DanoAoObjeto();

        //Decrementar munição no pente
        pente--;

        //Ativar o MuzzleFlash
        muzzleFlash.SetActive(true);

        //Verificar se acabou a munição no pente
        if(pente <= 0){
            //Ativar a animação de sem munição
            PlaySemMunicao();

            //Forçar que o pente fique em 0
            pente = 0;
        }

        //Ejetar a capsula
        EjetarCapsula();
    }

    private void EjetarCapsula(){
        //Instanciar a casula
        GameObject novaCapsula = Instantiate(capsula);

        //Posicionar na posição e rotação da qual a capsula será ejetada
        novaCapsula.transform.position = posicaoCapsula.position;
        novaCapsula.transform.rotation = posicaoCapsula.rotation;

        //Chamar o método para ejetar a capula
        novaCapsula.GetComponent<EjetarCapsula>().Ejetar();
    }

    public void IncrementarMunicao(int municao){
        //Incrementar na munição atual a nova quantidade de municao
        municaoAtual += municao;

        //Verificar se a munição atual ultrapassou o limite máximo de munição
        if(municaoAtual > municaoMaxima){
            //Definir a munição maxima na munição atual
            municaoAtual = municaoMaxima;
        }
    }
}
