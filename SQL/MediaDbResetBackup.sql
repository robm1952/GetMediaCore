use MediaCore

--delete from 
Truncate Table SongFiles
Truncate Table Songs
Truncate Table Genres
Truncate Table Albums
Truncate Table Artists
Truncate Table ArtistAlbumSongXref

--
DBCC CHECKIDENT ('Artists', RESEED, 1)
DBCC CHECKIDENT ('Albums', RESEED, 1)
DBCC CHECKIDENT ('Genres', RESEED, 1)
DBCC CHECKIDENT ('Songs', RESEED, 1)
DBCC CHECKIDENT ('Songfiles', RESEED, 1)
DBCC CHECKIDENT ('ArtistAlbumSongXref', RESEED, 1)
--

select *
from Artists arts

select *
from Albums

select *
from Genres

select *
from Songs

select *
from SongFiles

select *
from ArtistAlbumSongXref