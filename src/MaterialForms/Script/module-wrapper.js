'use strict';

function wrapModule(MaterialForms) {
    const async = require('async');

    const clrAlert = MaterialForms.WindowFactory.Alert.toFunction();
    const clrPrompt = MaterialForms.WindowFactory.Prompt.toFunction();

    function alert() {
        async.wait(clrAlert.apply(global, arguments).Show());
    }

    function prompt() {
        return async.wait(clrPrompt.apply(global, arguments).Show());
    }

    function alertAsync() {
        return async(clrAlert.apply(global, arguments).Show());
    }

    function promptAsync() {
        return async(clrPrompt.apply(global, arguments).Show());
    }

    return {
        alert,
        prompt,
        async: {
            alert: alertAsync,
            prompt: promptAsync
        }
    };
}

module.exports = wrapModule;
