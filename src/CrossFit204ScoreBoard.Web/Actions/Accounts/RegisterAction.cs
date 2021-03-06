﻿using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Continuations;
using FubuValidation;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Accounts
{
    public class RegisterAction
    {
        readonly IDocumentSession session;
        private readonly IEncryptor encryptor;

        public RegisterAction(IDocumentSession session, IEncryptor encryptor)
        {
            this.session = session;
            this.encryptor = encryptor;
        }

        public RegisterViewModel Get(RegisterRequest request)
        {
            return new RegisterViewModel();
        }

        public FubuContinuation Post(RegisterViewModel request)
        {
            var athlete = request.Athlete;
            athlete.Password = encryptor.Encrypt(athlete.Password);
            session.Store(athlete);
            return FubuContinuation.RedirectTo(new AthleteListRequest());
        }
    }

    public class RegisterRequest {}

    public class RegisterViewModel
    {
        public Athlete Athlete { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}