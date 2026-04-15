select top 5 art.*
From Artists art
order by art.artistId desc

Select top 5 alb.*
from Albums alb
order by alb.albumId desc

select top 5 gen.*
from Genres gen
order by gen.genreId desc

Select top 5 son.*
from Songs son
order by son.songID desc

--select *
--from SongFiles sof

--Select top 5 xref.*
--from ArtistAlbumSongXref xref
--order by xref.RefId desc

--during a time while the db is empty, the following actions need to occur:
--rename artist.DisplayName to artist.SortName
--if there needs to be a drop and create, so be it. just make sure it gets saved.

