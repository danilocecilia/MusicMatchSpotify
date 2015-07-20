using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SpotifyWebAPI;
using MusicMatchSpotifyWeb.Controllers;
namespace MusicMatchSpotifyWeb.ViewModel
{
    public class PlaylistTracksViewModel
    {
        public Page<Playlist> Playlist { get; set; }

        public Page<PlaylistTrack> PlaylistTrack { get; set; }

        public Playlist CurrentPlaylist { get; set; }
    }
}