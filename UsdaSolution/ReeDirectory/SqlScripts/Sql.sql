 CREATE PROCEDURE PrPermission
	-- Add the parameters for the stored procedure here
	@controllerName varchar(60),
	@login varchar(60)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	Select Sign(sum([Add])) as [Add],sign(sum([Edit])) as [Edit],sign(sum([Delete])) as [Delete], sign(sum([Print])) as [Print] from [dbo].[ERoleController] rc
	Inner Join [dbo].[EController] c on (c.Id = rc.Controller_Id)
	inner Join [dbo].[ERole] r on (r.Id = rc.Role_Id)
	inner Join [dbo].[ERoleUser] ru on (r.Id = ru.Role_Id)
	inner join [dbo].[EUser] u on ( u.Id = ru.User_Id)
	where  c.Name = @controllerName and u.[LoginName] =@login
	    
END
GO