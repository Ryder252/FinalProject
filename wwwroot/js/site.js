function playAudio(fileId) {
    var path = `/audio/${fileId}.mp3`;
    var audio = new Audio(path);
    audio.play()
        .then(() => {
            console.log('Audio started');
        })
        .catch((error) => {
            console.error('Error:', error)
        });
};