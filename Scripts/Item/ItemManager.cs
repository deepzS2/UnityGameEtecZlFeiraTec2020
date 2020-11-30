using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Velocidade do item
    [SerializeField]
    private float _speed = 3.5f;

    //Declarando a classe mainCharacter
    private MainCharacter _mainCharacter;

    // Um Vector3 (x, y, z) para armazenar as bordas da camera
    private Vector3 _camBorders;

    // Um array de sprites que serão utilizados
    [SerializeField]
    private Sprite[] _itemsSprites;

    // Tamanho máximo para ser gerado
    [SerializeField]
    private float _maxSize = 1f;

    // Tamanho mínimo para ser gerado
    [SerializeField]
    private float _minSize = 0.25f;

    // Variável para armazenar "o quanto o objeto vale em pontos"
    private int _itemPoints = 1;

    // Audio manager
    private AudioManager _audioManager;

    private GameManager _gameManager;

    //função será chamada quando iniciar o papel do script com alguem gameObject
    void Start()
    {
        // Instânciando a classe mainCharacter se existir na cena
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            _mainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>();
        }

        _camBorders = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        if (GameObject.FindGameObjectWithTag("AudioManager"))
        {
            _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        }

        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        RandomSprite();
        RandomSize();
    }

    //função que vai ser chamada a cada frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // Move o item para a esquerda - Não se esqueça do time.deltatime
        transform.Translate(Vector2.left * _speed * Time.deltaTime);

        // Caso o item saia para fora destrua-o
        if (transform.position.x < - _camBorders[0] - 1)
        {
            Destroy(gameObject);
        }
        
    }

    /// <summary>
    /// Função responsável por gerar um sprite aleatório
    /// </summary>
    private void RandomSprite()
    {
        // Pega o sprite renderer e coloca um sprite aleatório do array de sprites
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _itemsSprites[Random.Range(0, _itemsSprites.Length - 1)];
    }

    /// <summary>
    /// Função responsável por gerar um tamanho aleatório do lixo
    /// </summary>
    private void RandomSize()
    {
        // Valores de x e y, entre o tamanho mínimo até o tamanho máximo
        // Para manter proporcionalidade eles terão o mesmo valor aleatório
        float size = Random.Range(_minSize, _maxSize);

        // Para setar o novo tamanho utilizamos new Vector2, ou seja é o mesmo que = new (x, y)
        transform.localScale = new Vector2(size, size);

        CalculateVelocityBasedOnSize(size);
        CalculatePointsBasedOnSize(size);
    }

    /// <summary>
    /// Calcula a velocidade do item baseado em seu tamanho
    /// </summary>
    /// <param name="size">O tamanho gerado em RandomSize()</param>
    private void CalculateVelocityBasedOnSize(float size)
    {
        // Metade da velocidade
        float halfSpeed = _speed / 2;

        // Lerp para pegar metade e 1/4 (25%) do tamanho minimo e maximo
        float halfSize = Mathf.Lerp(_minSize, _maxSize, 0.5f);
        float oneQuarterSize = Mathf.Lerp(_minSize, _maxSize, 0.25f);

        // Quanto menor for o item, acrescenta +1 ponto
        // Maior que a metade do tamanho 
        if (size > oneQuarterSize && size <= halfSize)
        {
            _speed += halfSpeed * 1;
        } 
        else if (size <= oneQuarterSize)
        {
            _speed += halfSpeed * 2;
        }
    }

    /// <summary>
    /// Calcula o quanto o objeto irá valer, em pontos, baseado em seu tamanho
    /// </summary>
    /// <param name="size">O tamanho gerado em RandomSize()</param>
    private void CalculatePointsBasedOnSize(float size)
    {
        // Lerp para pegar metade e 1/4 (25%) do tamanho minimo e maximo
        float halfSize = Mathf.Lerp(_minSize, _maxSize, 0.5f);
        float oneQuarterSize = Mathf.Lerp(_minSize, _maxSize, 0.25f);

        // Quanto menor for o item, acrescenta +1 ponto
        if (size > oneQuarterSize && size <= halfSize)
        {
            _itemPoints += 1;
        }
        else if (size <= oneQuarterSize)
        {
            _itemPoints += 2;
        }
    }

    // Função de colisão
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se o objeto tiver a tag "Player" - ou seja, o jogador -
        // aumente a pontuação do player
        if (collision.gameObject.tag == "Player" && !_gameManager.gameOver)
        {
            // Então aumente a pontuação do player
            _mainCharacter.appendScore(_itemPoints);

            _audioManager.CatchItem();

            // E destrua o item
            Destroy(this.gameObject);
        }
    }
}
