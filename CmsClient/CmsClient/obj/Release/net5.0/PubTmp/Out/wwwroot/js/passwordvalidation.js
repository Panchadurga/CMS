var captchaValidation = false;
var fieldValidation = false;
var UsernameValidation = false;
var EmailValidation = false;
let state = true;
let showPassword = document.getElementById('password');
let showcPassword = document.getElementById('confirmpassword');


//Toggle password visibility
function Passwordtoggle(show) {
    console.log("show", show);
    if (state) {
        showPassword.setAttribute("type", "password");
        state = false;
        show.classList.remove('fa-eye-slash');
        show.classList.toggle('fa-eye-slash');
        show.classList.remove('fa-eye');
    }
    else {
        showPassword.setAttribute("type", "text");
        state = true;
        show.classList.toggle('fa-eye');
        show.classList.remove('fa-eye-slash');
    }
}
//Toggle confirmpassword visibility
function ConfirmPasswordtoggle(show) {
    if (state) {
        showcPassword.setAttribute("type", "password");
        state = false;
        show.classList.remove('fa-eye-slash');
        show.classList.toggle('fa-eye-slash');
        show.classList.remove('fa-eye');
    }
    else {
        showcPassword.setAttribute("type", "text");
        state = true;
        show.classList.toggle('fa-eye');
        show.classList.remove('fa-eye-slash');
            
    }
}


let password = document.querySelector(".checkpassword");
password.addEventListener("keyup", Checkpassword);

let cpassword = document.querySelector(".confirmpassword");
cpassword.addEventListener("keyup", Confirmpassword);


//Confirm password validation
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

//Password validation
function Checkpassword() {
    let password = document.querySelector(".checkpassword").value;
    let comment;


    function hasNumber(myString) {
        return /\d/.test(myString);
    }


    function isUpper(string) {
        return /(?=.*[A-Z])/.test(string);
    }

    function isLower(string) {
        return /(?=.*[a-z])/.test(string);
    }


    function isSpecialChar(string) {
        return /(?=.*[!@#$%^&*])/.test(string);
    }
    //console.log(password);
    
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
}

//Jquery callback function for username autosuggestion
$(function () {
    $("#username").keyup(function () {
        var val = $("#username").val();
        $(".usernameautosuggestion").load("/User/CheckUsernameAvailability", { username: val }, function () {
            var Usererr = $("#usernameerr").text();
            console.log(Usererr);

            if (Usererr.length > 0) { //username already exists in server
                $('.toggle-disabled').prop("disabled", true);//disable  
                UsernameValidation= false;
            }
            else {
                UsernameValidation = true;
                if (fieldValidation && captchaValidation && EmailValidation) {
                    // all validations passed
                    $('.toggle-disabled').prop("disabled", false);//enable  
                }
            }
        });

    });
})

//Jquery callback function for email availability
$(function () {
    $("#email").keyup(function () {
        var val = $("#email").val();
        $(".emailvalidation").load("/User/CheckEmailAvailability", { email: val }, function () {
            var Emailerr = $("#emailerr").text();
            console.log(Emailerr);

            if (Emailerr.length > 0) { //email already exists or invalid email
                $('.toggle-disabled').prop("disabled", true);//disable  
                EmailValidation = false;
            }
            else {
                EmailValidation = true;
                if (fieldValidation && captchaValidation && UsernameValidation) {
                    // all validations passed
                    $('.toggle-disabled').prop("disabled", false);//enable  
                }
            }
        });
    });
})

//Keyup event trigger for required fields 
$(document).on('change keyup', '.required', function (e) {


    let Disabled = true;
    $(".required").each(function () {
        let value = this.value
        if ((value) && (value.trim() != '')) {
            Disabled = false //not empty
        } else {
            Disabled = true // if empty
            return false
        }
    });
    //If any one of the field is not filled
    if (Disabled) {

        $('.toggle-disabled').prop("disabled", true);//disable
        console.log("required field empty disabled");

    }
    //if all fields are filled(required passed)
    else {
        var cmt = document.getElementById("cmt").textContent;
        var cmtcp = document.getElementById("cmtcp").textContent;
       

        if (cmt == "Kudos! That's a strong password" && cmtcp != "Password does not match") { //fieldvalidation passed
            if (captchaValidation == true && UsernameValidation == true && EmailValidation == true) {
                // all validations passed

                $('.toggle-disabled').prop("disabled", false); //button enabled
                console.log(UsernameValidation);
                console.log(EmailValidation);
                console.log("register enabled");
                fieldValidation = true;
            }
               
                /*$('.toggle-disabled').style.cursor = 'pointer';*/
            
            else {
                fieldValidation = true;
                //fieldValidation passed - but any one of captcha or username or email failed
                $('.toggle-disabled').prop("disabled", true);//disable        
              
                console.log("email /username err disabled");

            }

        }
        else { //field validation failed
            
            fieldValidation = false;
            $('.toggle-disabled').prop("disabled", true); //disable
            console.log("required fields err disabled");
        }
    }
})

//Google recaptcha callback function
function recaptcha_callback() {
   
    captchaValidation = true;

    if (fieldValidation && UsernameValidation && EmailValidation) {
        // all validations passed
        $('.toggle-disabled').prop("disabled", false); //enable

        console.log("register enabled");
    }
}