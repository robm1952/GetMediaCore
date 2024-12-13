/*
--gets list of albums by artist with genre name substituted in for genreId
select alb.AlbumTitle, g.genreName, alb.albumYear
from Albums alb inner join genres g on g.genreId = alb.albumGenre
where alb.albumArtistId = 603 and g.genreid = 8
order by alb.albumYear asc
*/

/*
--gets all artists associated with a compilation album
select art.ArtistId, art.ArtistName, alb.AlbumTitle
from Artists art inner join albumSongXref asx on asx.ArtistId = art.artistId
		inner join Albums alb  on alb.AlbumId = asx.AlbumId
where asx.AlbumId = 5
*/

update Albums set albumYear = 1988
where albumid = 899

select art.artistId, art.artistName, alb.albumId, alb.albumTitle, alb.albumYear
from Artists art inner join Albums alb on alb.albumArtistId = art.artistId
where art.artistName like '%Horton Heat%'

select *
from songs son
where son.songAlbumId = 899
order by son.songTrackNumber

select *
from ArtistAlbumSongXref asx
where asx.AlbumId = 899



