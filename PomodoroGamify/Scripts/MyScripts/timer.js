function stopTimer() {
    bootbox.confirm("Are you sure you wish to stop the timer?", function (result) {

        if (result) {
            document.getElementById("alertBox").style = "display: none;";
            clearInterval(timer);
            jQuery("#timerStartBtn").prop("disabled", false);
            jQuery("#timerStopBtn").prop("disabled", true);
            $("#time").text("25:00");
            failedPomodoroPost();
        }

    });
}

function failedPomodoroPost() {

    $.ajax({
        url: "/Home/FailedPomodoro",
        method: "POST",
        data: {},
        success: function (data) { }
    });

}

function startTimer(duration, display) {



    if (selectedQuestId !== "") {


        var ul = document.getElementById(selectedQuestId);

        var items = ul.getElementsByTagName("span");

        if (items[0].id > level) {
            bootbox.alert("You do not have the required level (" + items[0].id + ") to start " + selectedQuestId);
            return;
        }
            

        document.getElementById("alertBox").removeAttribute("style");
        document.getElementById("alertBox").innerHTML = "<strong>You are working on " + selectedQuestId + "<strong>";
    }




    jQuery("#timerStartBtn").prop("disabled", true);
    jQuery("#timerStopBtn").prop("disabled", false);

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
            jQuery("#timerStopBtn").prop("disabled", true);
            clearInterval(timer);
            pomodoroCompleted();
            return;
        }

    }, 1000);
}