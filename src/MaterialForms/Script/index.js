'use strict';
const MaterialForms = require('../MaterialForms.dll').MaterialForms;
const wrapper = require('./module-wrapper.js');
const forms = wrapper(MaterialForms);

MaterialForms.MaterialApplication.SetDefaultDispatcher(MaterialForms.DispatcherOption.Custom);
core.onDispose.next().then(forms.exit);

module.exports = forms;
