// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const musicContainer = document.getElementById("music-container");
const playBtn = document.getElementById("play");
const prevBtn = document.getElementById("prev");
const nextBtn = document.getElementById("next");

const audio = document.getElementById("audio");
const progress = document.getElementById("progress");
const progressCurrentTime = document.getElementById("progress-currentTime");
const progressDuration = document.getElementById("progress-duration");
const progressContainer = document.getElementById("progress-container");
const title = document.getElementById("title");
const cover = document.getElementById("cover");
const artist = document.getElementById("artist");

const searchPage = document.getElementById("link_search");

const songs = ["test", "test2"];
let songIndex = 1;
var audioPosition = 0;

loadSong(songs[songIndex]);
function loadSong(song) {
    title.innerText = song;
    audio.src = `img/songs/${song}.mp3`;
    cover.src = `img/cds/${song}.jpg`;
}

function playSong() {
    musicContainer.classList.add("play");
    playBtn.querySelector("i.fas").classList.remove("fa-play-circle");
    playBtn.querySelector("i.fas").classList.add("fa-pause-circle");
    audio.play();
}

function pauseSong() {
    musicContainer.classList.remove("play");
    playBtn.querySelector("i.fas").classList.add("fa-play-circle");
    playBtn.querySelector("i.fas").classList.remove("fa-pause-circle");
    audio.pause();
}

function prevSong() {
    songIndex--;

    if (songIndex < 0) {
        songIndex = songs.length - 1;
    }
    loadSong(songs[songIndex]);
    playSong();
}

function nextSong() {
    songIndex++;
    if (songIndex > songs.length - 1) {
        songIndex = 0;
    }
    loadSong(songs[songIndex]);
    playSong();
}

function updateProgress(e) {
    const { duration, currentTime } = e.srcElement;
    const progressPercent = (currentTime / duration) * 100;
    progressCurrentTime.innerText = Math.floor(currentTime / 60) + ":" + String(Math.floor(currentTime % 60)).padStart(2, "0");
    progressDuration.innerText = Math.floor(duration / 60) + ":" + String(Math.floor(duration % 60)).padStart(2, "0");
    progress.style.width = `${progressPercent}%`;
}

function setProgress(e) {
    const width = this.clientWidth;
    const clickX = e.offsetX;
    const duration = audio.duration;

    audio.currentTime = (clickX / width) * duration;
}

playBtn.addEventListener("click", () => {
    const isPlaying = musicContainer.classList.contains("play");
    if (isPlaying) {
        pauseSong();
    } else {
        playSong();
    }
});

function loadPage(url) {
    audio.pause();
    audioPosition = audio.currentTime;
    $.ajax({
        url: url,
        success: function (data) {
            $("#main").html(data);
            if (musicContainer.classList.contains("play")) {
                audio.currentTime = audioPosition;
                audio.play();
            }
        }
    });
}

prevBtn.addEventListener("click", prevSong);
nextBtn.addEventListener("click", nextSong);

audio.addEventListener("timeupdate", updateProgress);

progressContainer.addEventListener("click", setProgress);

audio.addEventListener("ended", nextSong);

$(".nav-link").click(function (e) {
    e.preventDefault();
    var url = $(this).attr("href");
    loadPage(url);
    if (url == "/Search") {
        document.getElementById("search-top-bar").style.setProperty('display', 'flex', 'important');
    } else {
        document.getElementById("search-top-bar").style.setProperty('display', 'none', 'important');
    }
});
//--------------------------------------------
let main = document.getElementById('main');
let header = document.getElementById('header-overlay');
main.addEventListener('scroll', function (event) {
    var scroll = this.scrollTop;
    if (scroll < 60) {
        header.style.opacity = '.' + scroll
    } else {
        header.style.opacity = 1
    }
})