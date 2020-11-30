using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFish : MonoBehaviour
{
    // Velocidade do peixe
    [SerializeField]
    private float _speed = 3.5f;

    private Vector3 _camBorders;

    //Declarando a classe mainCharacter
    private MainCharacter _mainCharacter;

    // Um array de sprites que serão utilizados
    [SerializeField]
    private Sprite[] _fishSprites;

    private GameManager _gameManager;

    //função será chamada quando iniciar o papel do script com algum gameObject

    void Start()
    {
        //Instânciando a classe mainCharacter
        _mainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>();

        _camBorders = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        gameObject.transform.Rotate(0f,180f,0f,Space.Self);

        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        RandomSprite();

    }

    //função que vai ser chamada a cada frame

    void Update()
    {
        Movement();
    }

    private void RandomSprite()
    {
        // Pega o sprite renderer e coloca um sprite aleatório do array de sprites
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _fishSprites[Random.Range(0, _fishSprites.Length)];
    }
    private void Movement()
    {
        // Move o peixe para a esquerda - Não se esqueça do time.deltatime
        transform.Translate(Vector2.right * _speed * Time.deltaTime);

        // Caso o peixe saia para fora destrua-o
        if (transform.position.x < - _camBorders[0] - 1)
        {
            Destroy(gameObject);
        }
    }

    // Função de colisão
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se o objeto tiver a tag "Player" - ou seja, o jogador -
        // tire pontos de prestígio 
        if (collision.gameObject.tag == "Player" && !_gameManager.gameOver)
        {
            // Então tire um ponto de prestígio do player
            _mainCharacter.lostPrestige();

            // E destrua o peixe
            Destroy(gameObject);
        }
    }
}
