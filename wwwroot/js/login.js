document.addEventListener('DOMContentLoaded', function () {
    const loginButton = document.getElementById('buttonLogin');
    loginButton.addEventListener('click', loginUser);
});

async function loginUser() {
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('message'); 

  
    document.getElementById('password').addEventListener('focus', () => {
        errorMessage.textContent = '';
    });

    document.getElementById('username').addEventListener('focus', () => {
        errorMessage.textContent = '';
    });


    if (!username || !password) {
        errorMessage.textContent = "Username and password credentials cannot be empty.";
        return;
    }

    const data = {
        Username: username,
        Password: password
    };

    try {
        const response = await fetch('/LogInUser', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            const responseData = await response.json();
            if (responseData.errorMessage == "User not found" || responseData.errorMessage == "Invalid password") {
                errorMessage.textContent = responseData.errorMessage;
            } else {
                if (responseData.success == "admin") {
                    window.location.href = '/AdminPage';
                } else {
                    window.location.href = '/UserPage';
                }
            }
        } else {
            const responseData = await response.json();
            errorMessage.textContent = responseData.errorMessage;
        }
    } catch (error) {
        console.error('Error occurred while fetching:', error);
        errorMessage.textContent = "An error occurred while logging in.";
    }
}
