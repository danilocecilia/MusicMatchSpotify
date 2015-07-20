using MusicMatchSpotifyWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicMatchSpotifyWeb.ViewModel
{
    public class PlaylistViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<SpotifyWebAPI.Image> Images { get; set; }
        public SpotifyWebAPI.User Owner { get; set; }
    }
}