function StartWaiting(spinner) {
    if (arguments.length == 0) {
        spinner = 1;
    }
    if (spinner < 0 || spinner > 8) {
        spinner = 1;
    }
    $.blockUI({
        css: {
            border: 'none',
            padding: '15px',
            backgroudColor: 'rgba(215, 44, 44, 0',
            opacity: 1,
            color: '#fff'
        },
        message: '<div class="loader' + spinner + '">Loading...</div>'
    });
}

function StopWaiting() {
    $.unblockUI();
}