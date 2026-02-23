use usersdb
go

create view users_info_view as 
select u.id, u.username, u.[role], c.[name] as country, t.access_token from Users as u 
	inner join Countries as c on u.id = c.[user_id] inner join Token as 
	t on u.id = t.[user_id]