let currentAudio;

function playAudio(fileId) {
    var path = `/audio/${fileId}.mp3`;
    if (currentAudio) {
        currentAudio.pause();
    }
    currentAudio = new Audio(path);
    currentAudio.play();
}

function stopAudio() {
    if (currentAudio) {
        currentAudio.pause();
    }
}