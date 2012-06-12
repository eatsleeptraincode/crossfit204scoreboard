﻿using System.Security.Principal;
using System.Threading;
using System.Web;
using CrossFit204ScoreBoard.Web.Models;

namespace CrossFit204ScoreBoard.Web.Security
{
    public class FubuPrincipal : IPrincipal
    {
        private readonly Athlete user;
        private readonly IIdentity identity;

        private FubuPrincipal(IIdentity identity)
        {
            this.identity = identity;
        }

        public FubuPrincipal(IIdentity identity, Athlete user)
            : this(identity)
        {
            this.user = user;
        }

        public static FubuPrincipal Current
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return HttpContext.Current.User as FubuPrincipal;
                }
                return Thread.CurrentPrincipal as FubuPrincipal;
            }
        }

        public bool IsInRole(string role) { return true; }

        public Athlete User { get { return user; } }

        public IIdentity Identity { get { return identity; } }

    }
}