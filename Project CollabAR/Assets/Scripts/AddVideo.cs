using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AddVideo : MonoBehaviour {

	private VideoPlayer videoPlayer;

	void Start(){
		videoPlayer = gameObject.transform.GetChild(0).GetComponent<UnityEngine.Video.VideoPlayer>();
		PickVideo();

	}

	private void PickVideo()
	{
		NativeGallery.Permission permission = NativeGallery.GetVideoFromGallery( ( path ) =>
		{
			Debug.Log( "Video path: " + path );
			if( path != null )
			{
				// Play the selected video
				playVideo("file://" + path );
			}
		}, "Select a video" );

		Debug.Log( "Permission result: " + permission );
	}

		void playVideo(string path){
			// Set the video to play. URL supports local absolute or relative paths.
			// Here, using absolute.
			videoPlayer.url = path;

			// Start playback. This means the VideoPlayer may have to prepare (reserve
			// resources, pre-load a few frames, etc.). To better control the delays
			// associated with this preparation one can use videoPlayer.Prepare() along with
			// its prepareCompleted event.
			videoPlayer.Play();
		}
}
