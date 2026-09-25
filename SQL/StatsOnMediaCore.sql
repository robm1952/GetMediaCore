/*
-- Add Son.songId when this query to get output data
--select Art.artistId, art.artistName, Alb.albumId, Alb.albumTitle, Gen.genreName, Alb.albumYear, Son.songID, Son.songTitle, Son.songTrackNumber
select Art.artistId, art.artistName, Alb.albumId, Alb.albumTitle, Gen.genreName, Alb.albumYear, Son.songTitle, Son.songTrackNumber
from Artists art inner join Albums Alb on Art.artistId = Alb.albumArtistId
				inner join Genres Gen on Alb.albumGenre = Gen.genreId
				inner join Songs Son on Alb.albumId = Son.songAlbumId
order by art.artistId, Alb.albumId,Son.songTrackNumber

select genreName
from Genres
order by genreName

-- As of 9/24/26
686 = select COUNT(artistId) as NumArtists
from Artists

1064 = select COUNT(albumId) as NumAlbums
from Albums

11975 = select COUNT(songId) as NumSongs
from Songs

declare @AvgSongsPerArtist Decimal (10,2)
set @AvgSongsPerArtist = Cast((select COUNT(songId) from Songs) as decimal) / Cast((select COUNT(artistId) from Artists) as decimal)
17.46 = Select Cast((select COUNT(songId) from Songs) as decimal) / Cast((select COUNT(artistId) from Artists) as decimal)
print @AvgSongsPerArtist

declare @AvgAlbumsPerArtist Decimal (10,2)
set @AvgAlbumsPerArtist = Cast((select COUNT(albumId) from Albums) as decimal) / Cast((select COUNT(artistId) from Artists) as decimal)
1.55 = Cast((select COUNT(albumId) from Albums) as decimal) / Cast((select COUNT(artistId) from Artists) as decimal)
print @AvgAlbumsPerArtist

declare @AvgSongsPerAlbum Decimal (10,2)
set @AvgSongsPerAlbum = Cast((select COUNT(songId) from Songs) as decimal) / Cast((select COUNT(albumId) from Albums) as decimal)
11.25 = Cast((select COUNT(songId) from Songs) as decimal) / Cast((select COUNT(albumId) from Albums) as decimal)
print @AvgSongsPerAlbum

*/
declare @AvgSongsPerAlbum Decimal (10,2)
set @AvgSongsPerAlbum = Cast((select COUNT(songId) from Songs) as decimal) / Cast((select COUNT(albumId) from Albums) as decimal)
print @AvgSongsPerAlbum

--declare @AvgAlbumsPerArtist Decimal (10,2)
--set @AvgAlbumsPerArtist = Cast((select COUNT(albumId) from Albums) as decimal) / Cast((select COUNT(artistId) from Artists) as decimal)
--print @AvgAlbumsPerArtist


--select COUNT(songId) as NumSongs
--from Songs /
--select COUNT(artistId) as NumArtists
--from Artists 

