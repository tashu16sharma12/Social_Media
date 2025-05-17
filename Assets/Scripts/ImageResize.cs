using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageResize : MonoBehaviour
{
    [SerializeField] Image targetImage; // Assign this in the inspector

    public void ResizeImage(string texPath)
    {
        if (texPath == "" || targetImage == null)
        {
            Debug.Log("Texture or target Image is missing.");
            return;
        }

        StartCoroutine(LoadImageFromStreamingAssets(texPath));
    }

    private IEnumerator ResizeAfterFrame(Texture2D tex)
    {
        // Calculate aspect ratio
        float aspectRatio = (float)tex.height / tex.width;

        RectTransform rt = targetImage.GetComponent<RectTransform>();
        float currentWidth = rt.rect.width;

        // Fallback if width is still zero
        if (currentWidth == 0)
            currentWidth = rt.sizeDelta.x;

        float newHeight = currentWidth * aspectRatio;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, newHeight);
        yield return null; // Wait for UI layout to finish
        FeedManager.Instance.RefreshCanvas();
    }

    IEnumerator LoadImageFromStreamingAssets(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(filePath))
        {
            www.SendWebRequest();

            while(!www.isDone)
            {
                yield return null;
            }

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D tex = DownloadHandlerTexture.GetContent(www);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                targetImage.color = Color.white;
                targetImage.sprite = sprite;
                StartCoroutine(ResizeAfterFrame(tex));
            }
        }
    }
}