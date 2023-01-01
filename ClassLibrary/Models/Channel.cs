using Google.Apis.YouTube.v3.Data;

namespace ClassLibrary.Models
{
    public class Channel
    {
        public string ChannelId { get; set; }
        public string? ChannelName { get; set; }
        public string? ChannelImage { get; set; }
        public Channel(Google.Apis.YouTube.v3.Data.Channel channel)
        {
            ChannelId = channel.Id;
            ChannelName = channel.Snippet.Title;
            ChannelImage = channel.Snippet.Thumbnails.High.Url;
        }
    }
}
