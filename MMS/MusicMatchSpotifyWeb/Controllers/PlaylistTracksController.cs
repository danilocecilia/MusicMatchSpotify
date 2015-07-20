using MusicMatchSpotifyWeb.ViewModel;
using SpotifyWebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MusicMatchSpotifyWeb.Controllers
{
    public class PlaylistTracksController : Base
    {
        private int _offset = 100;

        //
        // GET: /PlaylistTracks/
        public async Task<ActionResult> Index(string playlistId, string ownerId)
        {
            Session["NextUrl"] = null;

            var playlistTracks = await SpotifyWebAPI.Playlist.GetPlaylistTracks(ownerId, playlistId, AuthenticationToken);
            var currentPlaylist = await SpotifyWebAPI.Playlist.GetPlaylist(ownerId, playlistId, AuthenticationToken);
            var usersPlaylist = await SpotifyWebAPI.Playlist.GetUsersPlaylists(LoggedUser.Id, AuthenticationToken);

            PlaylistTracksViewModel playlistTracksVM = new PlaylistTracksViewModel()
            {
                Playlist = usersPlaylist,
                PlaylistTrack = playlistTracks,
                CurrentPlaylist = currentPlaylist
            };
            ViewData["LoggedUser"] = LoggedUser;
            return View(playlistTracksVM);
        }

        public async Task<PartialViewResult> PlaylistTracks(string nextUrl)
        {
            Page<PlaylistTrack> playlistTracks = new Page<PlaylistTrack>();

            if (nextUrl != null)
            {
                if (Session["NextUrl"] == null)
                {
                    playlistTracks = await SpotifyWebAPI.Playlist.GetPlaylistTracks(nextUrl, AuthenticationToken);
                }
                else
                {
                    if (Session["NextUrl"].ToString() == "noRows") { return PartialView(playlistTracks); }
                    playlistTracks = await SpotifyWebAPI.Playlist.GetPlaylistTracks(Session["NextUrl"].ToString(), AuthenticationToken);

                }

                Session["NextUrl"] = playlistTracks.Next != null ? playlistTracks.Next : "noRows";
            }
            return PartialView(playlistTracks);
        }

    }
}
