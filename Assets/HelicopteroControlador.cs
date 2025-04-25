using UnityEngine;

public class HelicopteroControlador : MonoBehaviour
{
    public Animator animator;
    public GameObject cameraHelicoptero; //Game object da camera que filma o helicoptero
    public Transform[] posicoesCamera;//Posicoes onde a camera do helicoptero vai se posicionar
    public AudioSource audioSource;
    private int posicaoCamera;//Identificador da posicao da camera
    // Start is called before the first frame update
    void Start()
    {
        posicaoCamera = 0;
        cameraHelicoptero.transform.position = posicoesCamera[posicaoCamera].position;
        cameraHelicoptero.SetActive(false);
        
        audioSource.volume = AudioMng.Instance.volumeVFX;
    }
    
    public void IniciarVoo(){
        //Ativar a animação de voo
        animator.SetBool("EstaVoando",true);
    }

    public void ExibirTelaFimDeJogo(){
        //Exbir a tela após o fim da animação de voo
        CanvasGameMng.Instance.ExibirTelaFinal();
    }

    public void MoverCameraProximaPosicao(){
        //Incrementar na posição da camera
        posicaoCamera++;

        //Posicionar a camera na proxima posicao
        cameraHelicoptero.transform.position = posicoesCamera[posicaoCamera].position;
        cameraHelicoptero.transform.rotation = posicoesCamera[posicaoCamera].rotation;
    }

    public void AtivarCameraHelicoptero(){
        cameraHelicoptero.SetActive(true);
    }
}
