'use strict';
const MaterialForms = require('../MaterialForms.dll').MaterialForms;
const wrapper = require('./module-wrapper.js');
module.exports = wrapper(MaterialForms);
MaterialForms.MaterialWindow.SetDefaultDispatcher(MaterialForms.DispatcherOption.Custom);
