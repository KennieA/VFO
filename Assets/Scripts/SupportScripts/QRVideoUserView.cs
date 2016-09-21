using System;

public class QRVideoUserView
{
    public int VideoId;
    public int UserId;
    public DateTime ViewDate;

    public QRVideoUserView(int videoId, int userId, DateTime viewDate)
    {
        VideoId = videoId;
        UserId = userId;
        ViewDate = viewDate;
    }
}
