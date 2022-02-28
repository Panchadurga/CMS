let state = false;
let password = document.getElementById('password');
let confirmPassword = document.getElementById('confirmpassword');

function myFunction(show) {
    show.classList.toggle('fa-eye-slash');
}

function Passwordtoggle() {
    if (state) {
        password.setAttribute("type", "password");
        state = false;
    }
    else {
        password.setAttribute("type", "text");
        state = true;
    }
}
function ConfirmPasswordtoggle() {
    if (state) {
        confirmPassword.setAttribute("type", "password");
        state = false;
    }
    else {
        confirmPassword.setAttribute("type", "text");
        state = true;
    }
}

