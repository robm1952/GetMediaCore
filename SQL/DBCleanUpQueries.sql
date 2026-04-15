--select art.*
--from Artists art
--where art.artistId in (17) --(5,6,7,10,11,12,65,66,67,70,71,72,74,75,76,110,111,112,142,143,144,164,165,166,177,178,179,243,244,245,246,247,358.359,360,361,362)
----where art.artistName like 'The%'

select alb.albumId, alb.albumArtistId, alb.albumTitle, gen.genreName, alb.albumYear
from albums alb inner join genres gen
	on alb.albumGenre = gen.genreId
	--where gen.genreId = 17
order by gen.genreId 

select *
from Genres gen