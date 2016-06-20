'use strict';
const MaterialForms = require('../MaterialForms.dll').MaterialForms;
const wrapper = require('./module-wrapper.js');
const forms = wrapper(MaterialForms);

MaterialForms.MaterialWindow.SetDefaultDispatcher(MaterialForms.DispatcherOption.Custom);
require('core').onDispose.next().then(forms.exit);

module.exports = forms;
