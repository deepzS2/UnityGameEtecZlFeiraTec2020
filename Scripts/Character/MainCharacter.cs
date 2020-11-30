using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainCharacter : MonoBehaviour
{
    //variável do prestígio
    private int _prestige = 3;

    //variável de pontuação
    public int _score = 0;

    private UIManager _uiManager;

    private GameManager _gameManager;

    private SpawnerManager _spawnerManager;

    private AudioManager _audioManager;


    //função será chamada quando iniciar o papel do script com alguem gameObject

    void Start()
    {
        // Procura o Canvas e pega o script de UIManager
        if (GameObject.FindGameObjectWithTag("UI")) {
            _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        }

        // Procura os scripts de GameManager e SpawnerManager
        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            _spawnerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SpawnerManager>();
        }

        // Procura o script de AudioManager
        if (GameObject.FindGameObjectWithTag("AudioManager")) {
            _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }
        
    }

    //função que vai ser chamada a cada frame

    void Update()
    {
        
    }

    /// <summary>
    /// Função para a perca de prestígio
    /// </summary>
    public void lostPrestige()
    {
        //Perde um de Prestige
        _prestige--;

        // Chama a função que modifica o texto de prestígio
        // Veja a documentação no script UIManager
        _uiManager.updatePrestige(_prestige);

        //Se a variável Prestígio chegar a 0, game over
        if(_prestige <= 0)
        {
            //Game over
            _gameManager.gameOver = true;

            StartCoroutine(_audioManager.fadeOut(2));

            _spawnerManager.SpawnShark();
        }
    }

    /// <summary>
    /// Função pra acrescentar pontuação
    /// </summary>
    /// <param name="points">Os pontos que o player ganhou</param>
    public void appendScore(int points)
    {
        //Aumenta a pontuação
        _score += points;

        // Chama a função que modifica o texto de score
        // Veja a documentação no script UIManager
        _uiManager.updateScore(_score);

        //checa se o score atual é maior do que o highscore salvo no PlayerPrefs
        if(_score > PlayerPrefs.GetInt("HighScore")){
            //salva o score atual no HighScore
            PlayerPrefs.SetInt("HighScore", _score);
        }
    }
    
}
