using App.Helpers;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public static MusicManager Instance { get; private set; }
	[SerializeField] private SoundFunctions soundFunctions;
	[SerializeField] private AudioSource source;
	private float _initVolume;

	private void Awake()
	{
		Instance = this;
		_initVolume = source.volume;
	}

	public void SetVolume(bool on)
	{
		soundFunctions.ChangeVolume(source, on ? _initVolume : 0);
	}
}