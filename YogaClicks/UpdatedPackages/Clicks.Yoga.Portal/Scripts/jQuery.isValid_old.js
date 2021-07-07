(function ($) {
    "use strict";

    $.isValid = function (element, options) {

        var defaults = {
            general: {
                minLength: 1,
                maxLength: 500,
                showLengthError: true,
                errorDetails: [
                    {
                        id: "-general-length-error",
                        type: "length",
                        message: "Must be be at least 1 character and no more than x characters."
                    }
                ]
            },
            password: {
                minLength: 6,
                maxLength: 100,
                numbers: false,
                letters: true,
                showLengthError: true,
                showInvalidError: false,
                passwordconfirm: true,
                errorDetails: [
                    {
                        id: "-password-length-error",
                        type: "length",
                        message: "Password must be at least 6 characters",
                        show: true
                    },
                    {
                        id: "-password-invalid-error",
                        type: "invalid",
                        message: "Password must contain letters and numbers.",
                        show: false
                    }
                ]
            },
            passwordconfirm: {
                showInvalidError: true,
                errorDetails: [
                    {
                        id: "-passwordconfirm-invalid-error",
                        type: "invalid",
                        message: "Passwords do not match.",
                        show: true
                    }
                ]
            },
            email: {
                domain: '',
                showInvalidError: true,
                showDomainError: false,
                emailconfirm: true,
                errorDetails: [
                    {
                        id: "-email-invalid-error",
                        type: "invalid",
                        message: "Email is an invalid email address.",
                        show: true
                    },
                    {
                        id: "-email-domain-error",
                        type: "domain",
                        message: "Email domain entered is invalid, only @xxxx.xxx is accepted.",
                        show: false
                    }
                ]
            },
            emailconfirm: {
                showInvalidError: true,
                errorDetails: [
                    {
                        id: "-emailconfirm-invalid-error",
                        type: "invalid",
                        message: "Emails do not match.",
                        show: true
                    }
                ]
            },
            letters: {
                showInvalidError: true,
                errorDetails: [
                    {
                        id: "-letters-invalid-error",
                        type: "invalid",
                        message: "Field is letters characters only.",
                        show: true
                    }
                ]
            },
            numbers: {
                showInvalidError: true,
                errorDetails: [
                    {
                        id: "-numbers-invalid-error",
                        type: "invalid",
                        message: "Field is numbers only.",
                        show: true
                    }
                ]
            },
            decimals: {
                showInvalidError: true,
                errorDetails: [
                    {
                        id: "-decimals-invalid-error",
                        type: "invalid",
                        message: "Field is decimals only",
                        show: true
                    }
                ]
            },
            date: {
                format: "DD/MM/YYYY",
                allowFutureDates: true,
                showInvalidError: false,
                showFormatError: true,
                errorDetails: [
                    {
                        id: "-date-invalid-error",
                        type: "invalid",
                        message: "Please enter a valid Date. The date specified is in the future.",
                        show: false
                    },
                    {
                        id: "-date-format-error",
                        type: "format",
                        message: "Please enter Date format as DD/MM/YYYY as specified in the placeholder.",
                        show: true
                    }
                ]
            },
            postcode: {
                showInvalidError: true,
                errorDetails: [
                    {
                        id: "-postcode-invalid-error",
                        type: "invalid",
                        message: "Not a valid Post Code.",
                        show: true
                    }
                ]
            },
            mobile: {
                showInvalidError: true,
                numberLength: 11,
                errorDetails: [
                    {
                        id: "-mobile-invalid-error",
                        type: "invalid",
                        message: "Invalid mobile number.",
                        show: true
                    }
                ]
            },
            checkbox: {
                showInvalidError: true,
                errorDetails: [
                    {
                        id: "-checkbox-invalid-error",
                        type: "invalid",
                        message: "Checkbox must be checked.",
                        show: true
                    }
                ]
            },
            select: {
                showInvalidError: true,
                errorDetails: [
                    {
                        id: "-select-invalid-error",
                        type: "invalid",
                        message: "Please choose an item from the dropdown.",
                        show: true
                    }
                ]
            },
            submitButton: null,
            turnOffErrors: false,
            onFormValidated: function () { },
            onFormInValidated: function () { }
        },
            self;

        this.init = function () {

            self = this;

            this.elem = element;
            this.$elem = $(element);
            this.isFieldValid = false;
            this.options = $.extend(true, {}, $.isValid.defaults, options);
            this.formID = '#' + this.$elem.attr('id');
            this.formIDStr = this.$elem.attr('id');
            this.$elem.addClass('isValid');

            createErrorIds();
            showErrors();

            this.formArray = $(this.formID + ' :input[type="text"],' +
                this.formID + ' :input[type="email"],' +
                this.formID + ' :input[type="password"],' +
                this.formID + ' :input[type="tel"],' +
                this.formID + ' :input[type="number"],' +
                this.formID + ' :input[type="date"],' +
                this.formID + ' :input[type="checkbox"],' +
                this.formID + ' textarea,' +
                this.formID + ' select');
        };

         this.isFormValidated = function () {
            return ($(this.formID + ' .invalid').length) ? false : true;
         };


         var showErrors = function () {

             if (self.options.turnOffErrors) {

                 for (var option in self.options) {

                     for (var property in self.options[option]) {

                         if (self.options[option].hasOwnProperty(property) && /show/.test(property)) {
                             self.options[option][property] = false;
                         }
                     }
                 }
             }
         };

         var createErrorIds = function () {

             for (var option in self.options) {

                 if (self.options[option].hasOwnProperty("errorDetails")) {

                     self.options[option].errorDetails.forEach(function (errorProperty, index) {

                         for (var property in errorProperty) {

                             if (property === "id") {
                                 errorProperty[property] = self.formIDStr + errorProperty[property];
                             }
                         }
                     });
                 }
             }
         };

        this.isValidField = function (field) {

            if (field.disabled || $(field).attr('data-field-type') === "notrequired") {
                this.isFieldValid = true;
            } else {
                var data = $(field).attr('data-field-type'),
                    valMethodName;

                switch (data) {
                    case 'general':
                        valMethodName = 'isGeneralValid';
                        break;

                    case 'password':
                        valMethodName = 'isPasswordValid';
                        break;

                    case 'passwordConfirm':
                        valMethodName = 'isPasswordConfirmValid';
                        break;

                    case 'email':
                        valMethodName = 'isEmailValid';
                        break;

                    case 'emailConfirm':
                        valMethodName = 'isEmailConfirmValid';
                        break;

                    case 'date':
                        valMethodName = 'isDateValid';
                        break;

                    case 'letters':
                        valMethodName = 'isLetters';
                        break;

                    case 'numbers':
                        valMethodName = 'isNumbers';
                        break;

                    case 'decimals':
                        valMethodName = 'isDecimals';
                        break;

                    case 'postCode':
                        valMethodName = 'isPostCodeValid';
                        break;

                    case 'checkbox':
                        valMethodName = 'isCheckboxTicked';
                        break;

                    case 'select':
                        valMethodName = 'isSelectChosen';
                        break;

                    default:
                        valMethodName = 'isEmpty';
                        break;
                }

                this.isFieldValid = this[valMethodName](field);
            }
            
            return this.isFieldValid;
        };

        this.isEmpty = function (field) {

            if (!$(field).isNull) {
                if ($.trim($(field).val()) == '') {
                    return true;
                }
                return ($(field).val().length === 0);
            } else {
                return true;
            }

        };

        this.isWhiteSpace = function (field) {

        };

        this.isGeneralValid = function (field) {

            var isEmpty = this.isEmpty(field);

            if (isEmpty) {
                this.options.general.activeErrorMessage = getErrorMessage(field, 'required');
            }
            
            return !isEmpty;
        };

        this.isLetters = function (field) {

            var isEmpty = this.isEmpty(field),
                validResult = /^[A-Za-z ]+$/.test($(field).val());

            if (!isEmpty) {
                this.options.letters.activeErrorMessage = validResult ? '' : getErrorMessage(field, 'invalid');
                return validResult;
            } else {
                this.options.letters.activeErrorMessage = getErrorMessage(field, 'required');
                return !isEmpty;
            }
        };

        this.isNumbers = function (field) {

            var isEmpty = this.isEmpty(field),
                validResult = /^[0-9 ]+$/.test($(field).val());

            if (!isEmpty) {
                this.options.numbers.activeErrorMessage = validResult ? '' : getErrorMessage(field, 'invalid');
                return validResult;
            } else {
                this.options.numbers.activeErrorMessage = getErrorMessage(field, 'required');
                return !isEmpty;
            }
        };

        this.isDecimals = function (field) {

            var isEmpty = this.isEmpty(field),
                validResult = /^(\d+\.?\d*|\.\d+)$/.test($(field).val());

            if (!isEmpty) {
                this.options.decimals.activeErrorMessage = validResult ? '' : getErrorMessage(field, 'invalid');
                return validResult;
            } else {
                this.options.decimals.activeErrorMessage = getErrorMessage(field, 'required');
                return !isEmpty;
            }
        };

        this.isPasswordValid = function (field) {

            var passwordMatcher = /^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$/,
                isEmpty,
                lengthResult,
                formatResult;

            isEmpty = this.isEmpty(field);

            lengthResult = isBetween($(field).val().length, this.options.password.minLength, this.options.password.maxLength);

            if (this.options.password.numbers && this.options.password.letters) {

                formatResult = (passwordMatcher.test($(field).val()));

                if (!isEmpty) {
                    this.options.password.activeErrorMessage = lengthResult && !formatResult ? getErrorMessage(field, 'format') : getErrorMessage(field, 'invalid');
                    return (lengthResult) ? formatResult : false;
                } else {
                    this.options.password.activeErrorMessage = getErrorMessage(field, 'required');
                    return !isEmpty;
                }
            }

            this.isPasswordConfirmValid($(this.formID + ' input[data-field-type="passwordConfirm"]'));

            if (!isEmpty) {
                this.options.password.activeErrorMessage = lengthResult ? '' : getErrorMessage(field, 'invalid');
                return lengthResult;
            } else {
                this.options.password.activeErrorMessage = getErrorMessage(field, 'required');
                return !isEmpty;
            }
        };

        this.isPasswordConfirmValid = function (field) {

            if (this.options.password.passwordConfirm) {

                var isEmpty = this.isEmpty(field),
                    validResult = $(field).val() === $(this.formID + ' input[data-field-type="password"]').val();

                if (!isEmpty) {
                    this.options.passwordConfirm.activeErrorMessage = validResult ? '' : getErrorMessage(field, 'invalid');
                    return validResult;
                } else {
                    this.options.passwordConfirm.activeErrorMessage = getErrorMessage(field, 'required');
                    return !isEmpty;
                }
            }
        };

        this.isEmailValid = function (field) {

            var emailMatcher = /[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/,
                isEmpty,
                validResult,
                domainResult;

            isEmpty = this.isEmpty(field);

            // Remove whitespace as some phones put a spacebar in if you use the autocomplete option on the phone
            var isWhiteSpace = /\s/.test($(field).val());
            if (isWhiteSpace) {
                $(field).val($(field).val().replace(/\s/g, ''));
            }

            validResult = emailMatcher.test($(field).val());

            if (this.options.email.domain !== '') {

                domainResult = ($(field).val().indexOf(this.options.email.domain, $(field).val().length - this.options.email.domain.length) !== -1);

                if (!isEmpty) {
                    this.options.email.activeErrorMessage = validResult && !domainResult ? getErrorMessage(field, 'domain') : getErrorMessage(field, 'invalid');
                    return (validResult) ? domainResult : false;
                } else {
                    this.options.email.activeErrorMessage = getErrorMessage(field, 'required');
                    return !isEmpty;
                }
            }

            this.isEmailConfirmValid($(this.formID + ' input[data-field-type="emailConfirm"]'));

            if (!isEmpty) {
                this.options.email.activeErrorMessage = validResult ? '' : getErrorMessage(field, 'invalid');
                return validResult;
            } else {
                this.options.email.activeErrorMessage = getErrorMessage(field, 'required');
                return !isEmpty;
            }
        };

        this.isEmailConfirmValid = function (field) {

            if (this.options.email.emailConfirm) {

                var isEmpty = this.isEmpty(field),
                    validResult = $(field).val() === $(this.formID + ' input[data-field-type="email"]').val();

                if (!isEmpty) {
                    this.options.emailConfirm.activeErrorMessage = validResult ? '' : getErrorMessage(field, 'invalid');
                    return validResult;
                } else {
                    this.options.emailConfirm.activeErrorMessage = getErrorMessage(field, 'required');
                    return !isEmpty;
                }
            }
        };

        this.isDateValid = function (field) {

            var date = $(field).val(),
                isEmpty,
                validResult,
                allowedResult,
                formatResult;

            isEmpty = this.isEmpty(field);

            var momentObject = new moment(date, this.options.date.format, true); // jshint ignore:line

            validResult = momentObject.isValid();
            formatResult = (momentObject._pf.charsLeftOver === 0);

            if (!this.options.date.allowFutureDates) {

                if (validResult) {
                    allowedResult = isBetween(Date.parse(momentObject._d), 0, Date.now());
                }
            }

            if (!isEmpty) {

                if (!validResult) {

                    if (!formatResult) {

                        if (momentObject._pf.unusedTokens.length === 1 || momentObject._pf.unusedTokens.length === 2 && momentObject._pf.charsLeftOver === 2) {
                            this.options.date.activeErrorMessage = getErrorMessage(field, 'format');
                        } else {
                            this.options.date.activeErrorMessage = getErrorMessage(field, 'invalid');
                        }

                    } else {
                        this.options.date.activeErrorMessage = getErrorMessage(field, 'invalid');
                    }
                } else {
                    if (!formatResult) {
                        this.options.date.activeErrorMessage = getErrorMessage(field, 'format');
                    } else {
                        if (!allowedResult) {
                            this.options.date.activeErrorMessage = getErrorMessage(field, 'allowedDate');
                        }
                    }
                }

                return validResult && formatResult && allowedResult;
            } else {
                this.options.date.activeErrorMessage = getErrorMessage(field, 'required');
                return !isEmpty;
            }
        };

        this.isPostCodeValid = function (field) {

            var isEmpty = this.isEmpty(field),
                validResult = checkPostCode($(field).val());

            if (!isEmpty) {
                this.options.postCode.activeErrorMessage = validResult ? '' : getErrorMessage(field, 'invalid');
                return validResult;
            } else {
                this.options.postCode.activeErrorMessage = getErrorMessage(field, 'required');
                return !isEmpty;
            }
        };

        this.isSelectChosen = function (field) {

            var validResult = true;

            if ($(field).val() === null) {
                validResult = false;
            } else if ($(field).val().length === 0) {
                validResult = false;
            }

            this.options.select.activeErrorMessage = validResult ? '' : getErrorMessage(field, 'required');

            return validResult;
        };

        this.isCheckboxTicked = function (field) {

            var validResult = $(field).is(':checked');

            this.options.checkbox.activeErrorMessage = validResult ? '' : getErrorMessage(field, 'required');

            return validResult;
        };
        
        this.showErrorFor = function (field) {
            
            if(this.options.showErrorMessages) {
                if ($(field).parent().find('.form-error').length) {
                    updateErrorContainer(field);

                } else {
                    var errorContainer = createErrorContainer(field);
                    var errorContainerWidth = getErrorContainerWidth(field);

                    $(field).parent().append(errorContainer);

                    //$(field).siblings('.form-error').css('min-height', '40px');
                    //$(field).siblings('.form-error').css('color', '#000000');
                }
            }
            
            $(field).addClass('invalid');
            $(field).trigger('isValid.fieldInvalidated');
        };

        this.hideErrorFor = function (field) {

            $(field).removeClass('invalid');
            $(field).siblings('.form-error').remove();
            
            $(field).trigger('isValid.fieldValidated');
        };

        var isBetween = function (val, min, max) {
            return (val >= min && val <= max);
        };

        var createErrorContainer = function (field) {

            try {
                var errorMessage = self.options[$(field).data().fieldType].activeErrorMessage;
                return '<div class="form-error">' + errorMessage + '</div>';
                
            } catch (e) {
                return '';
            } 

        };

        var updateErrorContainer = function (field) {

            var errorMessage = self.options[$(field).data().fieldType].activeErrorMessage;

            $(field).siblings('.form-error').text(errorMessage);
        };

        var getErrorContainerWidth = function (field) {

            if ($(field).attr('aria-hidden') === undefined && $(field).is(':visible')) {
                return $(field).outerWidth();
            } else {
                return Math.max.apply(Math, $(field).parent().children().map(function () {
                    return $(this).width();
                }).get());
            }
        };

        var getErrorMessage = function (field, errorType) {

            var fieldData = $(field).data(),
                errorMessageType;

            switch (errorType) {

                case 'required':
                    errorMessageType = 'requiredErrorMessage';
                    break;

                case 'invalid':
                    errorMessageType = 'invalidErrorMessage';
                    break;
                    
                case 'format':
                    errorMessageType = 'formatErrorMessage';
                    break;
                    
                case 'domain':
                    errorMessageType = 'domainErrorMessage';
                    break;
                    
                case 'allowedDate':
                    errorMessageType = 'allowedDateErrorMessage';
                    break;
            }

            if (fieldData[errorMessageType] !== undefined) {
                return fieldData[errorMessageType];
            } else {
                return self.options[$(field).data().fieldType][errorMessageType];
            }
        };

    };

    $.fn.isValid = function (options) {

        return this.each(function () {

            var isValid = new $.isValid(this, options);
            $(this).data('isValid', isValid);

            isValid.init();

            var submitButton = $(isValid.formID + ' :input[type="submit"]');

            submitButton.click(function (e) {

                isValid.formArray.each(function (index, field) {

                    if (!isValid.isValidField(field)) {
                        isValid.showErrorFor(field);
                    } else {
                        isValid.hideErrorFor(field);
                    }
                    
                    isValid.isFormValid = isValid.isFormValidated();

                    if (!isValid.isFormValid) {

                        $('html, body').animate({
                            scrollTop: $("input.invalid").first().offset().top
                        }, 10);

                        e.preventDefault();
                    }
                });
            });

            isValid.formArray.each(function (index, field) {

                if(isValid.options.validateOnBlur) {
                    $(field).on('blur change', function () {

                        if (!isValid.isValidField(field)) {
                            isValid.showErrorFor(field);
                        } else {
                            if ($(field).attr('data-field-type') !== "notrequired") {
                                isValid.hideErrorFor(field);
                            }
                        }
                    });
                }
                
                $(field).on('isValid.fieldInvalidated', function(event) {
                    
                    var eventObject = {
                        isValid: false,
                        fieldElement: $(event.currentTarget),
                        fieldData: $(event.currentTarget).data()
                    };
                    
                    isValid.options[$(field).data().fieldType].callbacks.onInvalidated(eventObject);
                });

                $(field).on('isValid.fieldValidated', function(event) {
                    
                    var eventObject = {
                        isValid: true,
                        fieldElement: $(event.currentTarget),
                        fieldData: $(event.currentTarget).data()
                    };

                    if ($(field).data().fieldType != "notrequired") {
                        try {
                            isValid.options[$(field).data().fieldType].callbacks.onValidated(eventObject);
                        }
                        catch (err) {
                            console.log(err.message);
                        }
                    }

                });
                
            });
        });
    };
    
    $.isValid.defaults = {
        general: {
            activeErrorMessage: '',
            requiredErrorMessage: 'Field is required',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        letters: {
            activeErrorMessage: '',
            requiredErrorMessage: 'Field is required',
            invalidErrorMessage: 'Field is letters only',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        numbers: {
            activeErrorMessage: '',
            requiredErrorMessage: 'Field is required',
            invalidErrorMessage: 'Field is numbers only',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        decimals: {
            activeErrorMessage: '',
            requiredErrorMessage: 'Field is required',
            invalidErrorMessage: 'Field is decimals only',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        password: {
            minLength: 5,
            maxLength: 100,
            numbers: false,
            letters: true,
            passwordConfirm: false,
            activeErrorMessage: '',
            requiredErrorMessage: 'Password is required',
            formatErrorMessage: 'Password should contain numbers and letters',
            invalidErrorMessage: 'Password should be more than 5 characters',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        passwordConfirm: {
            activeErrorMessage: '',
            requiredErrorMessage: 'Confirming your Password is required',
            invalidErrorMessage: 'Passwords do not match',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        email: {
            domain: '',
            emailConfirm: false,
            activeErrorMessage: '',
            requiredErrorMessage: 'Email is required',
            domainErrorMessage: 'Email domain should be @xxxx.com',
            invalidErrorMessage: 'Please enter a valid email address',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        emailConfirm: {
            activeErrorMessage: '',
            requiredErrorMessage: 'Confirming your Email is required',
            invalidErrorMessage: 'Email addresses do not match',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        date: {
            format: 'DD/MM/YYYY',
            allowFutureDates: true,
            activeErrorMessage: '',
            requiredErrorMessage: 'Date is required',
            formatErrorMessage: 'Date format doesn\'t match DD/MM/YYYY',
            invalidErrorMessage: 'Please enter a valid Date',
            allowedDateErrorMessage: 'Date not allowed',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        postCode: {
            activeErrorMessage: '',
            requiredErrorMessage: 'Post Code is required',
            invalidErrorMessage: 'Please enter a valid Post Code',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        select: {
            activeErrorMessage: '',
            requiredErrorMessage: 'Choosing an option is required',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        checkbox: {
            activeErrorMessage: '',
            requiredErrorMessage: 'Checkbox is required',
            callbacks: {
                onValidated: function (event) {},
                onInvalidated: function (event) {}
            }
        },
        validateOnBlur: true,
        showErrorMessages: true,
        onFormValidated: function () {},
        onFormInvalidated: function () {},
        validators: {}
    };

})(jQuery);
/*==================================================================================================

Application:   Utility Function
Author:        John Gardner

Version:       V1.0
Date:          18th November 2003
Description:   Used to check the validity of a UK postcode

Version:       V2.0
Date:          8th March 2005
Description:   BFPO postcodes implemented.
               The rules concerning which alphabetic characters are allowed in which part of the 
               postcode were more stringently implementd.

Version:       V3.0
Date:          8th August 2005
Description:   Support for Overseas Territories added                 

Version:       V3.1
Date:          23rd March 2008
Description:   Problem corrected whereby valid postcode not returned, and 'BD23 DX' was invalidly 
               treated as 'BD2 3DX' (thanks Peter Graves)        

Version:       V4.0
Date:          7th October 2009
Description:   Character 3 extended to allow 'pmnrvxy' (thanks to Jaco de Groot)  

Version:       V4.1
               8th September 2011
               Support for Anguilla overseas territory added    

Version:       V5.0
Date:          8th November 2012
               Specific support added for new BFPO postcodes           

Parameters:    toCheck - postcodeto be checked. 

This function checks the value of the parameter for a valid postcode format. The space between the 
inward part and the outward part is optional, although is inserted if not there as it is part of the 
official postcode.

If the postcode is found to be in a valid format, the function returns the postcode properly 
formatted (in capitals with the outward code and the inward code separated by a space. If the 
postcode is deemed to be incorrect a value of false is returned.

Example call:

  if (checkPostCode (myPostCode)) {
    alert ("Postcode has a valid format")
  } 
  else {alert ("Postcode has invalid format")};

--------------------------------------------------------------------------------------------------*/

var checkPostCode = function(toCheck) {

    // Permitted letters depend upon their position in the postcode.
    var alpha1 = "[abcdefghijklmnoprstuwyz]"; // Character 1
    var alpha2 = "[abcdefghklmnopqrstuvwxy]"; // Character 2
    var alpha3 = "[abcdefghjkpmnrstuvwxy]"; // Character 3
    var alpha4 = "[abehmnprvwxy]"; // Character 4
    var alpha5 = "[abdefghjlnpqrstuwxyz]"; // Character 5
    var BFPOa5 = "[abdefghjlnpqrst]"; // BFPO alpha5
    var BFPOa6 = "[abdefghjlnpqrstuwzyz]"; // BFPO alpha6

    // Array holds the regular expressions for the valid postcodes
    var pcexp = [];

    // BFPO postcodes
    pcexp.push(new RegExp("^(bf1)(\\s*)([0-6]{1}" + BFPOa5 + "{1}" + BFPOa6 + "{1})$", "i"));

    // Expression for postcodes: AN NAA, ANN NAA, AAN NAA, and AANN NAA
    pcexp.push(new RegExp("^(" + alpha1 + "{1}" + alpha2 + "?[0-9]{1,2})(\\s*)([0-9]{1}" + alpha5 + "{2})$", "i"));

    // Expression for postcodes: ANA NAA
    pcexp.push(new RegExp("^(" + alpha1 + "{1}[0-9]{1}" + alpha3 + "{1})(\\s*)([0-9]{1}" + alpha5 + "{2})$", "i"));

    // Expression for postcodes: AANA  NAA
    pcexp.push(new RegExp("^(" + alpha1 + "{1}" + alpha2 + "{1}" + "?[0-9]{1}" + alpha4 + "{1})(\\s*)([0-9]{1}" + alpha5 + "{2})$", "i"));

    // Exception for the special postcode GIR 0AA
    pcexp.push(/^(GIR)(\s*)(0AA)$/i);

    // Standard BFPO numbers
    pcexp.push(/^(bfpo)(\s*)([0-9]{1,4})$/i);

    // c/o BFPO numbers
    pcexp.push(/^(bfpo)(\s*)(c\/o\s*[0-9]{1,3})$/i);

    // Overseas Territories
    pcexp.push(/^([A-Z]{4})(\s*)(1ZZ)$/i);

    // Anguilla
    pcexp.push(/^(ai-2640)$/i);

    // Load up the string to check
    var postCode = toCheck;

    // Assume we're not going to find a valid postcode
    var valid = false;

    // Check the string against the types of post codes
    for (var i = 0; i < pcexp.length; i++) {

        if (pcexp[i].test(postCode)) {

            // The post code is valid - split the post code into component parts
            pcexp[i].exec(postCode);

            // Copy it back into the original string, converting it to uppercase and inserting a space 
            // between the inward and outward codes
            postCode = RegExp.$1.toUpperCase() + " " + RegExp.$3.toUpperCase();

            // If it is a BFPO c/o type postcode, tidy up the "c/o" part
            postCode = postCode.replace(/C\/O\s*/, "c/o ");

            // If it is the Anguilla overseas territory postcode, we need to treat it specially
            if (toCheck.toUpperCase() == 'AI-2640') {
                postCode = 'AI-2640';
            }

            // Load new postcode back into the form element
            valid = true;

            // Remember that we have found that the code is valid and break from loop
            break;
        }
    }

    // Return with either the reformatted valid postcode or the original invalid postcode
    if (valid) {
        return postCode;
    } else return false;
};