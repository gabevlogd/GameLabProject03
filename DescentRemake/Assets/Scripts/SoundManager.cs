/*
 * GameLab 2022/2023
 * first project: Hover's remake
 * current script's info: Searcher AI controller
 * author: Gabriele Garofalo
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource PlayerEffectsSource;
    public AudioSource EnemiesEffectsSource;
    public AudioSource WorldEffectsSource;
    public AudioSource MusicSource;
	public static SoundManager Instance = null;

	private void Awake()
	{
		if (Instance == null) Instance = this;
		else if (Instance != this) Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	/// <summary>
	/// Play a single clip through the sound effects source.
	/// </summary>
	/// <param name="clip">AudioClip to play</param>
	public void PlayPlayerSound(AudioClip clip)
	{
		Debug.Log("Playing: " + clip.name);
		PlayerEffectsSource.clip = clip;
		PlayerEffectsSource.Play();
	}

	public void PlayEnemiesSound(AudioClip clip)
	{
		Debug.Log("Playing: " + clip.name);
		EnemiesEffectsSource.clip = clip;
		EnemiesEffectsSource.Play();
	}

	public void PlayWorldSound(AudioClip clip)
	{
		Debug.Log("Playing: " + clip.name);
		WorldEffectsSource.clip = clip;
		WorldEffectsSource.Play();
	}

	/// <summary>
	/// Play a single clip through the music source.
	/// </summary>
	/// <param name="clip">AudioClip to play</param>
	public void PlayMusic(AudioClip clip)
	{
		Debug.Log("Playing: " + clip.name);
		MusicSource.clip = clip;
		MusicSource.Play();
	}

}
