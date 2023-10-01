document.addEventListener('DOMContentLoaded', function () {

    const textarea = document.querySelector('.text-area');
    const sendButton = document.getElementById('sendButton');
    const contentOutputDiv = document.getElementById('contentOutputDiv');

    sendButton.style.opacity = '0';

    textarea.addEventListener('focus', () => {
        sendButton.style.display = 'block';
        setTimeout(() => {
            sendButton.style.opacity = '1';
        }, 100);
    });

    textarea.addEventListener('blur', () => {
        sendButton.style.opacity = '0';
        setTimeout(() => {
            sendButton.style.display = 'none';
        }, 500);
    });

    sendButton.addEventListener('click', () => {
        animateTyping();
        tweet();
    });

    function animateTyping() {
        contentOutputDiv.textContent = '';
        contentOutputDiv.classList.remove('animation-trigger'); // Remove the class
        void contentOutputDiv.offsetWidth; // Trigger reflow
        contentOutputDiv.classList.add('animation-trigger'); // Add the class back
    }

    function tweet() {
        const tweetContent = textarea.value;

        if (!tweetContent) {
            alert("Please enter a tweet!");
            return;
        }

        const data = {
            contentInput: tweetContent
        };

        fetch('/Tweet', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                return response.json();
            })
            .then(data => {
                if (data.message === "Tweet posted successfully") {
                    contentOutputDiv.textContent = data.contentOutput;
                    textarea.value = '';
                } else {
                    alert("Failed to post. Please try again later.");
                }
            })
            .catch(error => {
                console.error('Error occurred while fetching:', error);
                alert("An error occurred while posting. Please try again later.");
            });
    }
});
