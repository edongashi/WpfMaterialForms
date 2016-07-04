'use strict'
const forms = require('MaterialForms');

forms.alert('Hello world!');

const again = forms.prompt('Would you like to be greeted again?', null, 'YES', 'NO');

if (again) {
    forms.alert('Hello again!');
}
