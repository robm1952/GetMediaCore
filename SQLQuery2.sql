
delete SongFiles
where SongFiles.SongFileId = 11287

delete Songs
where Songs.SongId = 11288


delete Albums
where Albums.AlbumId = 1944

delete Albums
where Albums.AlbumId = 1601

Select *
from Artists
where ArtistId = 1601

select *
from Albums
where AlbumArtistId = 1601

select *
from Songs
where Songs.SongAlbumId = 1944

select *
from SongFiles
where SongFiles.SongFileSongId =  11288

--where Artists.ArtistName like 'AC/DC'


/*Artists reset*/

delete from artists

DBCC CHECKIDENT ('Artists', RESEED, 0)

select *
from Artists

/*Albums reset*/
delete from albums

DBCC CHECKIDENT ('Albums', RESEED, 0)

select *
from Albums

/*Songs Reset*/
--delete from songs

--DBCC CHECKIDENT ('Songs', RESEED, 0)

select *
from songs

/*SongFiles Reset*/
--delete from songfiles

--=DBCC CHECKIDENT ('Songfiles', RESEED, 0)

select *
from songfiles
