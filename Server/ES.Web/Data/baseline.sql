use proto

-- Инициализировать таблицу Orders
declare @foremanId uniqueidentifier
select top 1 @foremanId = ObjectID from Users where [Role] = 4

insert into Orders(ObjectID, Number, RegDate, IsApproved, IsCompleted, IsChecked, IsCanceled, ForemanID)
values
(newid(), 1, getdate(), 0, 0, 0, 0, @foremanId);

-- Инициализировать таблицу OrderParts
declare @orderID uniqueidentifier
select top 1 @orderID = ObjectID from Orders

insert into OrderParts(ObjectID, OrderNum, EndDate, IsCompleted, OrderID)
values
(newid(), 1, null, 0, @orderID),
(newid(), 2, null, 0, @orderID)

-- Инициализировать таблицу OrderMaterials
declare @firstPartID uniqueidentifier
declare @secondPartID uniqueidentifier

declare @material1 uniqueidentifier
declare @material2 uniqueidentifier

select top 1 @firstPartID = ObjectID from OrderParts where OrderNum = 1;
select top 1 @secondPartID = ObjectID from OrderParts where OrderNum = 2;

select top 1 @material1 = ObjectID from Materials where [Name] like '%1%';
select top 1 @material2 = ObjectID from Materials where [Name] like '%2%';

insert into OrderMaterials(PartID, MaterialID, [Count])
values
(@firstPartID, @material1, 15),
(@firstPartID, @material2, 6),
(@secondPartID, @material1, 30),
(@secondPartID, @material2, 8)