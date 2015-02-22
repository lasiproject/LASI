use [aspnet5-AspSixApp-45c5647f-e635-4e2b-976c-067a568e2343];
create table AspNetUserDocuments(
    DocumentId nvarchar(256) primary key,
    Title nvarchar(500)  not null,
    UserId nvarchar(450) foreign key constraint AspNetUsers.Id,
    OrganizationId nvarchar(450) foreign key references [AspNetOrganizations].OrganizationId
)