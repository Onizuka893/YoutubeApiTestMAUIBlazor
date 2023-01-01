using ClassLibrary.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Channel = Google.Apis.YouTube.v3.Data.Channel;

namespace ClassLibrary.Services
{
    public class YoutubeApiCallerService
    {
        List<Models.Video> videoList = new();
        List<Playlist> playlistList;
        public List<Models.Video> GetVideos(string channelId)
        {
            int x = 0;
            try
            {
                Models.Video video;
                YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = YtApiKey.ApiKey });
                ChannelsResource.ListRequest channelsListRequest = yt.Channels.List("contentDetails");
                channelsListRequest.Id = channelId;
                ChannelListResponse channelsListResponse = channelsListRequest.Execute();
                foreach (Google.Apis.YouTube.v3.Data.Channel channel in channelsListResponse.Items)
                {
                    var channelurl = channel;
                    string uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                    string nextPageToken = "";
                    while (x < 5)
                    {
                        PlaylistItemsResource.ListRequest playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                        playlistItemsListRequest.PlaylistId = uploadsListId;
                        playlistItemsListRequest.MaxResults = 10;
                        playlistItemsListRequest.PageToken = nextPageToken;
                        PlaylistItemListResponse playlistItemsListResponse = playlistItemsListRequest.Execute();
                        foreach (PlaylistItem playlistItem in playlistItemsListResponse.Items)
                        {
                            video = new(playlistItem);
                            videoList.Add(video);
                        }
                        nextPageToken = playlistItemsListResponse.NextPageToken;
                        x++;
                    }
                }
                return videoList;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Playlist> GetPlayLists(string channelId)
        {
            playlistList = new List<Playlist>();
            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = YtApiKey.ApiKey });
            PlaylistsResource.ListRequest playlistRequest = yt.Playlists.List("snippet,contentDetails");
            playlistRequest.ChannelId = channelId;
            playlistRequest.MaxResults = 5;
            PlaylistListResponse playlistListResponse = playlistRequest.Execute();
            foreach (Playlist playlist in playlistListResponse.Items)
            {
                playlistList.Add(playlist);
            }
            return playlistList;
        }
        
        public List<Models.Video> GetPlaylistVideos(string playlistId)
        {
            List<Models.Video> playlistVideos = new();
            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = YtApiKey.ApiKey });
            PlaylistItemsResource.ListRequest playlistRequest = yt.PlaylistItems.List("snippet,contentDetails");
            playlistRequest.PlaylistId= playlistId;
            playlistRequest.MaxResults = 10;
            var playlistListResponse = playlistRequest.Execute();
            foreach (var video in playlistListResponse.Items)
            {
                playlistVideos.Add(new Models.Video(video));
            }
            return playlistVideos;
        }

        public Channel GetChannelInfo(string channelId)
        {
            YouTubeService yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = YtApiKey.ApiKey });
            ChannelsResource.ListRequest channelsListRequest = yt.Channels.List("snippet");
            channelsListRequest.Id = channelId;
            ChannelListResponse channelsListResponse = channelsListRequest.Execute();
            return channelsListResponse.Items.FirstOrDefault();
        }
    }
}
