using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Velocidade do boss
    [SerializeField]
    private float _speed = 3.5f;

    private Vector3 _camBorders;

    private Animation idleAnimation;

    //Declarando a classe mainCharacter
    private MainCharacter _mainCharacter;

    //Declarando a classe GameManager
    private GameManager _gameManager;

    //função será chamada quando iniciar o papel do script com algum gameObject
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //Instânciando a classe mainCharacter
        _mainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>();

        _camBorders = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
    }

    //função que vai ser chamada a cada frame

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        // Limita o boss à ficar 5 unidades da borda da tela
        if (transform.position.x >= _camBorders[0] - 5 && _gameManager.bossTime)
        {
            // Move o boss para a esquerda
            transform.Translate(Vector2.left * _speed * Time.deltaTime);
        }
        else
        {
            if (transform.position.x < _camBorders[0] + 4 && !_gameManager.bossTime)
            {
                    transform.Translate(Vector2.right * _speed * Time.deltaTime);
            }
            else
            {
                if (transform.position.x >= _camBorders[0] + 4)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    // Função de colisão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se o objeto tiver a tag "Player" - ou seja, o jogador -
        // tire pontos de prestígio 
        if (collision.gameObject.tag == "Player")
        {
            // Então tire um ponto de prestígio do player
            _mainCharacter.lostPrestige();

        }
    }
}
