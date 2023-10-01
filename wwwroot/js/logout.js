document.addEventListener('DOMContentLoaded', function () {
    const LogoutButton = document.getElementById('LogoutButton');
    LogoutButton.addEventListener('click', logout);
});

function logout() {
    fetch('/Logout', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
    })
        .then(response => {
            if (response.ok) {
               
                window.location.href = '/Index'; 
            } else {
          
                console.error('Logout failed:', response.statusText);
            }
        })
        .catch(error => {
            console.error('Error occurred while logging out:', error);
        });
}

