
var captchaValidation = false;
var fieldValidation = false;
let state = false;
let showPassword = document.getElementById('password');
let showcPassword = document.getElementById('confirmpassword');

function myFunction(show) {
    show.classList.toggle('fa-eye-slash');
}

function Passwordtoggle() {
    if (state) {
        showPassword.setAttribute("type", "password");
        state = false;
    }
    else {
        showPassword.setAttribute("type", "text");
        state = true;
    }
}
function ConfirmPasswordtoggle() {
    if (state) {
        showcPassword.setAttribute("type", "password");
        state = false;
    }
    else {
        showcPassword.setAttribute("type", "text");
        state = true;
    }
}



let password = document.querySelector(".checkpassword");
password.addEventListener("keyup", Checkpassword);

let cpassword = document.querySelector(".confirmpassword");
cpassword.addEventListener("keyup", Confirmpassword);

function Confirmpassword() {
    let password = document.querySelector(".checkpassword").value;
    let cpassword = document.querySelector(".confirmpassword").value;

    if (password != cpassword) {
        document.getElementById("cmtcp").style.color = "red";
        document.querySelector(".commentcp").innerHTML = "Password does not match";
    }
    else {
        document.getElementById("cmtcp").style.color = "green";
        document.querySelector(".commentcp").innerHTML = "";
    }

}

function Checkpassword() {
    let password = document.querySelector(".checkpassword").value;
    //let length = password.length;
    let comment;

   
    function hasNumber(myString) {
        return /\d/.test(myString);
    }

    function isUpper(string) {
        for (let i = 0; i < string.length; i++) {
            if (!isSpecialChar(string[i])) {
                if (string[i] == string[i].toUpperCase()) {
                    return true;
                }
            }
        }
        return false;
    }

    function isLower(string) {
        for (let i = 0; i < string.length; i++) {
            if (!isSpecialChar(string[i])) {
                if (string[i] == string[i].toLowerCase()) {
                    return true;
                }
            }
        }
        return false;
    }


    function isSpecialChar(string) {
        let result = false;
        for (let i = 0; i < string.length; i++) {
            if (string[i] == "!" || string[i] == "@" || string[i] == "#" || string[i] == "$" || string[i] == "%" || string[i] == "&" || string[i] == "*") {
                result = true;
                break;
            }
        }
        return result;
    }
    if (password.length <= 8) {
        comment = "Password must contain at least 8 characters";
    }
    else if (isUpper(password) == false) {
        comment = "Password must contain at least one uppercase character";
    } else if (isLower(password) == false) {
        comment = "Password must contain at least one lowercase character";
    } else if (hasNumber(password) == false) {
        comment = "Password must contain at least one numeric character";
    } else if (isSpecialChar(password) == false) {
        comment = "Password must contain at least one special character";
    } 
    else {
        comment = "Kudos! That's a strong password";
    }

    if (comment == "Kudos! That's a strong password") {
        document.getElementById("cmt").style.color = "green";
    }
    else {
        document.getElementById("cmt").style.color = "red";
    }
    document.querySelector(".comment").innerHTML = comment;
    /*document.querySelector(".length").innerHTML = length;*/
}
//function ValidateEmail() {
//    var email = document.getElementById("email").value;
//    if (/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(email)) {
//        return (true)
//    }   
//    return (false)
//}

$(function () {
    $("#username").keyup(function () {
        var val = $("#username").val();
        $(".usernameautosuggestion").load("/User/CheckUsernameAvailability", { username: val });
    });
})

$(function () {
    $("#email").keyup(function () {
        var val = $("#email").val();
        $(".emailvalidation").load("/User/CheckEmailAvailability", { email: val });
    });
})


//Enable/disable submit button - conditional rendering
$(document).on('change keyup', '.required', function (e) {
    

    let Disabled = true;
    $(".required").each(function () {
        let value = this.value
        if ((value) && (value.trim() != '')) {
            Disabled = false
        } else {
            Disabled = true
            return false
        }
    });

    if (!Disabled) {

        $('.toggle-disabled').prop("disabled", true);//disable
        console.log("161");

    }
    else {
        var cmt = document.getElementById("cmt").textContent;
        var cmtcp = document.getElementById("cmtcp").textContent;
        var email = document.getElementById("email").value;



        //    //var emailerr = "dfgh";
        //    //var usernameerr = "dfgh";
        //    //console.log(usernameerr);

        //    //if (emailerr.length > 0 || usernameerr.length > 0) {
        //    //    $('.toggle-disabled').prop("disabled", true); //disable
        //    //    console.log(emailerr.length, usernameerr.length)
        //    //    fieldValidation = false;
        //    //    console.log("fail in 181");
        //    //}

        if (cmt == "Kudos! That's a strong password" && cmtcp != "Password does not match") {
            if (captchaValidation == true) {
                $('.toggle-disabled').prop("disabled", false); //enable
                console.log("pass");
                fieldValidation = true;
                /*$('.toggle-disabled').style.cursor = 'pointer';*/
            }
            else {
                fieldValidation = true;
                $('.toggle-disabled').prop("disabled", true);//disable        
                console.log("192");

            }

        }
        else {
            fieldValidation = false;
            $('.toggle-disabled').prop("disabled", true); //disable
            /*$('.toggle-disabled').style.cursor = 'not-allowed';*/
            console.log("201");

        }                 
        
    }
})

function recaptcha_callback() {
    var registerBtn = document.querySelector("#registerBtn");
    captchaValidation = true;

    if (fieldValidation) {
        registerBtn.removeAttribute('disabled');
        
        /*registerBtn.style.cursor = 'pointer';*/
    }
    
    
}