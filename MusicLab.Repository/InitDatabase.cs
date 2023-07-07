using MusicLab.Repository.Models;

namespace MusicLab.Repository
{
    public static class InitDatabase
    {
        private static MusicLabContext context = new MusicLabContext();

        public static void DummyData()
        {
            //Add artist
            if (context.Artists.Count() == 0)
            {
                context.Artists.Add(new Artist("MCK", "Nghiêm Vũ Hoàng Long (sinh ngày 2 tháng 3 năm 1999 tại Hà Nội), thường được biết đến với nghệ danh MCK, RPT MCK, Nger, hay Ngơ (khi còn theo đuổi thể loại nhạc indie) là một nam rapper, ca sĩ kiêm sáng tác nhạc người Việt Nam.", "img/artists/mck.jpg", "img/artist/mck_cover.jpg"));
                context.Artists.Add(new Artist("Đen Vâu", "Nguyễn Đức Cường, thường được biết đến với nghệ danh Đen Vâu hay Đen, là một nam rapper và nhạc sĩ người Việt Nam. Đen Vâu từng giành được giải Cống hiến và là một trong số ít nghệ sĩ thành công từ làn sóng underground và âm nhạc indie của Việt Nam.", "img/artists/den.jpg", "img/artist/den_cover.jpg"));
                context.Artists.Add(new Artist("Avicii", "Tim Bergling, được biết đến với nghệ danh Avicii, là nam nhạc sĩ, DJ, nhà sản xuất âm nhạc người Thuỵ Điển.", "img/artist/avicii.jpg", "img/artists/avicii_cover.jpg"));
                context.Artists.Add(new Artist("Olew", "O.lew là một trong những ca sĩ hiện tượng trẻ cực viral cộng đồng mạng thời gian qua. Olew tên thật là Phan Thị Thùy Linh sinh ngày 27/09/1999. Ca khúc debut Rồi ta sẽ ngắm pháo hoa cùng nhau đã gây được tiếng vang lớn và là thành công đầu tiên của cô", "img/artist/olew.jpg", "img/artists/olew_cover.jpg"));
                context.Artists.Add(new Artist("Tlinh", "Tlinh tên thật là Nguyễn Thảo Linh sinh  ngày 7 tháng 10 năm 2000. Tlinh là nữ rapper trẻ đầy tiềm năng và triển vọng của làng rap Việt Nam. Cô có tầm ảnh hưởng rất lớn đến trào lưu âm nhạc hiện đại. Tlinh sở hữu cá tính và phong cách đôc đáo không lẫn vào đâu được.", "img/artist/tlinh.jpg", "img/artists/tlinh_cover.jpg"));
                context.SaveChanges();
            }

            //Add album
            if (context.Albums.Count() == 0)
            {
                context.Albums.Add(new Album("99%", DateTime.Parse("2023-03-14"), 1, "img/albums/99%.png", 1000000));
                context.SaveChanges();
            }

            //Add user
            if (context.Users.Count() == 0)
            {
                context.Users.Add(new User("datbgfa", "123", "Ngo Manh Dat", "datbgfa@gmail.com", "img/avatar.jpg", true));
                context.SaveChanges();
            }

            //Add follow
            if (context.FollowArtists.Count() == 0)
            {
                context.FollowArtists.Add(new FollowArtist("datbgfa", 1, DateTime.Now.AddDays(-90)));
                context.SaveChanges();
            }

            //Add song
            if (context.Songs.Count() == 0)
            {
                context.Songs.Add(new Song("Chỉ một đêm nữa thôi", "ChiMotDemNuaThoi.mp3", 0, DateTime.Parse("2023-02-16"), 1, "chi-mot-dem-nua-thoi.jpg", 1350000));
                context.Songs.Add(new Song("Anh đã ổn hơn", "AnhDaOnHon.mp3", 0, DateTime.Parse("2023-03-15"), 1, "anh-da-on-hon.jpg", 3250000));
                context.Songs.Add(new Song("Rồi ta sẽ ngắm pháo hoa cùng nhau", "RoiTaSeNgamPhaoHoaCungNhau.mp3", 0, DateTime.Parse("2023-01-15"), null, "roi-ta-se-ngam-phao-hoa-cung-nhau.jfif", 150300));
                context.Songs.Add(new Song("Wake Me Up", "WakeMeUp.mp3", 0, DateTime.Parse("2012-01-10"), null, "wake-me-up.jpg", 22000000));
                context.SaveChanges();
            }

            //Add category
            if (context.Categories.Count() == 0)
            {
                context.Categories.Add(new Category("Rap", "Rap là một hình thức nghệ thuật trong văn hóa hip hop xuất phát từ Âu Mỹ và được đặc trưng bằng việc trình diễn thông qua việc nói hoặc hô vang lời bài hát, ca từ một cách có vần điệu, kết hợp với động tác nhảy nhót, tạo hình.", true, "img/categories/rap.jpg"));
                context.Categories.Add(new Category("Pop", "Nhạc pop là một thể loại của nhạc đương đại và rất phổ biến trong làng nhạc đại chúng. Nhạc pop khởi đầu từ thập niên 1950 và có nguồn gốc từ dòng nhạc rock and roll.", true, "img/categories/pop.jpg"));
                context.Categories.Add(new Category("K-Pop", "K-pop, viết tắt của cụm từ tiếng Anh Korean popular music tức nhạc pop tiếng Hàn hay nhạc pop Hàn Quốc, là một thể loại âm nhạc bắt nguồn từ Hàn Quốc như một phần của văn hóa Hàn Quốc.", true, "img/categories/kpop.jpg"));
                context.Categories.Add(new Category("Indie", "Nhạc indie là nhạc được sản xuất độc lập mà không có sự can thiệp của các hãng đĩa thương mại hoặc các chi nhánh của họ, loại nhạc này có thể thực hiện bằng hình thức DIY để thu âm và xuất bản.", true, "img/categories/indie.jpg"));
                context.Categories.Add(new Category("EDM", "Nhạc nhảy điện tử được mô tả như một thể loại nhạc có tiết tấu mạnh kế thừa từ nhạc disco của những năm 1970 và ở một vài khía cạnh nào đó, nó cũng là những thể nghiệm của nhạc Pop.", true, "img/categories/edm.jpg"));
                context.Categories.Add(new Category("Top 10 Billboard", "Top 10 bài hát hot nhất trên bảng xếp hạng.", false, "img/categories/billboard.jpg"));
                context.Categories.Add(new Category("Trending Songs", "Những bài hát mới nhất và nhiều lượt nghe nhất.", false, "img/categories/trending.jpg"));
                context.SaveChanges();
            }

            //Add song category
            if (context.SongCategories.Count() == 0)
            {
                context.SongCategories.Add(new SongCategory(1, 1, DateTime.Now));
                context.SongCategories.Add(new SongCategory(2, 1, DateTime.Now));
                context.SongCategories.Add(new SongCategory(3, 2, DateTime.Now));
                context.SongCategories.Add(new SongCategory(4, 5, DateTime.Now));
                context.SaveChanges();
            }

            //Add favourite
            if (context.Favourites.Count() == 0)
            {
                context.Favourites.Add(new Favourite("datbgfa", 1, DateTime.Now));
                context.Favourites.Add(new Favourite("datbgfa", 4, DateTime.Now));
                context.SaveChanges();
            }

            //Add playlist
            if (context.Playlists.Count() == 0)
            {
                context.Playlists.Add(new Playlist("My first playlist", "datbgfa", DateTime.Now));
                context.Playlists.Add(new Playlist("Chill playlist", "datbgfa", DateTime.Now));
                context.SaveChanges();
            }

            //Add playlist song
            if (context.PlaylistSongs.Count() == 0)
            {
                context.PlaylistSongs.Add(new PlaylistSong(1, 1, DateTime.Now));
                context.PlaylistSongs.Add(new PlaylistSong(1, 2, DateTime.Now));
                context.PlaylistSongs.Add(new PlaylistSong(1, 3, DateTime.Now));
                context.PlaylistSongs.Add(new PlaylistSong(1, 4, DateTime.Now));
                context.PlaylistSongs.Add(new PlaylistSong(2, 2, DateTime.Now));
                context.SaveChanges();
            }

            //Add play history
            if (context.PlayHistories.Count() == 0)
            {
                context.PlayHistories.Add(new PlayHistory("datbgfa", 1, DateTime.Now.AddDays(-20), 0));
                context.PlayHistories.Add(new PlayHistory("datbgfa", 2, DateTime.Now.AddDays(-2), 0));
                context.PlayHistories.Add(new PlayHistory("datbgfa", 3, DateTime.Now.AddDays(-14), 0));
                context.PlayHistories.Add(new PlayHistory("datbgfa", 4, DateTime.Now.AddDays(-30), 0));
                context.SaveChanges();
            }

            //Add song artist
            if (context.SongArtists.Count() == 0)
            {
                context.SongArtists.Add(new SongArtist(1, 1, DateTime.Now));
                context.SongArtists.Add(new SongArtist(1, 2, DateTime.Now));
                context.SongArtists.Add(new SongArtist(4, 3, DateTime.Now));
                context.SongArtists.Add(new SongArtist(3, 4, DateTime.Now));
                context.SongArtists.Add(new SongArtist(5, 1, DateTime.Now));
                context.SaveChanges();
            }

            context.SaveChanges();
        }
    }
}
