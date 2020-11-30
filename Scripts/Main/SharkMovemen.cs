using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkMovemen : MonoBehaviour
{
    // Velocidade do boss
    [SerializeField]
    private float _speed = 8f;

    private Vector3 _camBorders;

    private UIManager _uiManager;

    private MainCharacter _mainCharacter;

    //Declarando a classe GameManager
    private GameManager _gameManager;

    //função será chamada quando iniciar o papel do script com algum gameObject
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("UI"))
        {
            _uiManager = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        }

        if (GameObject.FindGameObjectWithTag("GameManager"))
        {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        if (GameObject.FindGameObjectWithTag("Player"))
        {
            _mainCharacter = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacter>();
        }

        _camBorders = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
    }

    //função que vai ser chamada a cada frame

    void Update()
    {
        Movement();
    }

    /// <summary>
    /// Movimentação do tubarão
    /// </summary>
    private void Movement()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        
        if (transform.position.x < - _camBorders[0] - 10)
        {
            _uiManager.DisplayGameOver(_mainCharacter._score);
            _gameManager.deleteAll();
            Destroy(gameObject);
        }
            
    }
}
