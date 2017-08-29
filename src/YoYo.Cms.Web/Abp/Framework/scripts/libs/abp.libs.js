var abp = abp || {};
(function () {

    if (!$.blockUI) {
        return;
    }

    $.extend($.blockUI.defaults, {
        message: ' ',
        css: { },
        overlayCSS: {
            backgroundColor: '#AAA',
            opacity: 0.3,
            cursor: 'wait'    
        }
    });
    
    abp.ui.block = function (elm) {
        if (!elm) {
            $.blockUI();
        } else {
            $(elm).block();
        }
    };

    abp.ui.unblock = function (elm) {
        if (!elm) {
            $.unblockUI();
        } else {
            $(elm).unblock();
        }
    };

})();
var abp = abp || {};
(function ($) {

    if (!$) {
        return;
    }

    /* JQUERY ENHANCEMENTS ***************************************************/

    // abp.ajax -> uses $.ajax ------------------------------------------------

    abp.ajax = function (userOptions) {
        userOptions = userOptions || {};

        var options = $.extend(true, {}, abp.ajax.defaultOpts, userOptions);
        options.success = undefined;
        options.error = undefined;

        return $.Deferred(function ($dfd) {
            $.ajax(options)
                .done(function (data, textStatus, jqXHR) {
                    if (data.__abp) {
                        abp.ajax.handleResponse(data, userOptions, $dfd, jqXHR);
                    } else {
                        $dfd.resolve(data);
                        userOptions.success && userOptions.success(data);
                    }
                }).fail(function (jqXHR) {
                    if (jqXHR.responseJSON && jqXHR.responseJSON.__abp) {
                        abp.ajax.handleResponse(jqXHR.responseJSON, userOptions, $dfd, jqXHR);
                    } else {
                        abp.ajax.handleNonAbpErrorResponse(jqXHR, userOptions, $dfd);
                    }
                });
        });
    };

    $.extend(abp.ajax, {
        defaultOpts: {
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json',
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            }
        },

        defaultError: {
            message: 'An error has occurred!',
            details: 'Error detail not sent by server.'
        },

        defaultError401: {
            message: 'You are not authenticated!',
            details: 'You should be authenticated (sign in) in order to perform this operation.'
        },

        defaultError403: {
            message: 'You are not authorized!',
            details: 'You are not allowed to perform this operation.'
        },

        defaultError404: {
            message: 'Resource not found!',
            details: 'The resource requested could not found on the server.'
        },

        logError: function (error) {
            abp.log.error(error);
        },

        showError: function (error) {
            if (error.details) {
                return abp.message.error(error.details, error.message);
            } else {
                return abp.message.error(error.message || abp.ajax.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            if (!targetUrl) {
                location.href = abp.appPath;
            } else {
                location.href = targetUrl;
            }
        },

        handleNonAbpErrorResponse: function (jqXHR, userOptions, $dfd) {
            if (userOptions.abpHandleError !== false) {
                switch (jqXHR.status) {
                    case 401:
                        abp.ajax.handleUnAuthorizedRequest(
                            abp.ajax.showError(abp.ajax.defaultError401),
                            abp.appPath
                        );
                        break;
                    case 403:
                        abp.ajax.showError(abp.ajax.defaultError403);
                        break;
                    case 404:
                        abp.ajax.showError(abp.ajax.defaultError404);
                        break;
                    default:
                        abp.ajax.showError(abp.ajax.defaultError);
                        break;
                }
            }

            $dfd.reject.apply(this, arguments);
            userOptions.error && userOptions.error.apply(this, arguments);
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    abp.ajax.handleTargetUrl(targetUrl);
                });
            } else {
                abp.ajax.handleTargetUrl(targetUrl);
            }
        },

        handleResponse: function (data, userOptions, $dfd, jqXHR) {
            if (data) {
                if (data.success === true) {
                    $dfd && $dfd.resolve(data.result, data, jqXHR);
                    userOptions.success && userOptions.success(data.result, data, jqXHR);

                    if (data.targetUrl) {
                        abp.ajax.handleTargetUrl(data.targetUrl);
                    }
                } else if (data.success === false) {
                    var messagePromise = null;

                    if (data.error) {
                        if (userOptions.abpHandleError !== false) {
                            messagePromise = abp.ajax.showError(data.error);
                        }
                    } else {
                        data.error = abp.ajax.defaultError;
                    }

                    abp.ajax.logError(data.error);

                    $dfd && $dfd.reject(data.error, jqXHR);
                    userOptions.error && userOptions.error(data.error, jqXHR);

                    if (jqXHR.status === 401 && userOptions.abpHandleError !== false) {
                        abp.ajax.handleUnAuthorizedRequest(messagePromise, data.targetUrl);
                    }
                } else { //not wrapped result
                    $dfd && $dfd.resolve(data, null, jqXHR);
                    userOptions.success && userOptions.success(data, null, jqXHR);
                }
            } else { //no data sent to back
                $dfd && $dfd.resolve(jqXHR);
                userOptions.success && userOptions.success(jqXHR);
            }
        },

        blockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //block whole page
                    abp.ui.setBusy();
                } else { //block an element
                    abp.ui.setBusy(options.blockUI);
                }
            }
        },

        unblockUI: function (options) {
            if (options.blockUI) {
                if (options.blockUI === true) { //unblock whole page
                    abp.ui.clearBusy();
                } else { //unblock an element
                    abp.ui.clearBusy(options.blockUI);
                }
            }
        },

        ajaxSendHandler: function (event, request, settings) {
            var token = abp.security.antiForgery.getToken();
            if (!token) {
                return;
            }

            if (!settings.headers || settings.headers[abp.security.antiForgery.tokenHeaderName] === undefined) {
                request.setRequestHeader(abp.security.antiForgery.tokenHeaderName, token);
            }
        }
    });

    $(document).ajaxSend(function (event, request, settings) {
        return abp.ajax.ajaxSendHandler(event, request, settings);
    });

    /* JQUERY PLUGIN ENHANCEMENTS ********************************************/

    /* jQuery Form Plugin 
     * http://www.malsup.com/jquery/form/
     */

    // abpAjaxForm -> uses ajaxForm ------------------------------------------

    if ($.fn.ajaxForm) {
        $.fn.abpAjaxForm = function (userOptions) {
            userOptions = userOptions || {};

            var options = $.extend({}, $.fn.abpAjaxForm.defaults, userOptions);

            options.beforeSubmit = function () {
                abp.ajax.blockUI(options);
                userOptions.beforeSubmit && userOptions.beforeSubmit.apply(this, arguments);
            };

            options.success = function (data) {
                abp.ajax.handleResponse(data, userOptions);
            };

            //TODO: Error?

            options.complete = function () {
                abp.ajax.unblockUI(options);
                userOptions.complete && userOptions.complete.apply(this, arguments);
            };

            return this.ajaxForm(options);
        };

        $.fn.abpAjaxForm.defaults = {
            method: 'POST'
        };
    }

    abp.event.on('abp.dynamicScriptsInitialized', function () {
        abp.ajax.defaultError.message = abp.localization.abpWeb('DefaultError');
        abp.ajax.defaultError.details = abp.localization.abpWeb('DefaultErrorDetail');
        abp.ajax.defaultError401.message = abp.localization.abpWeb('DefaultError401');
        abp.ajax.defaultError401.details = abp.localization.abpWeb('DefaultErrorDetail401');
        abp.ajax.defaultError403.message = abp.localization.abpWeb('DefaultError403');
        abp.ajax.defaultError403.details = abp.localization.abpWeb('DefaultErrorDetail403');
        abp.ajax.defaultError404.message = abp.localization.abpWeb('DefaultError404');
        abp.ajax.defaultError404.details = abp.localization.abpWeb('DefaultErrorDetail404');
    });

})(jQuery);
/* Adapter for jTable (http://jtable.org) to ASP.NET Boilerplate (http://aspnetboilerplate.com)
 * by Halil ibrahim Kalkan (http://halilibrahimkalkan.com).
 */
(function ($) {

    if (!$ || !$.hik || !$.hik.jtable) {
        return;
    }

    //Reference to base object members
    var base = {
        _create: $.hik.jtable.prototype._create
    };

    //Extending jTable to adapt ASP.NET Boilerplate
    $.extend(true, $.hik.jtable.prototype, {

        //Override _create function to change actions according to Abp system
        _create: function () {
            var self = this;
            base._create.apply(self, arguments);

            if (self.options.actions.listAction) {
                self._adaptListActionforAbp();
            }

            if (self.options.actions.createAction) {
                self._adaptCreateActionforAbp();
            }

            if (self.options.actions.updateAction) {
                self._adaptUpdateActionforAbp();
            }

            if (self.options.actions.deleteAction) {
                self._adaptDeleteActionforAbp();
            }
        },

        //LIST ACTION ADAPTER
        _adaptListActionforAbp: function () {
            var self = this;
            var originalListAction = self.options.actions.listAction;
            self.options.actions.listAction = function (postData, jtParams) {
                return $.Deferred(function ($dfd) {

                    var input = $.extend({}, postData, {
                        skipCount: jtParams.jtStartIndex,
                        maxResultCount: jtParams.jtPageSize,
                        sorting: jtParams.jtSorting
                    });

                    originalListAction.method(input)
                        .done(function (result) {
                            $dfd.resolve({
                                "Result": "OK",
                                "Records": result.items || result[originalListAction.recordsField],
                                "TotalRecordCount": result.totalCount,
                                originalResult: result
                            });
                        })
                        .fail(function (error) {
                            self._handlerForFailOnAbpRequest($dfd, error);
                        });
                });
            };
        },

        //CREATE ACTION ADAPTER
        _adaptCreateActionforAbp: function () {
            var self = this;
            var originalCreateAction = self.options.actions.createAction;
            self.options.actions.createAction = function (postData) {
                return $.Deferred(function ($dfd) {

                    var input = $.extend({}, postData);

                    originalCreateAction.method(input)
                        .done(function (result) {
                            $dfd.resolve({
                                "Result": "OK",
                                "Record": originalCreateAction.recordField ? result[originalCreateAction.recordField] : result,
                                originalResult: result
                            });
                        })
                        .fail(function (error) {
                            self._handlerForFailOnAbpRequest($dfd, error);
                        });
                });
            };
        },

        //UPDATE ACTION ADAPTER
        _adaptUpdateActionforAbp: function () {
            var self = this;
            var originalUpdateAction = self.options.actions.updateAction;
            self.options.actions.updateAction = function (postData) {
                return $.Deferred(function ($dfd) {

                    var input = $.extend({}, postData);

                    originalUpdateAction.method(input)
                        .done(function (result) {
                            var jtableResult = {
                                "Result": "OK",
                                originalResult: result
                            };

                            if (originalUpdateAction.returnsRecord) {
                                if (originalUpdateAction.recordField) {
                                    jtableResult.Record = result[originalUpdateAction.recordField];
                                } else {
                                    jtableResult.Record = result;
                                }
                            }

                            $dfd.resolve(jtableResult);
                        })
                        .fail(function (error) {
                            self._handlerForFailOnAbpRequest($dfd, error);
                        });
                });
            };
        },

        //DELETE ACTION ADAPTER
        _adaptDeleteActionforAbp: function () {
            var self = this;
            var originalDeleteAction = self.options.actions.deleteAction;
            self.options.actions.deleteAction = function (postData) {
                return $.Deferred(function ($dfd) {

                    var input = $.extend({}, postData);

                    originalDeleteAction.method(input)
                        .done(function (result) {
                            $dfd.resolve({
                                "Result": "OK",
                                originalResult: result
                            });
                        })
                        .fail(function (error) {
                            self._handlerForFailOnAbpRequest($dfd, error);
                        });
                });
            };
        },

        _handlerForFailOnAbpRequest: function ($dfd, error) {
            if (error && error.message) {
                $dfd.resolve({
                    Result: "ERROR",
                    Message: error.message
                });
            } else {
                $dfd.reject(error);
            }
        },

        //Disable showing error messages
        _showError: function (message) {
            //do nothing since Abp handles error messages!
        }

    });

    //Overriding some defaults
    $.extend(true, $.hik.jtable.prototype.options, {
        pageList: "minimal"
    });

})(jQuery);
var abp = abp || {};
(function () {
    if (!moment || !moment.tz) {
        return;
    }

    /* DEFAULTS *************************************************/

    abp.timing = abp.timing || {};

    /* FUNCTIONS **************************************************/

    abp.timing.convertToUserTimezone = function (date) {
        var momentDate = moment(date);
        var targetDate = momentDate.clone().tz(abp.timing.timeZoneInfo.iana.timeZoneId);
        return targetDate;
    };

})();
var abp = abp || {};
(function ($) {

    //Check if SignalR is defined
    if (!$ || !$.connection) {
        return;
    }

    //Create namespaces
    abp.signalr = abp.signalr || {};
    abp.signalr.hubs = abp.signalr.hubs || {};

    //Get the common hub
    abp.signalr.hubs.common = $.connection.abpCommonHub;

    var commonHub = abp.signalr.hubs.common;
    if (!commonHub) {
        return;
    }

    //Register to get notifications
    commonHub.client.getNotification = function (notification) {
        abp.event.trigger('abp.notifications.received', notification);
    };

    //Connect to the server
    abp.signalr.connect = function() {
        $.connection.hub.start().done(function () {
            abp.log.debug('Connected to SignalR server!'); //TODO: Remove log
            abp.event.trigger('abp.signalr.connected');
            commonHub.server.register().done(function () {
                abp.log.debug('Registered to the SignalR server!'); //TODO: Remove log
            });
        });
    };

    if (abp.signalr.autoConnect === undefined) {
        abp.signalr.autoConnect = true;
    }

    if (abp.signalr.autoConnect) {
        abp.signalr.connect();
    }

    //reconnect if hub disconnects
    $.connection.hub.disconnected(function () {
        if (!abp.signalr.autoConnect) {
            return;
        }

        setTimeout(function () {
            if ($.connection.hub.state === $.signalR.connectionState.disconnected) {
                $.connection.hub.start();
            }
        }, 5000);
    });

})(jQuery);
var abp = abp || {};
(function () {

    if (!$.fn.spin) {
        return;
    }

    abp.libs = abp.libs || {};

    abp.libs.spinjs = {

        spinner_config: {
            lines: 11,
            length: 0,
            width: 10,
            radius: 20,
            corners: 1.0,
            trail: 60,
            speed: 1.2
        },

        //Config for busy indicator in element's inner element that has '.abp-busy-indicator' class.
        spinner_config_inner_busy_indicator: {
            lines: 11,
            length: 0,
            width: 4,
            radius: 7,
            corners: 1.0,
            trail: 60,
            speed: 1.2
        }

    };

    abp.ui.setBusy = function (elm, optionsOrPromise) {
        optionsOrPromise = optionsOrPromise || {};
        if (optionsOrPromise.always || optionsOrPromise['finally']) { //Check if it's promise
            optionsOrPromise = {
                promise: optionsOrPromise
            };
        }

        var options = $.extend({}, optionsOrPromise);

        if (!elm) {
            if (options.blockUI != false) {
                abp.ui.block();
            }

            $('body').spin(abp.libs.spinjs.spinner_config);
        } else {
            var $elm = $(elm);
            var $busyIndicator = $elm.find('.abp-busy-indicator'); //TODO@Halil: What if  more than one element. What if there are nested elements?
            if ($busyIndicator.length) {
                $busyIndicator.spin(abp.libs.spinjs.spinner_config_inner_busy_indicator);
            } else {
                if (options.blockUI != false) {
                    abp.ui.block(elm);
                }

                $elm.spin(abp.libs.spinjs.spinner_config);
            }
        }

        if (options.promise) { //Supports Q and jQuery.Deferred
            if (options.promise.always) {
                options.promise.always(function () {
                    abp.ui.clearBusy(elm);
                });
            } else if (options.promise['finally']) {
                options.promise['finally'](function () {
                    abp.ui.clearBusy(elm);
                });
            }
        }
    };

    abp.ui.clearBusy = function (elm) {
        //TODO@Halil: Maybe better to do not call unblock if it's not blocked by setBusy
        if (!elm) {
            abp.ui.unblock();
            $('body').spin(false);
        } else {
            var $elm = $(elm);
            var $busyIndicator = $elm.find('.abp-busy-indicator');
            if ($busyIndicator.length) {
                $busyIndicator.spin(false);
            } else {
                abp.ui.unblock(elm);
                $elm.spin(false);
            }
        }
    };

})();
var abp = abp || {};
(function ($) {
    if (!sweetAlert || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    abp.libs = abp.libs || {};
    abp.libs.sweetAlert = {
        config: {
            'default': {

            },
            info: {
                type: 'info'
            },
            success: {
                type: 'success'
            },
            warn: {
                type: 'warning'
            },
            error: {
                type: 'error'
            },
            confirm: {
                type: 'warning',
                title: 'Are you sure?',
                showCancelButton: true,
                cancelButtonText: 'Cancel',
                confirmButtonColor: "#DD6B55",
                confirmButtonText: 'Yes'
            }
        }
    };

    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title) {
        if (!title) {
            title = message;
            message = undefined;
        }

        var opts = $.extend(
            {},
            abp.libs.sweetAlert.config['default'],
            abp.libs.sweetAlert.config[type],
            {
                title: title,
                text: message
            }
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts, function () {
                $dfd.resolve();
            });
        });
    };

    abp.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    abp.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    abp.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    abp.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    abp.message.confirm = function (message, titleOrCallback, callback) {
        var userOpts = {
            text: message
        };

        if ($.isFunction(titleOrCallback)) {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        var opts = $.extend(
            {},
            abp.libs.sweetAlert.config['default'],
            abp.libs.sweetAlert.config.confirm,
            userOpts
        );

        return $.Deferred(function ($dfd) {
            sweetAlert(opts, function (isConfirmed) {
                callback && callback(isConfirmed);
                $dfd.resolve(isConfirmed);
            });
        });
    };

    abp.event.on('abp.dynamicScriptsInitialized', function () {
        abp.libs.sweetAlert.config.confirm.title = abp.localization.abpWeb('AreYouSure');
        abp.libs.sweetAlert.config.confirm.cancelButtonText = abp.localization.abpWeb('Cancel');
        abp.libs.sweetAlert.config.confirm.confirmButtonText = abp.localization.abpWeb('Yes');
    });

})(jQuery);

var abp = abp || {};
(function () {

    if (!toastr) {
        return;
    }

    /* DEFAULTS *************************************************/

    toastr.options.positionClass = 'toast-bottom-right';

    /* NOTIFICATION *********************************************/

    var showNotification = function (type, message, title, options) {
        toastr[type](message, title, options);
    };

    abp.notify.success = function (message, title, options) {
        showNotification('success', message, title, options);
    };

    abp.notify.info = function (message, title, options) {
        showNotification('info', message, title, options);
    };

    abp.notify.warn = function (message, title, options) {
        showNotification('warning', message, title, options);
    };

    abp.notify.error = function (message, title, options) {
        showNotification('error', message, title, options);
    };

})();