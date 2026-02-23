select u.*, t.* from Users as u inner join Tokens as t on u.id = t.[user_id]

/*select u.id, u.username, u.[role], c.[name] as country, t.access_token from Users as u 
	inner join Countries as c on u.id = c.[user_id] inner join Token as 
	t on u.id = t.[user_id]
use usersdb
go
execute usp_insert_users 'dmong', '12345678', 'ADMIN', 'Costa Rica'

select u.id, u.username, u.role, c.name as country from Users as u 
	inner join Countries as c on u.country_id = c.country_id*/

use usersdb
go
select * from Users

select * from OpenIddictTokens order by Subject asc
