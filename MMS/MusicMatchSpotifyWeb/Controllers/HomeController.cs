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
    public class HomeController : Base
    {
        const string clientId = "d2982c5e5488466bb1b10dc77648fe81";
        const string clientSecret = "597fe6cf0d604935928deca66d73c557";
        const string redirectUri = "http://localhost:26120/Home/Callback";

        // GET: /Home/
        public ActionResult Index()
        {
            Authentication.ClientId = clientId;
            Authentication.ClientSecret = clientSecret;
            Authentication.RedirectUri = redirectUri;

            StringBuilder strAuthorizePath = new StringBuilder();

            strAuthorizePath.Append("https://accounts.spotify.com/authorize/?");
            strAuthorizePath.Append("client_id=").Append(clientId);
            strAuthorizePath.Append("&response_type=code");
            strAuthorizePath.Append("&redirect_uri=").Append(redirectUri);
            strAuthorizePath.Append("&scope=").Append(string.Empty);

            return Redirect(strAuthorizePath.ToString());
        }

        public async Task<ActionResult> Callback()
        {
            if (AuthenticationToken.HasExpired)
                AuthenticationToken = await SpotifyWebAPI.Authentication.GetAccessToken(Request.QueryString["code"]);

            LoggedUser = await SpotifyWebAPI.User.GetCurrentUserProfile(AuthenticationToken);

            ViewData["LoggedUser"] = LoggedUser;

            var usersPlaylist = await SpotifyWebAPI.Playlist.GetUsersPlaylists(LoggedUser, AuthenticationToken);

            List<PlaylistViewModel> pvm = new List<PlaylistViewModel>();

            if (usersPlaylist.Items.Count() > 0)
            {
                usersPlaylist.Items.ForEach(w => pvm.Add(new PlaylistViewModel()
                {
                    Id = w.Id,
                    Description = w.Description,
                    Images = w.Images,
                    Name = w.Name,
                    Owner = w.Owner
                }));
            }
            return View(pvm);
        }

        public PartialViewResult Action()
        {
            return PartialView("~/Views/Shared/_MenuLogoHeader.cshtml", LoggedUser);
        }

        public async Task<ViewResult> Profile()
        { 
            ViewData["Following"] = await SpotifyWebAPI.User.GetFollowedArtists(AuthenticationToken);
            return View(LoggedUser);
        }
    }
}
