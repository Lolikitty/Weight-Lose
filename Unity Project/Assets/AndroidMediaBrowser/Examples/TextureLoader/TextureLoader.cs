using UnityEngine;
using AndroidMediaBrowser;

public class TextureLoader : MonoBehaviour
{
	void Start()
	{
		ImageBrowser.OnPicked += (image) =>
		{
			StartCoroutine(LoadTexture(image));
		};
		ImageBrowser.Pick();
	}
	
	private Texture2D _texture;
	private System.Collections.IEnumerator LoadTexture(Image image)
	{
		yield return StartCoroutine(image.LoadTexture());
		_texture = image.Texture;
	}
	
	void OnGUI()
	{
		if (_texture != null)
		{
			GUI.DrawTexture(
				new Rect(0, 0, Screen.width, Screen.height),
				_texture,
				ScaleMode.ScaleToFit,
				false);
		}
	}
}
