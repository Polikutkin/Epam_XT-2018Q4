"use strict";

function calculateExpression() {
    var text = document.getElementById("CheckText").value;
    var elementsTemplate = /(\+|-|\*|\/)|([0-9]+(\.[0-9]+)*)/g;
    var elements = text.match(elementsTemplate);
    var operators = "+-";
    
    if (operators.indexOf(elements[0]) != -1) {
        elements[0] += elements[1];
    }
    var result = +elements[0];
    elements.forEach(function (element, i) {
        switch (element) {
            case "+":
                result += +elements[i + 1];
               break;
            case "-":
                result -= elements[i + 1];
               break;
            case "*":
                result *= elements[i + 1];
               break;
            case "/":
                result /= elements[i + 1];
               break;
        }
    });
    document.getElementById("res").innerText = text + " = " + result.toFixed(2);
}