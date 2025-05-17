using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LikeFeature : MonoBehaviour
{
    [SerializeField] Image likeImage;
    [SerializeField] Sprite likeSprite;
    [SerializeField] Sprite dislikeSprite;

    [SerializeField] TMP_Text likesText;

    [SerializeField] int totalLikes;
    [SerializeField] bool like;

    [SerializeField] Post post;

    public int TotalLikes
    {
        get => totalLikes;
        set
        {
            totalLikes = value;
            likesText.text = LikeRep(totalLikes);
        }
    }

    public bool Like
    {
        get => like;
        set
        {
            like = value;

            if (like)
            {
                likeImage.sprite = likeSprite;
                TotalLikes++;
            }
            else
            {
                likeImage.sprite = dislikeSprite;
                TotalLikes--;
            }

            post.PostData.like = like;
            post.PostData.likes = totalLikes;
            FeedManager.Instance.SaveFeed();
        }
    }

    public void SetData(Post post)
    {
        this.post = post;
        like = post.PostData.like;
        TotalLikes = post.PostData.likes;

        if (like)
        {
            likeImage.sprite = likeSprite;
        }
        else
        {
            likeImage.sprite = dislikeSprite;
        }
    }

    string LikeRep(int likes)
    {
        if (likes >= 100000)
            return (likes / 100000f).ToString("0.##") + "L"; // 1L = 100,000
        else if (likes >= 1000)
            return (likes / 1000f).ToString("0.##") + "K";
        else
            return likes.ToString();
    }

    public void OnClickLike()
    {
        Like = !Like;
    }
}
