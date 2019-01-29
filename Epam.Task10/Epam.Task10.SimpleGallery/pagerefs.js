function goToPage(pageRef) {
    if (pageRef) {
        document.location.replace("" + pageRef);
    }
}

function closePage() {
    open(location, "_self").close();
}