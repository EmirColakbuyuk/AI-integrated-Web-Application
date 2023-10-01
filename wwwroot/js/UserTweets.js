function showToast(message, duration = 2000) {
    const toast = document.createElement("div");
    toast.className = "toast";
    toast.textContent = message;
    document.body.appendChild(toast);

    setTimeout(() => {
        toast.style.opacity = "0";
        setTimeout(() => {
            document.body.removeChild(toast);
        }, 300);
    }, duration);
}

function formatChildData(data) {
    const date = data.Date;
    const url = data.URL;
    return `Date: ${date}<br>URL: ${url}`;
}


function copyToClipboard(text) {
    const textArea = document.createElement("textarea");
    textArea.value = text;
    document.body.appendChild(textArea);
    textArea.select();
    document.execCommand('copy');
    document.body.removeChild(textArea);
    showToast("URL copied to clipboard:" + text);
}

async function getTweetsByUserId(userId) {
    try {
        const response = await fetch(`/Tweets/${userId}`);
        if (response.ok) {
            const tweetsData = await response.json();
            return tweetsData;
        } else if (response.status == 404) {
            throw new Error("No tweets found for the user.");
        } else {
            throw new Error("Failed to fetch tweets.");
        }
    } catch (error) {
        console.error("Error getting tweets:", error);
        throw error;
    }
}

// Modify the displayTweets function
function displayTweets(tweets) {
    let table = new DataTable('#tweetTable', {
        data: tweets.map((tweet) => ({
            "Tweet": tweet.contentInput,
            "ContentOutput": tweet.contentOutput,
            "Date": tweet.createdAt,
            "URL": "http://localhost:5279/" + tweet.url,
        })),
        columns: [
            { title: "Tweet", data: "Tweet" },
            { title: "ContentOutput", data: "ContentOutput" },
            { title: "Date", data: "Date" },
            { title: "URL", data: "URL" }
        ],
        paging: true,
        searching: false,
        info: true,
        pageLength: 20, 
        ordering: true,
        order: [[2, 'asc']],
        columnDefs: [
            { targets: [0, 1, 3], orderable: false }
        ]
    });

    // Hide the "ContentOutput", "Date", and "URL" columns initially
    table.column(1).visible(false); // Column 1 is "ContentOutput"
    table.column(2).visible(false); // Column 2 is "Date"
    table.column(3).visible(false); // Column 3 is "URL"

    $('#tweetTable tbody').on('click', 'tr', function () {
        const row = table.row(this);
        const icon = row.child.isShown() ? '-' : '+';
        if (icon === '-') {
            // If the row is already expanded, hide "ContentOutput", "Date", and "URL"
            table.column(1).visible(false); // Column 1 is "ContentOutput"
            table.column(2).visible(false); // Column 2 is "Date"
            table.column(3).visible(false); // Column 3 is "URL"
        } else {
            // If the row is not expanded, show "ContentOutput", "Date", and "URL"
            const data = row.data();
            table.column(1).visible(true); // Column 1 is "ContentOutput"
            table.column(2).visible(true); // Column 2 is "Date"
            table.column(3).visible(true); // Column 3 is "URL"
        }
        row.child(row.child.isShown() ? false : '').show();
        $(this).find('td.details-control').text(icon);
    });

    $('#tweetTable tbody').on('click', 'td', function () {
        const columnIndex = table.cell(this).index().column;
        if (columnIndex === 3) {
            const url = table.cell(this).data();
            copyToClipboard(url);
        }
    });
}
