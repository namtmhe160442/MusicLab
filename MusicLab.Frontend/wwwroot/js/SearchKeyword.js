$(document).ready(function () {
    $("#search-all").on('input', function () {
        var keyword = $(this).val();
        if (keyword === null || keyword === undefined || keyword === '') {
            
        } else {
            $('.main-container').empty();
            Manager.GetSearchResult(keyword);
        }
        
    });
});

var Manager = {
    GetSearchResult: function (keyword) {
        Manager.GetSongs(keyword);
    },
    GetSongs: function (keyword) {
        var serviceUrl = "https://localhost:7054/api/get-songs-by-keyword?keyword=" + keyword;
        APIManager.GetAPI(serviceUrl, onSuccess, onFailed);
        function onSuccess(jsonData) {
            if (jsonData.length > 0) {
                var firstSong = jsonData[0];
                var htmlContent = '';
                htmlContent += `<div class="row list mb-5">
                                <div class="col-12 col-md-4 col-lg-3">
                                    <h2 class="title mb-3">Top Result</h2>
                                    <div class="card">
                                        <a href="">
                                            <img src="${firstSong.image}" class="card-img-top rounded-circle w-50 mb-2" />
                                        </a>
                                        <div class="card-body p-0 ms-2">
                                            <a href="">
                                                <h3 class="card-title">${firstSong.title}</h3>
                                                <p class="card-text">`;

                firstSong.songArtists.forEach(function (song) {
                    if (song.artistId != firstSong.songArtists[firstSong.songArtists.length - 1].artistId) {
                        song.artist.name += ", ";
                    }
                    htmlContent += song.artist.name;
                });
                htmlContent += `</p>
                                    </a>
                                        </div>
                                        <div class="btn-play">
                                            <button class="d-flex justify-content-center align-items-center">
                                                <i class="fas fa-play"></i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-md-8 col-lg-9">
                                            <h2 class="title mb-3">Songs</h2>`;
                jsonData.forEach(function (song) {
                    htmlContent += `<div class="search-song-hover d-flex p-2">
                                                <a href="" class="search-btnPlay position-relative">
                                                    <img src="${song.image}" class="searched-song d-inline my-auto rounded">
                                                    <div class="search-btnPlay-song position-absolute top-50 start-50 translate-middle">
                                                        <i class="fa-solid fa-play text-white"></i>
                                                    </div>
                                                </a>
                                                <div class="d-inline-block ms-4 text-white-50">
                                                    <b class="fs-5 text-white">${song.title}</b><br />
                                                    <span>`;
                    song.songArtists.forEach(function (itemsong) {
                        if (itemsong.artistId != song.songArtists[song.songArtists.length - 1].artistId) {
                            itemsong.artist.name += ", ";
                        }
                        htmlContent += itemsong.artist.name;
                    });
                    htmlContent += `</span>
                                                </div>

                                                <span class="search-favorite ms-auto me-5 align-content-center text-white-50">
                                                    <i class="search-favorite-song fa-regular fa-heart me-5"></i>
                                                    ${Manager.ConvertDuration(song.duration)}
                                                </span>
                                            </div>`;
                });
                htmlContent += `</div>
                                    </div>`;
                $('.main-container').append(htmlContent);
            }
            Manager.GetArtists(keyword);
        }
        function onFailed(xhr, status, error) {
            window.alert(error);
        }
    },
    GetArtists: function (keyword) {
        var serviceUrl = "https://localhost:7054/api/get-artists-by-keyword?keyword=" + keyword;
        APIManager.GetAPI(serviceUrl, onSuccess, onFailed);
        function onSuccess(jsonData) {
            var htmlContent = '';
            if (jsonData.length > 0) {

                htmlContent += `<div class="row list mb-5">
                                    <h2 class="title mb-3">Artist</h2>`;
                jsonData.forEach(function (artist) {

                    htmlContent += `<div class="col-12 col-md-3 col-lg-2">
                                            <div class="card">
                                                <a href="">
                                                    <img src="${artist.image}" class="card-img-top rounded-circle mb-3" />
                                                </a>
                                                <div class="card-body p-0">
                                                    <a href="">
                                                        <h5 class="card-title">${artist.name}</h5>
                                                        <p class="card-text">Artist</p>
                                                    </a>
                                                </div>

                                                <div class="btn-play">
                                                    <button class="d-flex justify-content-center align-items-center">
                                                        <i class="fas fa-play"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>`;                
                });              
                htmlContent += `</div>`;

                $('.main-container').append(htmlContent);
            }
            Manager.GetAlbum(keyword);
        }
        function onFailed(xhr, status, error) {
            window.alert(error);
        }
    },
    GetAlbum: function (keyword) {
        var serviceUrl = "https://localhost:7054/api/get-albums-by-keyword?keyword=" + keyword;
        APIManager.GetAPI(serviceUrl, onSuccess, onFailed);
        function onSuccess(jsonData) {
            var htmlContent = '';
            if (jsonData.length > 0) {

                htmlContent += `<div class="row list mb-5">
                                    <h2 class="title mb-3">Album</h2>`;
                jsonData.forEach(function (album) {

                    htmlContent += `<div class="col-12 col-md-3 col-lg-2">
                                            <div class="card">
                                                <a href="">
                                                    <img src="${album.image}" class="card-img-top rounded-0 mb-3" />
                                                </a>
                                                <div class="card-body p-0">
                                                    <a href="">
                                                        <h5 class="card-title">${album.title}</h5>
                                                        <p class="card-text">${album.artist.name}</p>
                                                    </a>
                                                </div>

                                                <div class="btn-play">
                                                    <button class="d-flex justify-content-center align-items-center">
                                                        <i class="fas fa-play"></i>
                                                    </button>
                                                </div>
                                            </div>
                                        </div>`;
                });
                htmlContent += `</div>`;

                $('.main-container').append(htmlContent);
            }
        }
        function onFailed(xhr, status, error) {
            window.alert(error);
        }
    },

    ConvertDuration: function (duration) {
        // Convert the duration to a human-readable format (e.g., mm:ss)
        const minutes = Math.floor(duration / 60);
        const seconds = Math.floor(duration % 60);
        const durationFormatted = `${minutes}:${seconds.toString().padStart(2, '0')}`;
        return durationFormatted;
    }
}