using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System.Collections;

public class PostPrefab : MonoBehaviour
{
    [SerializeField] TMP_Text userName;
    [SerializeField] ImageResize profilePicture;

    [SerializeField] ImageResize postImage;
    [SerializeField] TMP_Text discription;
    [SerializeField] LikeFeature like;

    [SerializeField] Post post;

    public void SetPostData(Post post)
    {
        this.post = post;

        userName.text = post.UserProfile.UserName;
        discription.text = post.PostData.discription;
        like.SetData(post);

        profilePicture.ResizeImage(post.UserProfile.ProfilePicture);
        postImage.ResizeImage(post.PostData.post);
    }
}
