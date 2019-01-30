
var timer;
var paused = false;
var min = 0;
var sec = 5;

function startTimer() {

    timer = setInterval(function() {
        var timerBlock = document.getElementById("timer");

        if (timerBlock != null) {
            timerBlock.innerHTML = min +" : " + sec ;
            sec--;
            
            if (sec <= -1)
            {
                if (min <= 0) {
                    clearInterval();
                    goToNextPage()
                }
                
                min--;
                sec = 59;
            }
        }
    }, 1000);
}

function goToNextPage() {
    var nextPage = document.getElementById("timeoutPageRef");

    if (nextPage != null) {
        var nextPageRef = nextPage.getAttribute("href");
    }

    document.location.replace("" + nextPageRef);
}

function pauseTimer(pauseButton) {
    if (!paused) {
        paused = true;
        pauseButton.innerHTML = "> Continue timer";

        if (timer !== undefined) {
            clearInterval(timer);
        }
    }
    else {
        paused = false;
        pauseButton.innerHTML = "|| Pause timer";
        
        if (timer !== undefined) {
            startTimer();
        }
    }
}

function goToPage(pageRef) {
    if (pageRef) {
        document.location.replace("" + pageRef);
    }
}

function closePage(page) {
    document.location.replace("" + page);
    window.open(page, '_self', '');
    window.close();
}