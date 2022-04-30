using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance { get; private set; }

	[SerializeField] private AudioSource source;
	[SerializeField] private List<AudioClip> deadClips;
	[SerializeField] private List<AudioClip> shootClips;
	[SerializeField] private List<AudioClip> bloodClips;
	[SerializeField] private List<AudioClip> buttonClips;
	[SerializeField] private List<AudioClip> readyClips;
	[SerializeField] private List<AudioClip> bangClips;

	private void Awake()
	{
		Instance = this;
	}

	public void Play(SFXType type, float? volume = 1, float? delay = 0)
	{
		if (delay == 0)
			source.PlayOneShot(GetClip(type), volume ?? 1);
		else
			StartCoroutine(MyInvoke(delay ?? 1,
				() => { source.PlayOneShot(GetClip(type), volume ?? 1); }));
	}

	private IEnumerator MyInvoke(float delay, UnityAction action)
	{
		yield return new WaitForSeconds(delay);
		action?.Invoke();
	}

	private AudioClip GetClip(SFXType type)
	{
		var list = GetClips(type);
		return list[Random.Range(0, list.Count)];
	}

	private List<AudioClip> GetClips(SFXType type)
	{
		return type switch
		{
			SFXType.Dead => deadClips,
			SFXType.Shoot => shootClips,
			SFXType.Blood => bloodClips,
			SFXType.Button => buttonClips,
			SFXType.Ready => readyClips,
			SFXType.Bang => bangClips,
			_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
		};
	}
}