﻿namespace Accounts.Interfaces.Readers
{
    using System;
    using System.Collections.Generic;
    using Presentation.Comments;
    using Presentation.Dinner;

    public interface IDinnerReader
    {
        DinnerListDto GetDinnerList(double lat, double lng, int skip, int take);
        DinnerDto GetDinner(Guid id, Guid userId);
        DinnerConfirmDto DinnerCanBeConfirmedByPartner(string token);
        DinnerConfirmDto InvitationCanBeConfirmedByPartner(string token);
        ICollection<CommentDto> GetCommentsForDinner(Guid dinnerId);
        ICollection<PersonalDinnerListItem> GetAppliedDinnerList(Guid userId);
        ICollection<PersonalDinnerListItem> GetAttendedDinnerList(Guid userId);
        ICollection<PersonalDinnerListItem> GetHostedDinnerList(Guid userId);
        bool UserIsOwner(Guid userId, Guid dinnerId);
        ReviewApplicantsDto GetDinnerForReview(Guid id);
    }
}
