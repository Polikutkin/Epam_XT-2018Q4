"use strict";

function showNeedToChooseMessage() {
    alert("Please choose any option");
}

function moveOneOption(fromId, toId) {
    var index = document.getElementById(fromId).options.selectedIndex;
    
    if (index === -1) {
        showNeedToChooseMessage();
        return;
    }
    
    var option = document.getElementById(fromId).options[index].cloneNode(true);

    document.getElementById(toId).appendChild(option);
    document.getElementById(fromId).options[index].remove();
}

function moveAllOptions(fromId, toId) {
    var options = document.getElementById(fromId).options;
    var target = document.getElementById(toId);
    
    while (options.length > 0) {
        target.appendChild(options[0]);
    }
}