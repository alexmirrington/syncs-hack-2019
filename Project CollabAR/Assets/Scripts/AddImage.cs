using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddImage : MonoBehaviour {

	private bool loaded = true;

	void Start(){
		PickImage(512, gameObject);
		if(!loaded){
			transform.GetChild(0).GetComponent<Image>().color = Color.black;
		}
	}

	private void PickImage( int maxSize, GameObject imageObj)
	{
		NativeGallery.Permission permission = NativeGallery.GetImageFromGallery( ( path ) =>
		{
			Debug.Log( "Image path: " + path );
			if( path != null )
			{
				// Create Texture from selected image
				Texture2D test = NativeGallery.LoadImageAtPath( path, maxSize );
				if( test == null )
				{
					Debug.Log( "Couldn't load texture from " + path );
					loaded = false;
					return;
				}

				Sprite mySprite = Sprite.Create(test, new Rect(0.0f, 0.0f, test.width, test.height), new Vector2(0.5f, 0.5f), 100.0f);
				imageObj.transform.GetChild(0).GetComponent<Image>().sprite = mySprite;

			}
		}, "Select a PNG image", "image/png", maxSize );

		Debug.Log( "Permission result: " + permission );
	}
}
