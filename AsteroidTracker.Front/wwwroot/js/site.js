document.getElementById("pass-toggle").addEventListener('click', function (e) {
    let password = document.getElementById("password-field")
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password'
    password.setAttribute('type', type)
});

async function Login() {
    event.preventDefault();

    let data = JSON.stringify({
        Username: document.getElementById("mail-field").value,
        Password: document.getElementById("password-field").value
    })

    fetch('https://localhost:7224/Login', {
        headers: {
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*'
        },
        method: 'POST',
        body: data
    })
    .then(response => response.json())
    .then(function (x) {
        if (x.success === true) {
            window.location.href = `https://localhost:7283/AsteroidList?token=${ x.token }`;
        } else {
            document.getElementById("login-error")style.display = "block";
        }
        console.log(x);
    })

}