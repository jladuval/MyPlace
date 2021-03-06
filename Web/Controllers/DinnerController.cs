﻿namespace Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Web.Mvc;
    using Accounts.Interfaces.Commands.Applications;
    using Accounts.Interfaces.Commands.Dinner;
    using Accounts.Interfaces.Readers;
    using AutoMapper;
    using Base.CQRS.Commands;
    using Core.Extensions;
    using Mailing.Interfaces;
    using Models.Dinner;
    using Models.Shared;
    using Security.Interfaces.Queries;

    using Web.Attributes;

    [RequiresDetails]
    [Authorize]
    public class DinnerController : Controller
    {
        private readonly IGate _gate;

        private readonly IDinnerReader _dinnerReader;

        private readonly IProfileReader _profileReader;

        private readonly ISecurityUserReader _securityUserReader;

        public DinnerController(IGate gate, IDinnerReader dinnerReader, IProfileReader profileReader, ISecurityUserReader securityUserReader)
        {
            _gate = gate;
            _dinnerReader = dinnerReader;
            _profileReader = profileReader;
            _securityUserReader = securityUserReader;
        }

        public ActionResult Index(Guid id)
        {
            var model = Mapper.Map<ViewDinnerModel>(_dinnerReader.GetDinner(id, User.TryGetPrincipal().UserId));
            return View("ViewDinner", model);
        }

        [HttpPost]
        public ActionResult Apply(Guid id, string partnerEmail)
        {
            if(!string.IsNullOrEmpty(partnerEmail) && !_securityUserReader.UserExists(partnerEmail))
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);

            _gate.Dispatch(
                new ApplyForDinnerCommand(User.TryGetPrincipal().UserId, id)
                {
                    PartnerEmail = partnerEmail,
                    ConfirmUrl = Url.Action("ConfirmAttending", "Membership", null, Request.Url.Scheme)
                });

            if (partnerEmail != null)
            {
                _gate.Dispatch(new RunMailerCommand());
            }
            return new HttpStatusCodeResult(HttpStatusCode.Accepted);
        }

        [HttpPost]
        public ActionResult GetComments(string dinnerId)
        {
            var data = new List<CommentModel>();
            Guid id;
            if (Guid.TryParse(dinnerId, out id))
            {
                var comments = _dinnerReader.GetCommentsForDinner(id);
                data = Mapper.Map<List<CommentModel>>(comments);
            }
            return new JsonResult{ Data = data };
        }

        [HttpPost]
        public ActionResult SaveNewComment(string dinnerId, string text)
        {
            Guid teaId;
            if (Guid.TryParse(dinnerId, out teaId))
            {
                _gate.Dispatch(new AddCommentToDinnerCommand(teaId, text, User.TryGetPrincipal().UserId));
            }
            return new JsonResult { Data = "success" };
        }

        public ActionResult Create()
        {
            if (User.TryGetPrincipal().IsVerified)
            {
                return View(
                    "Create",
                    new CreateDinnerModel
                    {
                        CurrentLocation = _profileReader.GetLocationString(User.TryGetPrincipal().UserId)
                    });
            }
            
            TempData["ErrorMessage"] = "You have to verify your email before creating a dinner.";
            return RedirectToAction("Index", "DinnerList");
        }

        [HttpPost]
        public ActionResult Create(CreateDinnerModel model)
        {
            DateTime date;
            if (!DateTime.TryParseExact(model.Date, "MM/dd/yyyy HH:mm", null, DateTimeStyles.None, out date)) 
                ModelState.AddModelError("Date", "The date entered was invalid");

            if (model.PartnerEmail != null && !_securityUserReader.UserExists(model.PartnerEmail)) 
                ModelState.AddModelError("PartnerEmail", "This person does not have a registered account");

            if (ModelState.IsValid)
            {
                _gate.Dispatch(
                    new CreateDinnerCommand
                        {
                            UserId = User.TryGetPrincipal().UserId,
                            Starter = model.Starter,
                            Main = model.Main,
                            Dessert = model.Dessert,
                            Dry = model.DryDinner,
                            Description = model.Description,
                            Date = date,
                            PartnerEmail = model.PartnerEmail,
                            HostUrl = Url.Action("ConfirmHost", "Membership", null, Request.Url.Scheme)
                        });
                _gate.Dispatch(new RunMailerCommand());
                return RedirectToAction("Index", "DinnerList");
            }
            return View("Create", model);
        }
    }
}
