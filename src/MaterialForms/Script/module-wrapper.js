'use strict';
const async = require('async');

function wrapModule(MaterialForms) {
    const windowFactory = MaterialForms.WindowFactory;
    const clrAlert = windowFactory.Alert.toFunction();
    const clrPrompt = windowFactory.Prompt.toFunction();

    /**
     * Synchronously displays an alert dialog.
     * @param {string} message - Dialog message.
     * @param {string} title - Dialog title.
     * @param {string} action - Confirmation action button text.
     * @returns {boolean} - True if button is clicked,
     * null if the dialog is closed otherwise.
     */
    function alert() {
        async.wait(clrAlert.apply(global, arguments).Show());
    }

    /**
     * Synchronously displays a prompt dialog with positive and negative actions.
     * @param {string} message - Dialog message.
     * @param {string} title - Dialog title.
     * @param {string} positiveAction - Positive action button text.
     * @param {string} negativeAction - Negative action button text.
     * @returns {boolean} - True if positive button is clicked, 
     * false if negative button is clicked, null if the dialog is closed otherwise.
     */
    function prompt() {
        return async.wait(clrPrompt.apply(global, arguments).Show());
    }

    /**
     * Runs a function synchronously and shows a progress dialog while 
     * that function is running.  A progress controller is passed to the
     * function to allow altering the dialog and checking for cancellation.
     * @param {function} callback - Function to be called.
     * @param {Object} options - Optional options object.
     * @param {string} options.message - The message that is shown in the progress dialog.
     * @param {string} options.title - The title that is shown in the progress dialog.
     * @param {string} options.cancel - The cancellation button text. If left unspecified the button will not show.
     * @param {number} options.progress - Initial value of the progress bar.
     * @param {number} options.maximum - Maximum value of the progress bar.
     * @param {boolean} options.indeterminate - Indicates whether the progress bar is indeterminate.
     * @returns {} - Result of the callback.
     */
    function task(callback, options) {
        if (typeof callback !== 'function') {
            return null;
        }

        if (MaterialForms.MaterialApplication.CheckDispatcherAccess()) {
            return callback(function () { });
        }

        const progressSchema = new MaterialForms.ProgressSchema();
        if (callback.length === 0) {
            progressSchema.IsIndeterminate = true;
            progressSchema.ShowPercentage = false;
        }

        let window;
        let supportsCancellation = false;
        if (options && typeof options === 'object') {
            if (Number.isInteger(options.maximum)) {
                progressSchema.Maximum = options.maximum;
            }

            if (typeof options.progress === 'number') {
                progressSchema.Progress = options.progress;
            }

            window = windowFactory.FromSingleSchema(options.message || null, options.title || null, null, options.cancel || null, progressSchema);
            if (options.cancel) {
                supportsCancellation = true;
            }

            if (options.indeterminate) {
                progressSchema.IsIndeterminate = true;
                progressSchema.ShowPercentage = false;
            }
        } else {
            window = windowFactory.FromSingleSchema(null, null, null, null, progressSchema);
        }

        function progress(value, maximum) {
            if (Number.isInteger(maximum)) {
                progressSchema.Maximum = maximum;
            }

            if (typeof value === 'number') {
                progressSchema.Progress = value;
            }
        }

        const dialog = window.Dialog;
        Object.defineProperty(progress, 'message', {
            get() {
                return dialog.Message;
            },
            set(value) {
                if (typeof value === 'string') {
                    dialog.Message = value;
                }
            }
        });

        if (supportsCancellation) {
            const listener = new MaterialForms.DialogActionListener(dialog, 'negative');
            Object.defineProperty(progress, 'cancelled', {
                get() {
                    return listener.ActionPerformed;
                }
            });
        }

        const session = window.ShowTracked();
        try {
            return callback(progress);
        } finally {
            session.Close(true);
        }
    }

    /**
    * Asynchronously displays an alert dialog.
    * @param {string} message - Dialog message.
    * @param {string} title - Dialog title.
    * @param {string} action - Confirmation action button text.
    * @returns {Promise.<boolean>} - A promise that resolves to true if positive button is clicked, 
    * false if negative button is clicked, null if the dialog is closed otherwise.
    */
    function alertAsync() {
        return async(clrAlert.apply(global, arguments).Show());
    }

    /**
    * Asynchronously displays a prompt dialog with positive and negative actions.
    * @param {string} message - Dialog message.
    * @param {string} title - Dialog title.
    * @param {string} positiveAction - Positive action button text.
    * @param {string} negativeAction - Negative action button text.
    * @returns {Promise.<boolean>} - A promise that resolves to true 
    * if button is clicked, null if the dialog is closed otherwise.
    */
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
