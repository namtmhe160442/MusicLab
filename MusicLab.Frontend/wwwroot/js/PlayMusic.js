$(document).ready(function () {
    $(".play-music-btn").on('click', function () {
        var itemDiv = $(this).closest(".btn-play");
        var songId = itemDiv.attr("id");
        songs.length = 0;
        ManagerSong.GetSongById(songId);
    });

    $('.main-container').on('click', '.play-music-btn-artist', function (event) {
        var songId = $(this).parent().attr('id');
        songs.length = 0;
        ManagerSong.GetSongById(songId);
    });

    $(".play-music-album-btn").on('click', function () {
        var albumId = $(this).parent().attr("id");
        songs.length = 0;
        ManagerSong.GetSongByAlbumId(albumId);
    });

    $(".play-music-artist-btn").on('click', function () {
        var artistId = $(this).parent().attr("id");
        songs.length = 0;
        ManagerSong.GetSongByArtistId(artistId);
    });
});

var songUrl;
var ManagerSong = {
    GetSongById: async function (id) {
        try {
            var serviceUrl = "https://localhost:7054/api/get-song-by-id?songId=" + id;
            var jsonData = await APIManager.GetAPIReturn(serviceUrl);

            var artists = '';
            await ManagerSong.GetSongUrl(jsonData.id);

            jsonData.songArtists.forEach(function (song) {
                if (song.artistId != jsonData.songArtists[jsonData.songArtists.length - 1].artistId) {
                    song.artist.name += ", ";
                }
                artists += song.artist.name;
            });

            const songList = {
                title: jsonData.title,
                artist: artists,
                audioUrl: songUrl,
                cover: jsonData.image
            };
            songs.push(songList);

            loadSong(0);
            playSong();
        } catch (error) {
            window.alert(error);
        }
    },
    GetSongUrl: async function (id) {
        try {
            var serviceUrl = "https://localhost:7054/api/get-song-url?songId=" + id;
            const responseText = await APIManager.GetAPISongUrl(serviceUrl);
            songUrl = responseText;
        } catch (error) {
            window.alert(error);
        }
    },

    GetSongByAlbumId: async function (id) {
        try {
            var serviceUrl = "https://localhost:7054/api/get-songs-by-album?albumId=" + id;
            var jsonData = await APIManager.GetAPIReturn(serviceUrl);

            for (const eachsong of jsonData) {
                var artists = '';
                await ManagerSong.GetSongUrl(eachsong.id);

                eachsong.songArtists.forEach(function (song) {
                    if (song.artistId != eachsong.songArtists[eachsong.songArtists.length - 1].artistId) {
                        song.artist.name += ", ";
                    }
                    artists += song.artist.name;
                });

                const songList = {
                    title: eachsong.title,
                    artist: artists,
                    audioUrl: songUrl,
                    cover: eachsong.image
                };
                songs.push(songList);
            }

            loadSong(0);
            playSong();
        } catch (error) {
            window.alert(error);
        }
    },
    GetSongByArtistId: async function (id) {
        try {
            var serviceUrl = "https://localhost:7054/api/get-songs-by-artist?artistId=" + id;
            var jsonData = await APIManager.GetAPIReturn(serviceUrl);

            for (const eachsong of jsonData) {
                var artists = '';
                await ManagerSong.GetSongUrl(eachsong.id);

                eachsong.songArtists.forEach(function (song) {
                    if (song.artistId != eachsong.songArtists[eachsong.songArtists.length - 1].artistId) {
                        song.artist.name += ", ";
                    }
                    artists += song.artist.name;
                });

                const songList = {
                    title: eachsong.title,
                    artist: artists,
                    audioUrl: songUrl,
                    cover: eachsong.image
                };
                songs.push(songList);
            }

            loadSong(0);
            playSong();
        } catch (error) {
            window.alert(error);
        }
    }
}