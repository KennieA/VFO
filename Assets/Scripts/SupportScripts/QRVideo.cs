using System;

public class QRVideo
{
    public int Id;
    public string Name;
    public string Description;
    public string Url;
    public int Count;
    public int UserGroupId;
    public int UserId;
    public DateTime ReleaseDate;


    public QRVideo(int id, string name, string description, string url, int count, int userGroupId, int userId, DateTime releaseDate)
    {
        Id = id;
        Name = name;
        Description = description;
        Url = url;
        Count = count;
        UserGroupId = userGroupId;
        UserId = userId;
        ReleaseDate = releaseDate;
    }

    public QRVideo(string name, string description, string url, int count, int userGroupId, int userId, DateTime releaseDate)
    {
        Name = name;
        Description = description;
        Url = url;
        Count = count;
        UserGroupId = userGroupId;
        UserId = userId;
        ReleaseDate = releaseDate;
    }
}