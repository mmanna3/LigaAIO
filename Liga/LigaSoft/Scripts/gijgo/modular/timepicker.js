/*
 * Gijgo TimePicker v1.7.3
 * http://gijgo.com/timepicker
 *
 * Copyright 2014, 2017 gijgo.com
 * Released under the MIT license
 */
/* global window alert jQuery gj */
/**  */gj.timepicker = {
    plugins: {},
    messages: {
        'en-us': {
            am: 'AM',
            pm: 'PM'
        }
    }
};

gj.timepicker.config = {
    base: {

        /** The width of the timepicker.         */        width: undefined,

        modal: true,

        /** Specifies the format, which is used to format the value of the DatePicker displayed in the input.         */        format: 'HH:MM',

        /** The name of the UI library that is going to be in use.         */        uiLibrary: 'materialdesign',

        /** The initial timepicker value.         */        value: undefined,

        /** The language that needs to be in use.         */        locale: 'en-us',

        icons: {
            rightIcon: '<i class="gj-icon clock" />'
        },

        style: {
            modal: 'gj-modal',
            wrapper: 'gj-timepicker gj-timepicker-md gj-unselectable',
            input: 'gj-textbox-md',
            clock: 'gj-clock gj-clock-md'
        }
    },

    bootstrap: {
        style: {
            modal: 'modal',
            wrapper: 'gj-timepicker gj-timepicker-bootstrap gj-unselectable input-group',
            input: 'form-control',
            calendar: 'gj-calendar gj-calendar-bootstrap'
        },
        iconsLibrary: 'glyphicons',
        showOtherMonths: true
    },

    bootstrap4: {
        style: {
            modal: 'modal',
            wrapper: 'gj-timepicker gj-timepicker-bootstrap gj-unselectable input-group',
            input: 'form-control',
            calendar: 'gj-calendar gj-calendar-bootstrap'
        },
        showOtherMonths: true
    }
};

gj.timepicker.methods = {
    init: function (jsConfig) {
        gj.widget.prototype.init.call(this, jsConfig, 'timepicker');
        this.attr('data-timepicker', 'true');
        gj.timepicker.methods.initialize(this);
        return this;
    },

    initialize: function ($timepicker) {
        var data = $timepicker.data(), $calendar,
            $wrapper = $timepicker.parent('div[role="wrapper"]'),
            $rightIcon = data.uiLibrary !== 'materialdesign' && data.iconsLibrary === 'materialicons' ? $('<span class="input-group-addon">' + data.icons.rightIcon + '</span>') : $(data.icons.rightIcon);

        $rightIcon.attr('role', 'right-icon');
        if ($wrapper.length === 0) {
            $wrapper = $('<div role="wrapper" />').addClass(data.style.wrapper); // The css class needs to be added before the wrapping, otherwise doesn't work.
            $timepicker.wrap($wrapper);
        } else {
            $wrapper.addClass(data.style.wrapper);
        }
        $wrapper = $timepicker.parent('div[role="wrapper"]');

        data.width && $wrapper.css('width', data.width);

        $timepicker.val(data.value).addClass(data.style.input).attr('role', 'input');

        //data.fontSize && $timepicker.css('font-size', data.fontSize);

        $rightIcon.on('click', function (e) {
            var $clock = $('body').find('[role="clock"][guid="' + $timepicker.attr('data-guid') + '"]');
            if ($clock.is(':visible')) {
                gj.timepicker.methods.hide($timepicker);
            } else {
                gj.timepicker.methods.show($timepicker);
            }
        });

        $timepicker.on('blur', function () {
            $timepicker.timeout = setTimeout(function () {
                if (!$timepicker.mouseMove) {
                    gj.timepicker.methods.hide($timepicker);
                }
            }, 500);
        });

        $wrapper.append($rightIcon);

        $calendar = gj.timepicker.methods.createClock($timepicker);

        if (data.keyboardNavigation) {
            $timepicker.on('keydown', gj.timepicker.methods.createKeyDownHandler($timepicker, $calendar));
        }
    },

    createClock: function ($timepicker) {
        var date, data = $timepicker.data(),
            $clock = $('<div role="clock" />').addClass(data.style.clock).attr('guid', $timepicker.attr('data-guid')),
            $hour = $('<div role="hour" />'),
            $minute = $('<div role="minute" />'),
            $header = $('<div role="header" />'),
            $body = $('<div role="body" />'),
            $dial = $('<div role="dial"></div>'),
            $btnOk = $('<button class="gj-button-md">Ok</button>'),
            $btnCancel = $('<button class="gj-button-md">Cancel</button>'),
            $footer = $('<div role="footer" />');

        date = gj.core.parseDate(data.value, data.format, data.locale);
        if (!date || isNaN(date.getTime())) {
            date = new Date();
        } else {
            $timepicker.attr('hours', date.getHours());
        }
        
        $dial.on('mousedown', gj.timepicker.methods.mouseDownHandler($timepicker, $clock));
        $dial.on('mousemove', gj.timepicker.methods.mouseMoveHandler($timepicker, $clock));
        $dial.on('mouseup', gj.timepicker.methods.mouseUpHandler($timepicker, $clock));

        $hour.on('click', function () {
            gj.timepicker.methods.renderHours($timepicker, $clock);
        });
        $minute.on('click', function () {
            gj.timepicker.methods.renderMinutes($timepicker, $clock);
        });
        $header.append($hour).append(':').append($minute);
        $clock.append($header);

        $body.append($dial);
        $clock.append($body);

        $btnCancel.on('click', function () { $timepicker.hide(); });
        $footer.append($btnCancel);
        $btnOk.on('click', function () { $timepicker.hide(); });
        $footer.append($btnOk);
        $clock.append($footer);

        $clock.hide();

        $('body').append($clock);

        if (data.modal) {
            $clock.wrapAll('<div role="modal" class="' + data.style.modal + '"/>');
            gj.core.center($clock);
        }
        return $clock;
    },

    getPointerValue: function (x, y, mode) {
        var value, radius, size = 256,
            angle = Math.atan2(size / 2 - x, size / 2 - y) / Math.PI * 180;

        if (angle < 0) {
            angle = 360 + angle;
        }

        switch (mode) {
            case '12h': {
                value = 12 - Math.round(angle * 12 / 360);
                return value === 0 ? 12 : value;
            }
            case '24h': {
                radius = Math.sqrt(Math.pow(size / 2 - x, 2) + Math.pow(size / 2 - y, 2));
                value = 12 - Math.round(angle * 12 / 360);
                if (value === 0) {
                    value = 12;
                }
                if (radius < size / 2 - 32) {
                    value = value === 12 ? 0 : value + 12;
                }
                return value;
            }
            case 'minutes': {
                value = Math.round(60 - 60 * angle / 360);
                return value === 60 ? 0 : value;
            }
        }
    },

    updateArrow: function(e, $timepicker, $clock) {
        var mouseX, mouseY, rect, value;
        mouseX = $timepicker.mouseX(e);
        mouseY = $timepicker.mouseY(e);

        rect = e.target.getBoundingClientRect();
        if ($timepicker.dialMode == 'hours') {
            value = gj.timepicker.methods.getPointerValue(mouseX - rect.left, mouseY - rect.top, '24h');
            $clock.attr('hour', value);
        } else if ($timepicker.dialMode == 'minutes') {
            value = gj.timepicker.methods.getPointerValue(mouseX - rect.left, mouseY - rect.top, 'minutes');
            $clock.attr('minute', value);
        }

        if ($timepicker.dialMode == 'hours') {
            setTimeout(function () {
                gj.timepicker.methods.renderMinutes($timepicker, $clock);
            }, 1000);
        } else if ($timepicker.dialMode == 'minutes') {
            $timepicker.hide();
        }

        gj.timepicker.methods.select($timepicker, $clock);
    },

    select: function ($timepicker, $clock) {
        var $dial = $clock.find('[role="dial"]'),
            $arrow = $clock.find('[role="arrow"]'),
            hour = $clock.attr('hour'),
            minute = $clock.attr('minute');

        if ($timepicker.dialMode == 'hours' && (hour == 0 || hour > 12)) {
            $arrow.css('width', 'calc(50% - 52px)');
        } else {
            $arrow.css('width', 'calc(50% - 20px)');
        }

        if ($timepicker.dialMode == 'hours') {
            $arrow.css('transform', 'rotate(' + ((hour * 30) - 90).toString() + 'deg)');
        } else {
            $arrow.css('transform', 'rotate(' + ((minute * 6) - 90).toString() + 'deg)');
        }
        $arrow.show();

        gj.timepicker.methods.update($timepicker, $clock);
    },

    update: function ($timepicker, $clock) {
        var data = $timepicker.data(),
            hour = $clock.attr('hour') || 0,
            minute = $clock.attr('minute') || 0,
            date = new Date(0, 0, 0, hour, minute),
            value = gj.core.formatDate(date, data.format, data.locale);

        $clock.find('[role="header"] > [role="hour"]').text(gj.core.pad(hour));
        $clock.find('[role="header"] > [role="minute"]').text(gj.core.pad(minute));

        $timepicker.val(value);
        gj.timepicker.events.change($timepicker);
    },

    mouseDownHandler: function ($timepicker, $clock) {
        return function (e) {
            $timepicker.mouseMove = true;
        }
    },

    mouseMoveHandler: function ($timepicker, $clock) {
        return function (e) {
            if ($timepicker.mouseMove) {
                gj.timepicker.methods.updateArrow(e, $timepicker, $clock);
            }
        }
    },

    mouseUpHandler: function ($timepicker, $clock) {
        return function (e) {
            gj.timepicker.methods.updateArrow(e, $timepicker, $clock);
            $timepicker.mouseMove = false;
            $timepicker.focus();
            clearTimeout($timepicker.timeout);
        }
    },

    renderHours: function ($timepicker, $clock) {
        var $dial = $clock.find('[role="dial"]');

        clearTimeout($timepicker.timeout);
        $dial.empty();

        $dial.append('<div role="arrow" style="transform: rotate(-90deg); display: none;"><div class="c296"></div><div class="c297"></div></div>');

        $dial.append('<span role="hour" style="transform: translate(54px, -93.5307px);">1</span>');
        $dial.append('<span role="hour" style="transform: translate(93.5307px, -54px);">2</span>');
        $dial.append('<span role="hour" style="transform: translate(108px, 0px);">3</span>');
        $dial.append('<span role="hour" style="transform: translate(93.5307px, 54px);">4</span>');
        $dial.append('<span role="hour" style="transform: translate(54px, 93.5307px);">5</span>');
        $dial.append('<span role="hour" style="transform: translate(6.61309e-15px, 108px);">6</span>');
        $dial.append('<span role="hour" style="transform: translate(-54px, 93.5307px);">7</span>');
        $dial.append('<span role="hour" style="transform: translate(-93.5307px, 54px);">8</span>');
        $dial.append('<span role="hour" style="transform: translate(-108px, 1.32262e-14px);">9</span>');
        $dial.append('<span role="hour" style="transform: translate(-93.5307px, -54px);">10</span>');
        $dial.append('<span role="hour" style="transform: translate(-54px, -93.5307px);">11</span>');
        $dial.append('<span role="hour" style="transform: translate(-1.98393e-14px, -108px);">12</span>');
        $dial.append('<span role="hour" style="transform: translate(38px, -65.8179px);">13</span>');
        $dial.append('<span role="hour" style="transform: translate(65.8179px, -38px);">14</span>');
        $dial.append('<span role="hour" style="transform: translate(76px, 0px);">15</span>');
        $dial.append('<span role="hour" style="transform: translate(65.8179px, 38px);">16</span>');
        $dial.append('<span role="hour" style="transform: translate(38px, 65.8179px);">17</span>');
        $dial.append('<span role="hour" style="transform: translate(4.65366e-15px, 76px);">18</span>');
        $dial.append('<span role="hour" style="transform: translate(-38px, 65.8179px);">19</span>');
        $dial.append('<span role="hour" style="transform: translate(-65.8179px, 38px);">20</span>');
        $dial.append('<span role="hour" style="transform: translate(-76px, 9.30732e-15px);">21</span>');
        $dial.append('<span role="hour" style="transform: translate(-65.8179px, -38px);">22</span>');
        $dial.append('<span role="hour" style="transform: translate(-38px, -65.8179px);">23</span>');
        $dial.append('<span role="hour" style="transform: translate(-1.3961e-14px, -76px);">00</span>');

        $clock.find('[role="header"] [role="hour"]').addClass('selected');
        $clock.find('[role="header"] [role="minute"]').removeClass('selected');

        $timepicker.dialMode = 'hours';

        gj.timepicker.methods.select($timepicker, $clock);
    },

    renderMinutes: function ($timepicker, $clock) {
        var $dial = $clock.find('[role="dial"]');

        clearTimeout($timepicker.timeout);
        $dial.empty();

        $dial.append('<div role="arrow" style="transform: rotate(-90deg); display: none;"><div class="c296"></div><div class="c297"></div></div>');

        $dial.append('<span role="hour" style="transform: translate(54px, -93.5307px);">5</span>');
        $dial.append('<span role="hour" style="transform: translate(93.5307px, -54px);">10</span>');
        $dial.append('<span role="hour" style="transform: translate(108px, 0px);">15</span>');
        $dial.append('<span role="hour" style="transform: translate(93.5307px, 54px);">20</span>');
        $dial.append('<span role="hour" style="transform: translate(54px, 93.5307px);">25</span>');
        $dial.append('<span role="hour" style="transform: translate(6.61309e-15px, 108px);">30</span>');
        $dial.append('<span role="hour" style="transform: translate(-54px, 93.5307px);">35</span>');
        $dial.append('<span role="hour" style="transform: translate(-93.5307px, 54px);">40</span>');
        $dial.append('<span role="hour" style="transform: translate(-108px, 1.32262e-14px);">45</span>');
        $dial.append('<span role="hour" style="transform: translate(-93.5307px, -54px);">50</span>');
        $dial.append('<span role="hour" style="transform: translate(-54px, -93.5307px);">55</span>');
        $dial.append('<span role="hour" style="transform: translate(-1.98393e-14px, -108px);">60</span>');

        $clock.find('[role="header"] [role="hour"]').removeClass('selected');
        $clock.find('[role="header"] [role="minute"]').addClass('selected');

        $timepicker.dialMode = 'minutes';

        gj.timepicker.methods.select($timepicker, $clock);
    },

    show: function ($timepicker) {
        var data = $timepicker.data(),
            offset = $timepicker.offset(),
            $clock = $('body').find('[role="clock"][guid="' + $timepicker.attr('data-guid') + '"]');

        gj.timepicker.methods.renderHours($timepicker, $clock);
        if (!$timepicker.value()) {
            $timepicker.value(gj.core.formatDate(new Date(), data.format, data.locale));
        }
        $clock.show();
        $clock.closest('div[role="modal"]').show();
        if (data.modal) {
            gj.core.center($clock);
        } else {
            $clock.css('left', offset.left).css('top', offset.top + $timepicker.outerHeight(true) + 3);
        }
        $timepicker.focus();
        gj.timepicker.events.show($timepicker);
    },

    hide: function ($timepicker) {
        var $clock = $('body').find('[role="clock"][guid="' + $timepicker.attr('data-guid') + '"]');
        $clock.hide();
        $clock.closest('div[role="modal"]').hide();
        gj.timepicker.events.hide($timepicker);
    },

    value: function ($timepicker, value) {
        var $clock, time, data = $timepicker.data();
        if (typeof (value) === "undefined") {
            return $timepicker.val();
        } else {
            time = gj.core.parseDate(value, data.format, data.locale);
            if (time) {
                $clock = $('body').find('[role="clock"][guid="' + $timepicker.attr('data-guid') + '"]');
                $clock.attr('hour', time.getHours());
                $clock.attr('minute', time.getMinutes());
                gj.timepicker.methods.select($timepicker, $clock);
            } else {
                $timepicker.val('');
            }
            return $timepicker;
        }
    },

    destroy: function ($timepicker) {
        var data = $timepicker.data(),
            $parent = $timepicker.parent(),
            $clock = $('body').find('[role="clock"][guid="' + $timepicker.attr('data-guid') + '"]');
        if (data) {
            $timepicker.off();
            if ($clock.parent('[role="modal"]').length > 0) {
                $clock.unwrap();
            }
            $clock.remove();
            $timepicker.removeData();
            $timepicker.removeAttr('data-type').removeAttr('data-guid').removeAttr('data-timepicker');
            $timepicker.removeClass();
            $parent.children('[role="right-icon"]').remove();
            $timepicker.unwrap();
        }
        return $timepicker;
    }
};

gj.timepicker.events = {
    /**
     * Triggered when the timepicker value is changed.
     *     */    change: function ($timepicker) {
        return $timepicker.triggerHandler('change');
    },

    /**
     * Event fires when the timepicker is opened.     */    show: function ($timepicker) {
        return $timepicker.triggerHandler('show');
    },

    /**
     * Event fires when the timepicker is closed.     */    hide: function ($timepicker) {
        return $timepicker.triggerHandler('hide');
    }
};

gj.timepicker.widget = function ($element, jsConfig) {
    var self = this,
        methods = gj.timepicker.methods;

    self.mouseMove = false;
    self.dialMode = undefined;

    /** Gets or sets the value of the timepicker.     */    self.value = function (value) {
        return methods.value(this, value);
    };

    /** Remove timepicker functionality from the element.     */    self.destroy = function () {
        return methods.destroy(this);
    };

    /** Show the calendar.     */    self.show = function () {
        gj.timepicker.methods.show(this);
    };

    /** Hide the calendar.     */    self.hide = function () {
        gj.timepicker.methods.hide(this);
    };

    $.extend($element, self);
    if ('true' !== $element.attr('data-timepicker')) {
        methods.init.call($element, jsConfig);
    }

    return $element;
};

gj.timepicker.widget.prototype = new gj.widget();
gj.timepicker.widget.constructor = gj.timepicker.widget;

(function ($) {
    $.fn.timepicker = function (method) {
        var $widget;
        if (this && this.length) {
            if (typeof method === 'object' || !method) {
                return new gj.timepicker.widget(this, method);
            } else {
                $widget = new gj.timepicker.widget(this, null);
                if ($widget[method]) {
                    return $widget[method].apply(this, Array.prototype.slice.call(arguments, 1));
                } else {
                    throw 'Method ' + method + ' does not exist.';
                }
            }
        }
    };
})(jQuery);
