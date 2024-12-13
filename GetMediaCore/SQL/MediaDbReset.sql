use Media

delete from SongFiles
delete from Songs
delete from Albums
delete from  Artists

--
DBCC CHECKIDENT ('Artists', RESEED, 0)
DBCC CHECKIDENT ('Albums', RESEED, 0)
DBCC CHECKIDENT ('Songs', RESEED, 0)
DBCC CHECKIDENT ('Songfiles', RESEED, 0)
--

select *
from Artists arts

select *
from Albums

select *
from Songs

select *
from SongFiles
