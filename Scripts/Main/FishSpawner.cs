using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    //TODO: Definir quando que os peixes serão 
    private Transform _transform;
    
    // Peixe prefab
    public GameObject fishGameObject;

    // Renderizador de sprite
    private SpriteRenderer _spriteRenderer;

    // Possiveis sprites para o peixe
    public Sprite[] fishSprite;

    // Bordas da câmera
    private Vector3 _camBorders;

    // Numeros aleatorios
    private int _randomNumberVar = 0;

    //função será chamada quando iniciar o papel do script com alguem gameObject

    void Start() {
        // Instanciando o sprite renderer do prefab do peixe
        _spriteRenderer = fishGameObject.GetComponent<SpriteRenderer>();
        _camBorders = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));
    }

    //função que vai ser chamada a cada frames

    void Update()
    {
        if(_randomNumber(0, 80) == 79){
            spawnFish();
        }
    }

    /// <summary>
    /// Retorna um número aleatório entre os parametros "min" e "max"
    /// </summary>
    /// <param name="min">Mínimo</param>
    /// <param name="max">Máximo</param>
    /// <returns>Retorna um integer com o valor</returns>
    private int _randomNumber(int min, int max)
    {

        _randomNumberVar = Random.Range(min, max);

        return _randomNumberVar;
    }

    /// <summary>
    /// Define uma posição aleatória no eixo y
    /// </summary>
    /// <returns>Um valor entre -4 e 4</returns>
    private int _definePosition()
    {
        int i = _randomNumber(-4,4);
        return i;
    }

    /// <summary>
    /// Pega um sprite aleatório
    /// </summary>
    private void _defineSprite()
    {
        int i = _randomNumber(0, fishSprite.Length);
       
        // Define o sprite selecionado
        _spriteRenderer.sprite = fishSprite[i];
    }

    /// <summary>
    /// Cria/"spawna" o peixe
    /// </summary>
    public void spawnFish()
    {
        int i = _definePosition();

        // Pega o segundo valor do vetor de posições _camBorders
        float _border = _camBorders[2];

        // Posição do peixe - (x, y)
        Vector2 _fishPosition = new Vector2(_border, i);

        _defineSprite();

        // Cria o peixe no jogo
        Instantiate(fishGameObject, _fishPosition, transform.rotation);
    }
}
