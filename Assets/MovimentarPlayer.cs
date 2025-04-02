using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovimentarPlayer : MonoBehaviour
{
    [Header("Configurações Camera")]
    private Camera playerCamera; //Variavel com a referencia da camera do jogo
    public float velocidadeCamera; //Velocidade de rotação da camera do player
    public float limiteCameraAnguloX; //Defini o limite do angulo que o jogador pode ter ao olhar para cima ou baixo
    private float cameraRotacaoX; //Armazena o valor da rotacao em X da camera
    
    [Header("Configurações Movimento")]
    public float velocidadeCaminhada; //Velocidade de caminhada do jogador
    public float velocidadeCorrida; //Velocidade de corrida do jogador
    public float forcaPulo; //Define a força de subida do objeto
    public float forcaGravidade; //Define a força de decida do objeto refente a sua gravidade
    private Vector3 direcaoMovimento; //Define a direção para onde o objeto deve ir
    private CharacterController characterController; //Variavel de controle do personagem
    // Start is called before the first frame update
    void Start()
    {
        //Obter a referencia da camera principal da cena
        playerCamera = Camera.main;

        //Configurar a variavel do Character Controller
        characterController = GetComponent<CharacterController>();

        //Setar a direção inicial para zero quando o jogo iniciar
        direcaoMovimento = Vector3.zero;

        //Travar e ocular o mouse no inicio do jogo
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimentar o player nos eixos X, Y e Z
        MovimentarXYZ();

        //Movimentar a camera do player
        RotacionarCameraEmX();

        //Rotacionar player em Y
        RotacionarY();
    }

    private void MovimentarXYZ(){

        //Obter a Referencia da frente do player
        Vector3 frente = transform.TransformDirection(Vector3.forward); 

        //Obter a referencia da direita do player
        Vector3 direita = transform.TransformDirection(Vector3.right);

        //Obter o input que faz o jogador correr
        bool estaCorrendo = Input.GetKey(KeyCode.LeftShift);

        //Calcular a velocidade de movimento para frente
        float velocidadeFrente = estaCorrendo ? velocidadeCorrida : velocidadeCaminhada;
        
        //Definir o movimento para frente ou para tras
        velocidadeFrente *= Input.GetAxis("Vertical");

        //Calcular a velocidade de movimento nas laterais
        float velocidadeLateral = estaCorrendo ? velocidadeCorrida : velocidadeCaminhada;
        
        //Definir o movimento para esquerda ou direita
        velocidadeLateral *= Input.GetAxis("Horizontal");

        //Definir a direção inicial do eixo Y
        float direcaoY = direcaoMovimento.y;

        //Calcular a direcao do player
        direcaoMovimento = (frente * velocidadeFrente) + (direita * velocidadeLateral);

        //Verificar se o jogador está em contato com o chão para poder efetuar o pulo
        if(Input.GetButton("Jump") && characterController.isGrounded == true){
            //Definir a direção em Y para cima e assim efetuar o pulo posteriormente
            direcaoMovimento.y = forcaPulo;
        }
        else{
            //Definir a direção do movimento em Y no valor referenciado
            direcaoMovimento.y = direcaoY;
        }

        //Verificar se o jogador não está mais no chão para fazer ele cair
        if(characterController.isGrounded == false){
            //Apontar a direção em Y para baixo, fazendo o objeto descer
            direcaoMovimento.y -= forcaGravidade * Time.deltaTime;
        }
        
        //Movimentar o player
        characterController.Move(direcaoMovimento * Time.deltaTime);
    }

    private void RotacionarCameraEmX(){
        
        //Obter o input do mouse em Y para alterar a rotacao em X da camera
        cameraRotacaoX += -Input.GetAxis("Mouse Y") * velocidadeCamera;

        //Limiter a rotacao em X da camera
        cameraRotacaoX = Mathf.Clamp(cameraRotacaoX,-limiteCameraAnguloX,limiteCameraAnguloX);

        //Rotacionar a camera para a rotação desejada em X
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotacaoX,0,0);
    }

    private void RotacionarY(){
        //Obter input do mouse em X
        float rotacaoY = Input.GetAxis("Mouse X") * velocidadeCamera;

        //Definir a rotação do player
        transform.rotation *= Quaternion.Euler(0, rotacaoY  ,0);
    }
}
