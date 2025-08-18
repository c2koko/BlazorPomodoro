let audio;
let isTabActive = true;

function startBackgroundMusic() {
    console.log('Starting background music');
    if (!audio) {
        audio = new Audio('/music.mp3');
        audio.loop = true;
        audio.volume = isTabActive ? 0.4 : 0.7;
    }
    audio.play();
}

function updatePageTitle(title) {
    document.title = title;
}

function initTabVisibility() {
    document.addEventListener('visibilitychange', function () {
        isTabActive = !document.hidden;

        if (audio) {
            audio.volume = isTabActive ? 0.4 : 0.7;
        }
    });
}
