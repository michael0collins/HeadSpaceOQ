using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	void Awake() {
		if(s_instance == null) {
			DontDestroyOnLoad(this);
			s_instance = this as T;
		} else if(s_instance != this) {
			Destroy(gameObject);
		}
	}

	protected static T s_instance;

	public static T Instance  { get {
			// Check if the instance already exists in the scene
			if (s_instance == null)
				s_instance = (T)FindObjectOfType(typeof(T));

			return s_instance;
		}
	}
}