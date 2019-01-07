function stopTimer() {
    bootbox.confirm("Are you sure you wish to stop the timer?", function (result) {

        if (result) {
            clearInterval(timer);
            jQuery("#timerStartBtn").prop("disabled", false);
            jQuery("#timerStopBtn").prop("disabled", true)
            $("#time").text("25:00");
        }

    });
}

function startTimer(duration, display) {
    jQuery("#timerStartBtn").prop("disabled", true);
    jQuery("#timerStopBtn").prop("disabled", false)

    var minutes, seconds;
    timer = setInterval(function () {
        minutes = parseInt(duration / 60, 10);
        seconds = parseInt(duration % 60, 10);


        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.text(minutes + ":" + seconds);

        if (--duration < 0) {
            updateExperience();
            $("#time").text("EXPIRED!");
            jQuery("#timerStartBtn").prop("disabled", false);
            jQuery("#timerStopBtn").prop("disabled", true)
            clearInterval(timer);
            pomodoroCompleted();
            return;
        }

    }, 1000);
}