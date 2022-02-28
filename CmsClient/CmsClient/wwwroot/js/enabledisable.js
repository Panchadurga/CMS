
$("body").on('DOMSubtreeModified', "#emailerr", function () {

    console.log("")

    if (document.getElementById('emailerr').innerText.length > 0) {

        console.log("button enabled");

        $('.toggle-disabled').prop("disabled", true);

    }



});
$("body").on('DOMSubtreeModified', "#usernameerr", function () {

    console.log("user")

    if (document.getElementById('usernameerr').innerText.length > 0) {

        console.log("button enabled");

        $('.toggle-disabled').prop("disabled", true);

    }

});
