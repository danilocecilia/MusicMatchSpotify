using SpotifyWebAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicMatchSpotifyWeb.Controllers
{
    public class Base : Controller
    {
        public AuthenticationToken AuthenticationToken
        {
            get
            {
                return new AuthenticationToken()
                {
                    AccessToken = Session["AccessToken"] != null ? (string)Session["AccessToken"] : string.Empty,
                    ExpiresOn = Session["ExpiresOn"] != null ? (DateTime)Session["ExpiresOn"] : DateTime.MinValue,
                    RefreshToken = Session["RefreshToken"] != null ? (string)Session["RefreshToken"] : string.Empty,
                    TokenType = Session["TokenType"] != null ? (string)Session["TokenType"] : string.Empty,
                };
            }
            set
            {
                Session["AccessToken"] = value.AccessToken;
                Session["ExpiresOn"] = value.ExpiresOn;
                Session["RefreshToken"] = value.RefreshToken;
                Session["TokenType"] = value.TokenType;
            }
        }

        private User _loggedUser;
        public User LoggedUser
        {
            get
            {
                if (Session["LoggedUser"] == null)
                {
                    Session["LoggedUser"] = _loggedUser;
                    return _loggedUser;
                }
                else return (User)Session["LoggedUser"];
            }
            set
            {
                _loggedUser = value;
            }
        }
    }
}