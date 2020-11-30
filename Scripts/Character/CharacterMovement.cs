using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    //variável para definir a velocidade
    [SerializeField]
    private float _velocity = 10f;

    private Transform _characterSprite;

    private Joystick _joystick;

    // GameManager
    private GameManager _gameManager;

    //variável do tipo Rigidbody2D para manipular o componente do gameObject
    private Rigidbody2D _rigidbody2D;

    private float _halfPlayerSizeX;

    //Animações
    private bool _isDown, _isTop, _isLeft, _isRight;

    //função será chamada quando iniciar o papel do script com alguem gameObject
    void Start()
    {
        //passando o valor do rigdbody2D para a variável
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _joystick = FindObjectOfType<Joystick>();

        _characterSprite = this.gameObject.transform.GetChild(0);

        _halfPlayerSizeX = _characterSprite.GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    //função que vai ser chamada a cada frame

    void Update()
    {
        if (!_gameManager.gameOver)
        {
            // Vertical - variável para definir qual direção o personagem está indo pelo eixo y
            // Horizontal - variável para definir qual direção o personagem está indo pelo eixo x
            float verticalMovement, horizontalMovement;

            if (SystemInfo.deviceType == DeviceType.Handheld) {
                verticalMovement = _joystick.Vertical;

                horizontalMovement = _joystick.Vertical;
            }
            else {
                // Verificar qual botão foi apertado, será definido por 1 (para cima) e -1 (para baixo)
                verticalMovement = Input.GetAxis("Vertical");

                // Verificar qual botão foi apertado, será definido por 1 (para cima) e -1 (para baixo)
                horizontalMovement = Input.GetAxis("Horizontal");
            }

            // Um vector para armazenar a direção do player
            Vector2 movement = new Vector2(horizontalMovement * _velocity, verticalMovement * _velocity);

            /*
            Passando os valores para o transform, onde irá dar "translate" ou mover, 
            e multiplicando pelo deltaTime - DeltaTime é o tempo entre frames, visto que caso a pessoa esteja rodando
            a 60 frames por segundo ela ficará muito veloz, multiplicar pelo tempo irá impedir isso
             */

            transform.Translate(movement * Time.deltaTime);

            /*if (verticalMovement > 0 && _characterSprite.gameObject.transform.eulerAngles.z <= 19) {
                Debug.Log(_characterSprite.gameObject.transform.eulerAngles.z);
                _characterSprite.gameObject.transform.Rotate(0f, 0f, (verticalMovement * Time.deltaTime) * 100, Space.Self);
            }
            else
            {
                if (verticalMovement < 0 && _characterSprite.gameObject.transform.eulerAngles.z >= -89)
                {
                    Debug.Log(_characterSprite.gameObject.transform.eulerAngles.z);
                    _characterSprite.gameObject.transform.Rotate(0f, 0f, (verticalMovement * Time.deltaTime) * 100, Space.Self);
                }
                else
                {
                    _characterSprite.gameObject.transform.Rotate(0f, 0f, 0, Space.Self);
                }
            }*/
            clampPlayerMovement();
        }

        void clampPlayerMovement()
        {
            Vector3 position = transform.position;

            float distance = transform.position.z - Camera.main.transform.position.z;

            float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + _halfPlayerSizeX;
            float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - _halfPlayerSizeX;
            float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).y + _halfPlayerSizeX;
            float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)).y - _halfPlayerSizeX;

            position.x = Mathf.Clamp(position.x, leftBorder, rightBorder);
            position.y = Mathf.Clamp(position.y, topBorder, bottomBorder);
            transform.position = position;
        }
    }
}
