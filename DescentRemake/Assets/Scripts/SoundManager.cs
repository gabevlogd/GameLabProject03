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
	public AudioSource EffectsSource;
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
	public void Play(AudioClip clip)
	{
		EffectsSource.clip = clip;
		if (!EffectsSource.isPlaying) EffectsSource.Play();
	}

	/// <summary>
	/// Play a single clip through the music source.
	/// </summary>
	/// <param name="clip">AudioClip to play</param>
	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}

}
