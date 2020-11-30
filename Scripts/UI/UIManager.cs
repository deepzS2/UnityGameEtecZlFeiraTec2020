using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _prestigeText;

    [SerializeField]
    private Text _scoreText;

    private GameManager _gameManager;

    //Banco de dados variáveis
    private databaseMain _databaseMain;

    private int _score;

    [SerializeField]
    private Text _scoreName;

    //GameOver variáveis
    [SerializeField]
    private GameObject _gameOverPanel;

    [SerializeField]
    private Text _gameOverScoreText;

    [SerializeField]
    private Text _gameOverRecordText;

    [SerializeField]
    private GameObject _sendButton;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            _databaseMain = GameObject.FindGameObjectWithTag("GameManager").GetComponent<databaseMain>();
        }

        if (_prestigeText)
        {
            // 3 pontos de prestigio por padrão
            _prestigeText.text = "3";
        }

        if (_scoreText)
        {
            // 0 pontos de score por padrão;
            _scoreText.text = "0";
        }

        if (_gameManager)
        {
            // Carrega o script GameManager
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    /// <summary>
    /// Função responsável por modificar o texto com o prestigio atual
    /// </summary>
    /// <param name="prestige">O prestigio atual</param>
    public void updatePrestige(int prestige)
    {
        // Define o texto com o prestígio atual - Lembre-se toString() para não haver conflito 
        // de tipo de variável, onde text é string e score é integer
        _prestigeText.text = prestige.ToString();
    }

    /// <summary>
    /// Função responsável por modificar o texto com a pontuação atual
    /// </summary>
    /// <param name="score">A pontuação atual</param>
    public void updateScore(int score)
    {
        // Define o texto com a pontuação atual - Lembre-se toString() para não haver conflito 
        // de tipo de variável, onde text é string e score é integer
        _scoreText.text = score.ToString();
    }
 
    /// <summary>
    /// Função responsável para mudar a cena, quando pressionado um botão
    /// </summary>
    public void buttonChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Função responsável pelo botão pressionado "Exit"
    /// </summary>
    public void buttonExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Função responsável por mostrar a tela de game over
    /// </summary>
    /// <param name="score">Score do player para mostrar na tela de game over</param>
    public void DisplayGameOver(int score)
    {
        _score = score;
        _gameOverPanel.SetActive(true);
        _gameOverScoreText.text = "" + score;
        _gameOverRecordText.text = PlayerPrefs.GetInt("bestScore").ToString();
    }

    /// <summary>
    /// Salva o score se o player pressionar o botão de salvar
    /// </summary>
    public void saveScore()
    {
        _databaseMain.saveData(_score, _scoreName.text);
        Destroy(_sendButton);
    }
}
