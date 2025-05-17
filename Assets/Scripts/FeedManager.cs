using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FeedManager : MonoBehaviour
{
    private static FeedManager instance;

    public static FeedManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<FeedManager>();

            return instance;
        }
    }

    [SerializeField] Feed feed;
    [SerializeField] PostPrefab postPrefab;
    [SerializeField] Transform content;

    class FeedData
    {
        public List<Post> feeds;
    }

    IEnumerator Start()
    {
        LoadFeed();

        foreach(Post item in feed.feeds)
        {
            Instantiate(postPrefab, content).SetPostData(item);
            yield return new WaitForSeconds(1f); // So that it won't stuck on first frame
        }
    }

    public void RefreshCanvas()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }

    public void SaveFeed()
    {
        string json = JsonUtility.ToJson(feed, true); // Pretty print
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "feed.json"), json);
    }

    public void LoadFeed()
    {
        string path = Path.Combine(Application.persistentDataPath, "feed.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            FeedData feedData = JsonUtility.FromJson<FeedData>(json);
            feed.feeds = feedData.feeds;
        }
    }
}