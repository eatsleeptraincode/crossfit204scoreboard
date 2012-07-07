﻿using System.Security.Principal;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Security
{
    [TestFixture]
    public class AdminPolicyTests : RavenContext<AdminPolicy>
    {
        private const string UserName = "Name";
        private const string UserName2 = "Name2";

        [Test]
        public void NullUserDenied()
        {
            var identity = MockFor<IIdentity>();
            identity.Stub(i => i.Name).Return(UserName);
            Services.Get<ISecurityContext>().Stub(c => c.CurrentIdentity).Return(identity);
            var right = ClassUnderTest.RightsFor(null);
            right.ShouldEqual(AuthorizationRight.Deny);
        }

        [Test]
        public void NotAdminUserDenied()
        {
            Session.Store(new Athlete{UserName = UserName, IsAdmin = false});
            Session.SaveChanges();
            var identity = MockFor<IIdentity>();
            identity.Stub(i => i.Name).Return(UserName);
            Services.Get<ISecurityContext>().Stub(c => c.CurrentIdentity).Return(identity);
            var right = ClassUnderTest.RightsFor(null);
            right.ShouldEqual(AuthorizationRight.Deny);
        }

        [Test]
        public void AdminUserAllowed()
        {
            Session.Store(new Athlete { UserName = UserName2, IsAdmin = true });
            Session.SaveChanges();
            var request = MockFor<IFubuRequest>();
            var identity = MockFor<IIdentity>();
            identity.Stub(i => i.Name).Return(UserName2);
            Services.Get<ISecurityContext>().Stub(c => c.CurrentIdentity).Return(identity);
            var right = ClassUnderTest.RightsFor(request);
            right.ShouldEqual(AuthorizationRight.Allow);
        }
    }
}