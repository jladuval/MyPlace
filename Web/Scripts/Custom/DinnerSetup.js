

var Job = function(name, guests, text, slots) {
    this.name = name;
    this.guests = ko.observableArray(guests);
    this.text = text;
    this.slots = slots;
};

var Guest = function (id, name, gender, host) {
    this.id = id;
    this.name = ko.observable(name);
    this.gender = gender;
    this.host = host;
};

var JobPlacementModel = function (jobs, guests) {
    var self = this;
    this.jobs = ko.observableArray(jobs);
    this.guests = ko.observableArray(guests);
    this.guests.id = "Guests";
    this.lastAction = ko.observable();
    this.lastError = ko.observable();
    this.maxGuests = 2;
    this.isJobFilled = function (parent) {
        return parent().length < self.maxGuests;
    };

    this.updateLastAction = function (arg) {
        self.lastAction("Moved " + arg.item.name() + " from " + arg.sourceParent.id + " (seat " + (arg.sourceIndex + 1) + ") to " + arg.targetParent.id + " (seat " + (arg.targetIndex + 1) + ")");
    };

    this.verifyAssignments = function (arg) {
        var gender, found,
            parent = arg.targetParent;

        if (parent.id !== "Available Students" && parent().length === 3 && parent.indexOf(arg.item) < 0) {
            gender = arg.item.gender;
            if (!ko.utils.arrayFirst(parent(), function (student) { return student.gender !== gender; })) {
                self.lastError("Cannot move " + arg.item.name() + " to " + arg.targetParent.id + " because there would be too many " + gender + " students");
                arg.cancelDrop = true;
            }
        }
    };
};

ko.bindingHandlers.flash = {
    init: function (element) {
        $(element).hide();
    },
    update: function (element, valueAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        if (value) {
            $(element).stop().hide().text(value).fadeIn(function () {
                clearTimeout($(element).data("timeout"));
                $(element).data("timeout", setTimeout(function () {
                    $(element).fadeOut();
                    valueAccessor()(null);
                }, 3000));
            });
        }
    },
    timeout: null
};


var guests = [
    new Guest(16, "Parker1", "male", true),
    new Guest(17, "Dennis1", "male"),
    new Guest(18, "Angel1", "female")
];

var jobs = [
    new Job("Starter", [], "The first course", 1),
    new Job("Main", [], "The first course", 2),
    new Job("Dessert", [], "The first course", 3),
];

var vm = new JobPlacementModel(jobs, guests);

ko.bindingHandlers.sortable.beforeMove = vm.verifyAssignments;
ko.bindingHandlers.sortable.afterMove = vm.updateLastAction;

ko.applyBindings(vm);