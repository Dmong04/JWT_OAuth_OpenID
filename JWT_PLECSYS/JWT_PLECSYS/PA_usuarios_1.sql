use usersdb
go

create procedure usp_insert_users (
	@username varchar(30),
	@password varchar(80),
	@role varchar(10),
	@country varchar(50)
) as
begin
	begin try
		if @username is null or LTRIM(RTRIM(@username)) = ''
			throw 50001, 'El campo de nombre de usuario no debe quedar vacío', 1
		if @password is null or LTRIM(RTRIM(@password)) = ''
			throw 50002, 'El campo de contraseña no debe quedar vacío', 1
		if @role is null or LTRIM(RTRIM(@role)) = ''
			throw 50003, 'El campo de rol no debe quedar vacío', 1
		if @country is null or LTRIM(RTRIM(@country)) = ''
			throw 50004, 'El campo de país no debe quedar vacío', 1
		declare @country_id int
		select @country_id =  country_id from Countries where name = @country
		if @country_id is null
			throw 50005, 'El país ingresado no existe', 1
		insert into Users (username, [password], [role], country_id)
			values (@username, @password, @role, @country_id)
	end try
	begin catch
	declare @error varchar(max)
	set @error = error_message()
		raiserror(@error, 16, 1)
		return
	end catch
end
go