function ReviewVM(dataIn) {
    var _this = {};

    _this.Applicants = ko.observableArray();

    _.each(dataIn.Applicants, function (applicant) {
        _this.Applicants.push(Applicant(applicant));
    });
    
    _this.AcceptedApplicants = ko.observableArray();

    _this.Accept = function (element) {
        var confirmation = confirm("Are you sure you would like to accept this application?\nYou cannot remove guests once you have accepted them.");
        if (confirmation == true) {
            $.post('/MyEvents/AcceptApplicant', { DinnerId: dataIn.Id, ApplicationId: element.Id() })
           .done(function (response) {
               element.AcceptedApplicants.push(element);
           });
        }
    };

    _this.Remove = function(element) {
        $.post('/MyEvents/HideApplicant', { DinnerId: dataIn.Id, ApplicationId: element.Id() })
            .done(function(response) {
                element.Visible(false);
            });
    };

    return _this;
}

function Applicant(dataIn) {
    var _this = ko.mapping.fromJS(dataIn);

    _this.Id = ko.observable(dataIn.Id);
    
    _this.ApplicantUrl = ko.observable('/Profile?id=' + _this.ApplicantId());

    _this.PartnerUrl = ko.observable('/Profile?id=' + _this.PartnerId());

    _this.Visible = ko.observable(true);

    return _this;
}