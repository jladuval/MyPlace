function ViewDinnerVM(dinnerId) {
    var _this = {};

    _this.ApplyText = ko.observable('I\'d love to come');

    _this.HasPartner = ko.observable(false);

    _this.TogglePartnerText = ko.observable('+ 1');

    _this.PartnerEmail = ko.observable('');

    _this.ApplyError = ko.observable();

    _this.TogglePartner = function () {
        _this.ApplyError('');
        _this.HasPartner(!_this.HasPartner());
        if (_this.HasPartner()) {
            $('#partnerEmail').show('fast');
            _this.ApplyText('We\'d love to come');
            _this.TogglePartnerText('<');
        } else {
            $('#partnerEmail').hide('fast');
            _this.ApplyText('I\'d love to come');
            _this.TogglePartnerText('+ 1');
        }
    };

    _this.ApplyClick = function () {
        if (_this.HasPartner()) {
            if (!_this.PartnerEmail()) {
                _this.ApplyError('Please enter an email');
                return;
            }
        }
        _this.ApplyError('');
        var partnerData = _this.HasPartner() ? _this.PartnerEmail() : '';
        $.post('/Dinner/Apply', { id: dinnerId, partnerEmail: partnerData })
            .done(function () {
                $('#applyButtons').hide();
                $('#applied').show('fast');
            })
            .fail(function () {
                if (_this.HasPartner())
                    _this.ApplyError('Your friend has not yet signed up');
            });
    };

    return _this;
}
