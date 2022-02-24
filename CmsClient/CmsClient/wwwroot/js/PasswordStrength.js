let state = false;
let password = document.getElementById('password');
let confirmPassword = document.getElementById('confirmpassword');
let passwordStrength = document.getElementById('password-strength');
let lowerUpperCase = document.querySelector('.lower-upper-case i');
let number = document.querySelector('.one-number i');
let specialCharacter = document.querySelector('.one-special-char i');
let eightCharacter = document.querySelector('.eight-character i');


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

password.addEventListener('keyup', function () {
    let pass = password.value;
    checkStrength(pass);
})
function checkStrength(password) {
    let strength = 0;

    //if password contains lower and upper case character
    if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {
        strength += 1;
        lowerUpperCase.classList.remove('fa-circle');
        lowerUpperCase.classList.add('fa-check');
    }
    else {
        lowerUpperCase.classList.add('fa-circle');
        lowerUpperCase.classList.remove('fa-check');

    }

    //if password has a number
    if (password.match(/([0-9])/)) {
        strength += 1;
        number.classList.remove('fa-circle');
        number.classList.add('fa-check');
    }
    else {
        number.classList.add('fa-circle');
        number.classList.remove('fa-check');

    }

    //if password has one special character
    if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) {
        strength += 1;
        specialCharacter.classList.remove('fa-circle');
        specialCharacter.classList.add('fa-check');
    }
    else {
        specialCharacter.classList.add('fa-circle');
        specialCharacter.classList.remove('fa-check');

    }

    //if password is more than 8
    if (password.length > 7) {
        strength += 1;
        eightCharacter.classList.remove('fa-circle');
        eightCharacter.classList.add('fa-check');
    }
    else {
        eightCharacter.classList.add('fa-circle');
        eightCharacter.classList.remove('fa-check');

    }

    if (strength == 0) {
        passwordStrength.style = 'width: 0 %';
    }
    else if (strength == 2) {
        passwordStrength.classList.remove('progress-bar-warning');
        passwordStrength.classList.remove('progress-bar-success');
        passwordStrength.classList.add('progress-bar-danger');
        passwordStrength.style = 'width: 10%';
    }
    else if (strength == 3) {
        passwordStrength.classList.remove('progress-bar-success');
        passwordStrength.classList.remove('progress-bar-danger');
        passwordStrength.classList.add('progress-bar-warning');
        passwordStrength.style = 'width: 60%';
    }
    else if (strength == 4) {
        passwordStrength.classList.remove('progress-bar-danger');
        passwordStrength.classList.remove('progress-bar-success');
        passwordStrength.classList.add('progress-bar-success');
        passwordStrength.style = 'width: 100%';
    }
}