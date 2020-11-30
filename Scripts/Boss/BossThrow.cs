using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossThrow : MonoBehaviour
{
    private GameManager _gameManager;

    // Timer entre os loops
    [SerializeField]
    private float _coroutineTimer;

    [SerializeField]
    private GameObject _itemGameObject;

    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        StartCoroutine(SpawnThings());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnThings()
    {
        // Enquanto não for game over
        while (!_gameManager.gameOver && _gameManager.bossTime)
        {

            Vector2 pos = transform.position;

            // Instancia os objetos
            // Quaternion.Euler retorna uma rotação de (x, y, z)
            Instantiate(_itemGameObject, pos, Quaternion.Euler(0, 0, Random.Range(-10.0f, 10.0f)));

            // Espere 0,75 segundos antes de executar o timer novamente
            yield return new WaitForSeconds(_coroutineTimer);
        }
    }
}
