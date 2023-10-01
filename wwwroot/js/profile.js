document.addEventListener('DOMContentLoaded', function () {
    const changePasswordButton = document.getElementById('changePasswordButton');
    const changePasswordForm = document.getElementById('changePasswordForm');

    changePasswordButton.addEventListener('click', function () {
        changePasswordForm.style.display = 'block';
    });

    const confirmChangePasswordButton = document.getElementById('confirmChangePasswordButton');
    confirmChangePasswordButton.addEventListener('click', changePassword);

    const newPasswordInput = document.getElementById('newPassword');
    const confirmNewPasswordInput = document.getElementById('confirmNewPassword');
    const passwordMatchMessage = document.getElementById('passwordMatchMessage');

    function checkPasswordsMatch() {
        const newPassword = newPasswordInput.value;
        const confirmNewPassword = confirmNewPasswordInput.value;
        
        
        
        if (newPassword !== "" && confirmNewPassword !== "") {
            if (newPassword !== confirmNewPassword) {
                passwordMatchMessage.textContent = "New passwords do not match.";
                
            } else {
                passwordMatchMessage.textContent = ""; 
                
            }
        } else {
            
            passwordMatchMessage.textContent = "";
           
        }
    }
    newPasswordInput.addEventListener('input', checkPasswordsMatch);
    confirmNewPasswordInput.addEventListener('input', checkPasswordsMatch);
});

async function changePassword() {
    const oldPassword = document.getElementById('oldPassword').value;
    const newPassword = document.getElementById('newPassword').value;
    const confirmNewPassword = document.getElementById('confirmNewPassword').value;

    if (!oldPassword || !newPassword || !confirmNewPassword) {
        document.getElementById('changePasswordMessage').textContent = "All fields are required.";
        return;
    }

    else if (newPassword !== confirmNewPassword) {
        document.getElementById('changePasswordMessage').textContent = "New passwords do not match.";
        return;
    }

    const data = {
        OldPassword: oldPassword,
        NewPassword: newPassword
    };

    try {
        const response = await fetch('/ChangePassword', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        });

        if (response.ok) {
            const responseData = await response.json();
            if (responseData.success) {
                document.getElementById('changePasswordMessage').textContent = "Password changed successfully.";
            } else {
                document.getElementById('changePasswordMessage').textContent = responseData.errorMessage;
            }
        } else {
            const responseData = await response.json();
            document.getElementById('changePasswordMessage').textContent = responseData.errorMessage;
        }
    } catch (error) {
        console.error('Error occurred while fetching:', error);
        document.getElementById('changePasswordMessage').textContent = "An error occurred while changing the password.";
    }
}
