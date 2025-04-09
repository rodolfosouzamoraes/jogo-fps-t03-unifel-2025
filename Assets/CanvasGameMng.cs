using TMPro;
using UnityEngine;

public class CanvasGameMng : MonoBehaviour
{
    public static CanvasGameMng Instance;
    void Awake()
    {
        if(Instance == null){
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    [Header("Configurações Status Player")]
    public TextMeshProUGUI txtVida;
    public TextMeshProUGUI txtMunicao;
    public GameObject pnlStatusPlayer;
    private int vidaJogador;
    private int vidaMaximaJogador;

    // Start is called before the first frame update
    void Start()
    {
        //Definir a vida máxima que o jogador pode ter
        vidaMaximaJogador = 100;

        //Definir que o jogador comece com a vida cheia
        vidaJogador = vidaMaximaJogador;

        //Atualizar o texto da vida do jogador
        txtVida.text = $"+{vidaJogador}";
    }

    // Update is called once per frame
    void Update()
    {
        AtualizarMunicaoUI();
    }

    /// <summary>
    /// Método para atualizar em tempo real a munição atual da arma do jogador
    /// </summary>
    private void AtualizarMunicaoUI(){
        //Obter a info da munição do pente da arma
        int pente = PlayerMng.DisparoPlayer.ArmaAtiva.Pente;

        //Obter a munição atual da arma
        int municao = PlayerMng.DisparoPlayer.ArmaAtiva.MunicaoAtual;

        //Verificar se o pente ou a munição é inferior a 10 para poder colocar um zero a esquerda
        string valorPente = pente < 10 ? $"0{pente}" : $"{pente}";
        string valorMunicao = municao < 10 ? $"0{municao}" : $"{municao}";

        //Atualizo o texto da munição
        txtMunicao.text = $"{valorPente}/{valorMunicao}";
    }
}
