"use strict";

function showNeedToChooseMessage() {
    alert("Please choose any option");
}

function moveToSelect(fromId, toId) {
    var index = document.getElementById(fromId).options.selectedIndex;
    
    if (index === -1) {
        showNeedToChooseMessage();
        return;
    }
    
    var option = document.getElementById(fromId).options[index].cloneNode(true);

    document.getElementById(toId).appendChild(option);
    document.getElementById(fromId).options[index].remove();
}

function moveToAviable(fromId, toId) {
    var index = document.getElementById(fromId).options.selectedIndex;
    
    if (index === -1) {
        showNeedToChooseMessage();
        return;
    }
    
    var option = document.getElementById(fromId).options[index].cloneNode(true);

    document.getElementById(toId).appendChild(option);
    document.getElementById(fromId).options[index].remove();
}

function moveAllToSelect(fromId, toId) {
    var options = document.getElementById(fromId).options;
    var selected = document.getElementById(toId);
    
    for (var i = 0; i < options.length; i++) {
        selected.appendChild(options[i--]);
    }
}

function moveAllToAviable(fromId, toId) {
    var options = document.getElementById(fromId).options;
    var aviable = document.getElementById(toId);
    
    for (var i = 0; i < options.length; i++) {
        aviable.appendChild(options[i--]);
    }
}