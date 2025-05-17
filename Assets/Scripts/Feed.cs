using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Feed", menuName = "Scriptable Objects/Feed")]
public class Feed : ScriptableObject
{
    public List<Post> feeds;
}

[Serializable]
public class Post
{
    public UserProfile UserProfile;
    public PostData PostData;
}

// UserData
[Serializable]
public class UserProfile
{
    public string UserName;
    public string ProfilePicture;
}

[Serializable]
public class PostData
{
    public string post;
    public string discription;
    public int likes;
    public bool like;
}
