'use strict';
const forms = require('MaterialForms');
const timer = require('timer');

const result = forms.task(function (progress) {
	
	for (var i = 0; i <= 100; i++) {
	    if (progress.cancelled) {
            // Check if cancellation has been requested
			return false;
		}
		
		if (i === 50) {
			progress.message = 'Almost finished...';
		}
		
	    // Report current progress (out of 100)
		progress(i);
		timer.block(50);
	}
	
    // Returns this value to the caller of forms.task()
	return true;
	
}, { message: 'Working', cancel: 'CANCEL' } );

if (result) {
	forms.alert("Completed successfully!");
} else {
	forms.alert("Canceled!");
}
