using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenuMng : MonoBehaviour
{
    public GameObject[] paineis; // 0 - Menu, 1 - Sobre, 2 - Configuraçoes
    public Slider sldVFX;
    public Slider sldMusica;
    private Volume volumes;
    // Start is called before the first frame update
    void Start()
    {
        //Desbloquear o mouse
        DesbloquearMouse();

        //Exibir o painel Menu
        ExibirPainel(0);

        //Configurar painel Configurações
        ConfigurarPainelConfiguracoes();

        //Tocar o audio do menu
        AudioMng.Instance.PlayAudioMenu();

        //Ocultar a tela de carregamento
        CanvasLoadingMng.Instance.OcultarTelaDeCarregamento();
    }

    public void ExibirPainel(int id){
        //Ocultar todos os paineis
        foreach(var item in paineis){
            item.SetActive(false);
        }

        //Ativar o painel desejado
        paineis[id].SetActive(true);
    }

    public void FecharJogo(){
        Application.Quit();
    }

    public void Jogar(){
        //Exibir tela de loading
        CanvasLoadingMng.Instance.ExibirTelaDeCarregamento();

        //Carregar a cena do jogo
        SceneManager.LoadScene(1);
    }

    private void DesbloquearMouse(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ConfigurarPainelConfiguracoes(){
        //Obter os volumes 
        volumes = DBMng.ObterVolumes();

        //Atualizar os slides com os valores dos volumes
        sldVFX.value = volumes.vfx;
        sldMusica.value = volumes.musica;

        //Atualizar o audioMng com os volumes
        AudioMng.Instance.MudarVolume(volumes);
    }

    private void AtualizarVolumes(){
        volumes = DBMng.ObterVolumes();
        AudioMng.Instance.MudarVolume(volumes);
    }

    public void MudarVolumeVFX(){
        //Salvar o volume VFX novo junto com o volume da musica ao abrir o jogo
        DBMng.SalvarVolume(sldVFX.value,volumes.musica);

        //Atualizar os volumes no AudioMng
        AtualizarVolumes();
    }

    public void MudarVolumeMusica(){
        //Salvar o volume Musica novo junto com o volume da musica ao abrir o jogo
        DBMng.SalvarVolume(volumes.vfx,sldMusica.value);

        //Atualizar os volumes no AudioMng
        AtualizarVolumes();
    }
}
