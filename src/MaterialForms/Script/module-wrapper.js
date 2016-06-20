'use strict';
const async = require('async');

function wrapModule(MaterialForms) {
    const windowFactory = MaterialForms.WindowFactory;
    const clrAlert = windowFactory.Alert.toFunction();
    const clrPrompt = windowFactory.Prompt.toFunction();

    function alert() {
        async.wait(clrAlert.apply(global, arguments).Show());
    }

    function prompt() {
        return async.wait(clrPrompt.apply(global, arguments).Show());
    }

    function task(callback, options) {
        if (typeof callback !== 'function') {
            return null;
        }

        if (MaterialForms.MaterialWindow.CheckDispatcherAccess()) {
            return callback(function () { });
        }

        const progressSchema = new MaterialForms.ProgressSchema();
        if (callback.length === 0) {
            progressSchema.IsIndeterminate = true;
            progressSchema.ShowPercentage = false;
        }

        let window;
        if (options && typeof options === 'object') {
            if (Number.isInteger(options.maximum)) {
                progressSchema.Maximum = options.maximum;
            }

            if (typeof options.progress === 'number') {
                progressSchema.Progress = options.progress;
            }

            window = windowFactory.FromSingleSchema(options.message || null, options.title || null, null, options.cancel || null, progressSchema);
        } else {
            window = windowFactory.FromSingleSchema(null, null, null, null, progressSchema);
        }

        function progress(value, maximum) {
            if (typeof value === 'number') {
                progressSchema.Progress = value;
            }

            if (Number.isInteger(maximum)) {
                progressSchema.Maximum = maximum;
            }
        }

        const session = window.ShowTracked();
        try {
            return callback(progress);
        } finally {
            session.Close();
        }
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
        task,
        async: {
            alert: alertAsync,
            prompt: promptAsync
        },
        exit: function () {
            MaterialForms.MaterialWindow.ShutDownApplication();
        }
    };
}

module.exports = wrapModule;
