function ReviewVM(dataIn) {
    var _this = {};

    _this.Applicants = ko.observableArray();

    _.each(dataIn.Applicants, function (applicant) {
        _this.Applicants.push(Applicant(applicant));
    });

    return _this;
}

function Applicant(dataIn) {
    var _this = ko.mapping.fromJS(dataIn);

    _this.ApplicantUrl = ko.observable('/Profile?id=' + _this.ApplicantId());

    _this.PartnerUrl = ko.observable('/Profile?id=' + _this.PartnerId());

    _this.Visible = ko.observable(true);

    return _this;
}