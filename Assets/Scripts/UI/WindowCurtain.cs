using UnityEngine;

public class WindowCurtain : MonoBehaviour {
	public float fadeAlpha = 0.5f;
	public float fadeInDuration = 0.3f;
	
	private UISprite curtainSprite;
	
	void Awake () {
		//Window.anyShown += FadeIn;
		//Window.anyHidden += FadeOut;

		curtainSprite = GetComponent<UISprite>();
	}


	
	//void OnDestroy () {
	//	Window.anyShown -= FadeIn;
	//	Window.anyHidden -= FadeOut;
	//}
	
	public void FadeIn () {
		//curtainSprite.sprite.TweenAlpha (fadeAlpha, 0.3f);
		TweenAlpha.Begin(gameObject, fadeInDuration, fadeAlpha);
	}
	
	public void FadeOut () {
		//if (!Window.isAnyShow) 
	//		curtainSprite.sprite.TweenAlpha (0f, 0.15f);
		TweenAlpha.Begin (gameObject, 0f, 0.15f);

	}
}