# TicketOrder
create table Class
(
    Id     int not null
        primary key,
    Name   nvarchar(30),
    Price  float,
    Amount float,
    Status int
)
go

create table Passenger
(
    Id           int not null
        primary key,
    Name         nvarchar(30),
    Email        nvarchar(50),
    Address      nvarchar(50),
    Phone        nvarchar(12),
    IdentityCard nvarchar(50)
)
go

create table [Order]
(
    Id          int not null
        primary key,
    TotalPrice  float,
    Quantity    int,
    PassengerId int
        constraint FK_Order_Passenger
            references Passenger
)
go

create table Ticket
(
    Id          int not null
        primary key,
    TotalPrice  float,
    Quantity    int,
    TotalTicket float,
    Departure   nvarchar(50),
    Arrival     nvarchar(50),
    StartTime   datetime,
    EndTime     datetime,
    ClassId     int
        constraint FK_Ticket_Class
            references Class,
    OrderId     int
        constraint FK_Ticket_Order
            references [Order]
)
go

