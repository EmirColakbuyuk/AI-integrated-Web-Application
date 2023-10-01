document.addEventListener('DOMContentLoaded', function () {
    const deleteButtons = document.querySelectorAll('.delete-btn');

    deleteButtons.forEach(button => {
        button.addEventListener('click', function (event) {
            const id = event.target.dataset.id;
            const action = event.target.dataset.action;

            if (confirm('Are you sure you want to delete this item?')) {
                fetch(`/${action}/${id}`, {
                    method: 'POST',
                })
                    .then(response => {
                        if (!response.ok) {
                            throw new Error('Network response was not ok.');
                        }
                        return response.json();
                    })
                    .then(data => {
                        if (data.success) {
                            window.location.reload();
                        } else {
                            alert('An error occurred while deleting the item.');
                        }
                    })
                    .catch(error => {
                        console.error('Error occurred while deleting:', error);
                        alert('An error occurred while deleting the item.');
                    });
            }
        });
    });
});
