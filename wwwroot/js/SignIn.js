document.addEventListener('DOMContentLoaded', function () {
    const signinButton = document.getElementById('buttonSignIn');
    signinButton.addEventListener('click', SignInUser);
});

async function SignInUser() {
    const name = document.getElementById('name').value;
    const surname = document.getElementById('surname').value;
    const username = document.getElementById('username').value;
    const mail = document.getElementById('mail').value;
    const password = document.getElementById('password').value;

    if (!name || !surname || !username || !mail || !password) {
        document.getElementById('message').textContent = "All fields are required.";
        return;
    }

    const data = {
        Name: name,
        Surname: surname,
        Username: username,
        Mail: mail,
        Password: password
    };

    try {
        const response = await fetch('/SignIn', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            const responseData = await response.json();
            if (responseData.success) {
                window.location.href = '/UserPage';
            } else {
                document.getElementById('message').textContent = responseData.message;
                
            }
        } else {
            const responseData = await response.json();
            document.getElementById('message').textContent = responseData.message;
           
        }
    } catch (error) {
        console.error('Error occurred while fetching:', error);
        document.getElementById('message').textContent = "An error occurred while signing in.";
    }
}
