create table Product
(
	ProductID int not null identity,
	Name nvarchar(25) not null,
    constraint PK_Product primary key (ProductID)
);

create table Store
(
	StoreID int not null identity,
	Name nvarchar(25) not null,
    constraint PK_Store primary key (StoreID)
);

create table OwnerInventory
(
	ProductID int not null,
	StockLevel int not null,
    constraint PK_OwnerInventory primary key (ProductID),
    constraint FK_OwnerInventory_Product foreign key
    (ProductID) references Product (ProductID)
);

create table StoreInventory
(
    StoreID int not null,
    ProductID int not null,
    StockLevel int not null,
    constraint PK_StoreInventory primary key (StoreID, ProductID),
    constraint FK_StoreInventory_Store foreign key
    (StoreID) references Store (StoreID),
    constraint FK_StoreInventory_Product foreign key
    (ProductID) references Product (ProductID)
);

create table StockRequest
(
    StockRequestID int not null identity,
    StoreID int not null,
    ProductID int not null,
    Quantity int not null,
    constraint PK_StockRequest primary key (StockRequestID),
    constraint FK_StockRequest_Store foreign key
    (StoreID) references Store (StoreID),
    constraint FK_StockRequest_Product foreign key
    (ProductID) references Product (ProductID)
);
