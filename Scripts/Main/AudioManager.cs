using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

	// Reprodutor da musica
	private AudioSource _sourceMusic;

	// Slider do volume da música
    [SerializeField]
	private Slider _sliderMusic;

	// Reprodutor de efeitos
	private AudioSource _sourceEffect;

	// Slider do volume dos efeitos
    [SerializeField]
	private Slider _sliderSound;

	[SerializeField]
	private AudioClip[] _track;

	[SerializeField]
	private AudioClip[] _effect;

	private bool _isFading;

	[SerializeField][Range(0.0f, 10.0f)]
	private float _fadeDuration;

	void Start () {

		// Procura os reprodutores de musica e de efeitos e pega o componente
		_sourceMusic = GameObject.FindWithTag("SourceMusic").GetComponent<AudioSource>();
		_sourceEffect = GameObject.FindWithTag("SourceEffect").GetComponent<AudioSource>();

	}

	void Update(){

		// Checa se existe um reprodutor de efeitos
		if(_sourceEffect != null && _sliderSound != null){
			// Define o valor do slider pro valor salvo no PlayerPrefs
			_sliderSound.value = PlayerPrefs.GetFloat("volumeEffect");
		}
		else{
			if(_sliderSound != null){
				// Define o valor do slider como 1 float
				_sliderSound.value = 1f;
			}
		}

		if(!_isFading){
			// Checa se existe um registro no PlayerPrefs relacionado ao volume da musica
			if(PlayerPrefs.HasKey("volumeMusic") == true){
				// Define o volume e o valor do slider pro valor salvo no PlayerPrefs
				_sourceMusic.volume = PlayerPrefs.GetFloat("volumeMusic");
				if(_sliderMusic != null){
					_sliderMusic.value = PlayerPrefs.GetFloat("volumeMusic");
				}
			}

			//Se não existir, define o volume e o slider para o máximo
			else{
				if(_sliderMusic != null){
					_sliderMusic.value = 1f;
				}
				_sourceMusic.volume = 1f;
			}
		}

		//Checando se existe uma preferencia de volume salva para os EFEITOS
		if(PlayerPrefs.HasKey("volumeEffect") == true){
			// Define o volume e o valor do slider pro valor salvo no PlayerPrefs
			_sourceEffect.volume = PlayerPrefs.GetFloat("volumeEffect");
			if(_sliderSound != null){
				_sliderSound.value = PlayerPrefs.GetFloat("volumeEffect");
			}
		}

		//Se não existir, define o volume e o slider para o máximo
		else{
			if(_sliderSound != null){
				_sliderSound.value = 1f;
			}
			_sourceEffect.volume = 1f;
		}

	}

	/// <summary>
	/// Função que define o valor do PlayerPrefs pro valor do slider e o
	/// volume da musica pra esse mesmo valor
	/// </summary>
	public void setMusic(){
		PlayerPrefs.SetFloat("volumeMusic", _sliderMusic.value);
			_sourceMusic.volume = PlayerPrefs.GetFloat("volumeMusic");
	}

	/// <summary>
	/// Função que define o valor do PlayerPrefs pro valor do slider e o
	/// volume dos efeitos pra esse mesmo valor
	/// </summary>
	public void setEffect(){
		PlayerPrefs.SetFloat("volumeEffect", _sliderSound.value);
			_sourceEffect.volume = PlayerPrefs.GetFloat("volumeEffect");
	}

	/// <summary>
	/// Fade out = Saida, ou seja, vai diminuindo o volume da musica
	/// </summary>
	/// <param name="nextClipIndex">Index do clip</param>
	/// <returns>Null</returns>
	public IEnumerator fadeOut(int nextClipIndex){
		_isFading = true;

		float initialVolume = _sourceMusic.volume;

		while (_sourceMusic.volume > 0)
		{
			_sourceMusic.volume -= initialVolume * Time.deltaTime / _fadeDuration;
			yield return null;
		}

		if(nextClipIndex == 1){
			BossMusic();
		}

		else if(nextClipIndex == 2){
			GameOverMusic();
		}

		else if(nextClipIndex == 0)
		{
			_sourceMusic.clip = _track[0];
			_sourceMusic.Play();
		}

		_isFading = false;
	}

	/// <summary>
	/// Música do boss
	/// </summary>
	public void BossMusic(){
		_sourceMusic.clip = _track[1];
		_sourceMusic.Play();
	}

	/// <summary>
	/// Música de game over
	/// </summary>
	public void GameOverMusic(){
		_sourceMusic.clip = _track[2];
		_sourceMusic.Play();
	}

	/// <summary>
	/// Efeito de pegar objeto
	/// </summary>
	public void CatchItem(){
		_sourceEffect.clip = _effect[0];
		_sourceEffect.time = .4f;
		_sourceEffect.Play();
	}

}