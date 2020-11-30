using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour {

    // Prefab do peixe
    [SerializeField]
    private GameObject _fishGameObject;

    // Prefab do lixo
    [SerializeField]
    private GameObject _itemGameObject;

    // Prefab do boss
    [SerializeField]
    private GameObject _boss;

    // Array que guarda os GameObjects encontrados com a tag boss
    public GameObject[] _bossSearch;

    [SerializeField]
    private GameObject _shark;

    // Timer entre os loops
    [SerializeField]
    private float _coroutineTimer;

    // Script de gerenciamento do jogo
    private GameManager _gameManager;

    // Script de gerenciamento do jogo
    private AudioManager _audioManager;

    // Bordas da câmera
    public Vector3 _camBorders;

    // Variável que irá armazenar os números aleatorios
    private float _randomNumberVar = 0f;

    void Awake()
    {
        // Procura o script GameManager dentro do GameObject com a tag GameManager
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // Pega a posição relativa no mundo da borda da câmera e atribui
        _camBorders = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
    }

    void Update()
    {
        // Procura mais uma vez os GameObjects com a tag boss
        _bossSearch = GameObject.FindGameObjectsWithTag("Boss");

        // Verifica se o booleano bossTime é true e se a array bossSearch tem o tamanho igual a 0
        if (_gameManager.bossTime && _bossSearch.Length == 0 || _bossSearch == null){
            // Chama o método que spawna o boss
            SpawnBoss();
        }
    }

    /// <summary>
    /// Responsável por começar os timers de spawn
    /// </summary>
    public void StartSpawning()
    {
        StartCoroutine(SpawnThings());
    }

    /// <summary>
    /// Spawna o tubarão assim que o player perde e o z = 1 para evitar um colapso de sprites
    /// </summary>
    public void SpawnShark() {
        // Pega a posição do player
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        
        // A posição é fora da camera para a direita, na mesma altura que o player
        Vector3 pos = new Vector3(_camBorders[0] + 10, player.position.y, 1);

        Instantiate(_shark, pos, Quaternion.identity);
    }

    /// <summary>
    /// Retorna um número aleatório entre os parametros "min" e "max"
    /// </summary>
    /// <param name="min">Mínimo</param>
    /// <param name="max">Máximo</param>
    /// <returns>Retorna um float com o valor</returns>
    private float _randomNumber(float min, float max)
    {

        _randomNumberVar = Random.Range(min, max);

        return _randomNumberVar;
    }

    /// <summary>
    /// Define uma posição aleatória no eixo y
    /// </summary>
    /// <returns>Um valor entre -4 e 4</returns>
    private Vector2 _definePosition()
    {
        float i = _randomNumber(-6.5f, 6.5f);

        // Pega o primeiro valor do vetor de posições _camBorders, que representa as laterais da tela
        float _border = _camBorders[0] + 1;

        // Posição do peixe (x, y)
        Vector2 _position = new Vector2(_border, i);

        return _position;
    }

    /// <summary>
    /// Spawna o boss fora da camera para a direita e o z = 1 para evitar um colapso de sprites
    /// </summary>
    public void SpawnBoss(){
        Vector3 pos = new Vector3(_camBorders[0] + 10, 0, 1);

        Instantiate(_boss, pos, Quaternion.identity);
    }

    /// <summary>
    /// Cria/"spawna" um dos items entre a lista que será gerada, IEnumarator é como se chama os "timers"
    /// Exemplo: Gera um peixe ou um lixo
    /// </summary>
    private IEnumerator SpawnThings()
    {
        // Enquanto não for game over
        while (!_gameManager.gameOver && !_gameManager.bossTime)
        {
            // Um ID do proximo prefab que será gerado
            // 1 é um lixo, e 2 é um peixe
            int id = Random.Range(1, 3);

            // Posição do peixe (x, y)
            Vector2 pos = _definePosition();

            // Instancia os objetos
            if (id == 1)
            {
                Instantiate(_itemGameObject, pos, Quaternion.identity);
            }
            else
            {
                Instantiate(_fishGameObject, pos, Quaternion.identity);
            }

            // Espere 0,75 segundos antes de executar o timer novamente
            yield return new WaitForSeconds(_coroutineTimer);
        }
    }
}