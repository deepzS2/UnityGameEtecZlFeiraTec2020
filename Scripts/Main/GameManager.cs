using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Joystick do level
    [SerializeField]
    private GameObject _joystick;

    // Script de controle dos elementos da tela
    private UIManager _uiManager;

    // Script que faz o spawn dos itens e dos peixes
    private SpawnerManager _spawnerManager;

    // Script do player
    private MainCharacter _mainCharacter;

    // Script do AudioManager
    private AudioManager _audioManager;

    // Bool de som
    private bool _musicBoss;

    // Prefab do tubarão
    [SerializeField]
    private GameObject _shark;

    // Painel de configurações
    [SerializeField]
    private GameObject _options;

    // Booleano que define se o painel de configurações tá aberto
    private bool _isOptionOpen = false;

    // Booleano que define se o jogador perdeu
    public bool gameOver = false;

    // Booleano que define se o boss deve aparecer
    public bool bossTime;

    public float bossDuration;

    public int _bossScore = 150;

    private float _originalBossDuration;

    // deletar todos os gameObjects da tela
    private GameObject[] _Fish;

    private GameObject[] _Trash;

    private GameObject _Character;

    private GameObject _Boss;

    private GameObject _player;

    void Start()
    {

        // Procura o Canvas e pega o script de UIManager
        if (GameObject.FindGameObjectWithTag("UI"))
        {
            _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        }
        
        // Procura no GameObject o script de spawner
        _spawnerManager = this.gameObject.GetComponent<SpawnerManager>();

        // Procura o script do player
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _mainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>();
        }

        // Chama o método que começa a spawnar as coisas
        _spawnerManager.StartSpawning();

        if (GameObject.FindGameObjectWithTag("AudioManager"))
        {
            // Procura o script AudioManager dentro do GameObject com a tag AudioManager
            _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        // Confere se o dispositivo que tá rodando o jogo é um celular, e se sim, ativa o joystick
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            _joystick.SetActive(true);
        }

        bossDuration = Random.Range(10, 30);
        _originalBossDuration = bossDuration;
    }

    // Update is called once per frame
    void Update()
    {
        // Aguarda o ESC ser pressionado, quando pressionado, chama o método OpenCloseOptions
        if (Input.GetKeyDown("escape"))
        {
            OpenCloseOptions();
        }

        // Verifica se o score do player é maior que 100, se for maior que 100, define o bossTime como true
        if (_mainCharacter._score >= _bossScore)
        {
            // Se o boss estiver na cena em pouco tempo toque a musica
            // tantantan
            if (bossDuration > 0)
            {
                if (!_musicBoss)
                {
                    StartCoroutine(_audioManager.fadeOut(1));
                    _musicBoss = true;
                }

                bossDuration -= Time.deltaTime;
                bossTime = true;
            }

            // Após isso vai diminuindo a música e começa a spawnar
            if (bossDuration <= 0)
            {
                StartCoroutine(_audioManager.fadeOut(0));

                _musicBoss = false;

                bossTime = false;

                _spawnerManager.StartSpawning();

                _bossScore = _mainCharacter._score + 150;

                bossDuration = _originalBossDuration;
            }
        }

    }



    /// <summary>
    /// Método que ativa e desativa o painel de configurações e troca o valor do _isOptionOpen para o seu oposto
    /// </summary>
    public void OpenCloseOptions()
    {
        _options.SetActive(!_isOptionOpen);
        _isOptionOpen = !_isOptionOpen;
        if (_isOptionOpen)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Deleta todos os objetos em cena
    /// </summary>
    public void deleteAll()
    {
        _Fish = GameObject.FindGameObjectsWithTag("Fish");
        _Trash = GameObject.FindGameObjectsWithTag("Trash");
        _Character = GameObject.FindGameObjectWithTag("Player");
        _Boss = GameObject.FindGameObjectWithTag("Boss");

        int i;

        for (i = 0; i < _Fish.Length; i++)
        {
            Destroy(_Fish[i]);
        }
        for (i = 0; i < _Trash.Length; i++)
        {
            Destroy(_Trash[i]);
        }
        Destroy(_Boss);
        Destroy(_Character);
    }
}