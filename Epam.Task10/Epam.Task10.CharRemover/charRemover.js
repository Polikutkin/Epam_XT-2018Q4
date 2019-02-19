"use strict";
        
function removeDoubleLetters() {
    var text = document.getElementById("CheckText").value;

    var words = text.split(" ");
    var ignoredSymbols = [" ", ".", ",", ";", ":", "!", "?", "\t", "\n"];
    var doubledLetters = {};

    words.forEach(function (word) {
        word.split("").forEach(function (letter, i) {
            if (!doubledLetters[letter]
                && ignoredSymbols.indexOf(letter) == -1
                && word.indexOf(letter, i + 1) != -1) {
                    doubledLetters[letter] = true;
            }
        });
    });
    var result = text.split("").filter(function (letter) {
        return !doubledLetters[letter];
    }).join("");

    document.getElementById("res").innerText = result;
}