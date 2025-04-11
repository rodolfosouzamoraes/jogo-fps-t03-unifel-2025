using TMPro;
using Unity.VisualScripting;
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

    public bool fimDeJogo;

    [Header("Configurações Status Player")]
    public TextMeshProUGUI txtVida;
    public TextMeshProUGUI txtMunicao;
    public GameObject pnlStatusPlayer;
    [SerializeField] private int vidaJogador;
    private int vidaMaximaJogador;

    [Header("Configurações Topo")]
    public GameObject pnlTopo;
    public GameObject[] iconesChaves;
    public TextMeshProUGUI txtObjetivo;
    public TextMeshProUGUI txtTempo;
    private int totalChavesColetadas;
    private float tempoJogo;

    [Header("Configurações Fim de Jogo")]
    public GameObject pnlFimDeJogo;
    public TextMeshProUGUI txtTempoFinal;
    public TextMeshProUGUI txtTotalZumbisMortos;
    private int totalZumbisMortos;
    private int tempoFinal;

    // Start is called before the first frame update
    void Start()
    {
        //Definir a vida máxima que o jogador pode ter
        vidaMaximaJogador = 100;

        //Definir que o jogador comece com a vida cheia
        vidaJogador = vidaMaximaJogador;

        //Atualizar o texto da vida do jogador
        txtVida.text = $"+{vidaJogador}";

        //Definir a quantidade de chaves coletadas no inicio do jogo
        totalChavesColetadas = 0;

        //Definir o objetivo do jogo no inicio
        txtObjetivo.text = $"Colete as 7 chaves!";

        //Definir o tempo para 0
        tempoJogo = 0;

        //Atualizar o tempo no texto
        txtTempo.text = $"{tempoJogo}";

        //Definir o fim de jogo para false
        fimDeJogo = false;

        //Definir o total de zumbis mortos para 0
        totalZumbisMortos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AtualizarMunicaoUI();
        ContarTempo();
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

    public void IncrementarVidaJogador(){
        //Atribuir a vida ao jogador
        vidaJogador += 25;

        //Verificar se a vida ultrapassou o limite máximo
        if(vidaJogador > vidaMaximaJogador){
            //Defino a vida máxima ao jogador
            vidaJogador = vidaMaximaJogador;
        }

        //Atualizar o texto da vida máxima
        txtVida.text = $"+{vidaJogador}";
    }

    public void IncrementarChave(){
        //Incrementar o total de chaves
        totalChavesColetadas++;

        //Ativar o icone da chave coletada
        iconesChaves[totalChavesColetadas].SetActive(true);

        //Verificar se todas as chaves foram coletadas
        if(ColetouTodasAsChaves()){
            //Mudo o objetivo do jogo
            txtObjetivo.text = "Encontre o portão final!";
        }
    }

    public bool ColetouTodasAsChaves(){
        //returnar o valor true se de fato todas as chaves forem coletadas
        return totalChavesColetadas == iconesChaves.Length -1 ? true : false;
    }

    private void ContarTempo(){
        //Incrementar o tempo na variável
        tempoJogo = Time.time;

        //Atualizar o tempo no texto
        txtTempo.text = $"{(int)tempoJogo}";
    }

    public void DefinirFimJogo(){
        //Dizer que o jogo acabou
        fimDeJogo = true;

        //Ocultar o painel de status e topo
        pnlStatusPlayer.SetActive(false);
        pnlTopo.SetActive(false);

        //...

        //Exibir tela final
        ExibirTelaFinal();
    }

    public void ExibirTelaFinal(){
        //Obter o tempo final do jogo
        tempoFinal = (int)tempoJogo;
        
        //Exibir o tempo final no texto
        txtTempoFinal.text = $"{tempoFinal}";

        //Exibir o total de zumbis mortos no texto
        txtTotalZumbisMortos.text = $"{totalZumbisMortos}";

        //Exibir o painel de fim de jogo
        pnlFimDeJogo.SetActive(true);

        //Salvar os dados

        //Desabilitar as armas
        PlayerMng.DisparoPlayer.DesabilitarArmas();

        //Desbloquear o mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
