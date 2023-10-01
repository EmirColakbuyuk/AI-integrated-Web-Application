document.addEventListener("DOMContentLoaded", () => {
    const track = document.getElementById("imagetrack");
    const images = track.getElementsByClassName("image");

    

    setTimeout(() => {
        

        for (const image of images) {
            image.style.width = `${window.innerWidth / 6}px`;
        }

        for (const image of images) {
            image.style.width = `${216}px`;
            image.style.transition = "width 5s"; 
        }
    }, 3000); 
});
