select art.artistid, art.artistName, alb.albumId, alb.albumTitle, son.songID, son.songTitle, sonf.SongFileId, sonf.SongFileFullyQualifiedName, aasx.*
from Artists art
	inner join Albums alb on alb.albumArtistId = art.artistId
	inner join Songs son on son.songAlbumId = alb.albumId
	inner join SongFiles sonf on sonf.SongFileSongId = son.songID
	inner join ArtistAlbumSongXref aasx on aasx.ArtistId = art.artistId and aasx.AlbumId = alb.albumId and aasx.SongId = son.songID
where art.ArtistName = 'ac/dc' and alb.albumTitle = 'black ice'