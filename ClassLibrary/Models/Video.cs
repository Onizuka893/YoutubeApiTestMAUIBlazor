using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class Video
    {
        public string? VideoId { get; set; }
        public string? VideoTitle { get; set; }
        public string? VideoTumbnail { get; set; }
        public string? VideoDescription { get; set; }
        public string? ChannelName { get; set; }
        public DateTime VideoPublishedAt { get; set; }

        public Video(PlaylistItem item)
        {
            VideoId = item.Snippet.ResourceId.VideoId;
            VideoTitle = item.Snippet.Title;
            VideoDescription = item.Snippet.Description;
            ChannelName = item.Snippet.ChannelTitle;
            if (item.Snippet.Thumbnails.Standard != null)
                VideoTumbnail = item.Snippet.Thumbnails.Standard.Url;
            else
                VideoTumbnail = item.Snippet.Thumbnails.High.Url;
            VideoPublishedAt = (DateTime)item.Snippet.PublishedAt;
        }
    }
}
