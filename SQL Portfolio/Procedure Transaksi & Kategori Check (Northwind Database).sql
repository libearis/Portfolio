-- Procedure untuk simulasi transaksi pembelian yang masuk kedalam order dan orderDetails
-- diikuti dengan pengurangan unit in stock di table product
create procedure dbo.OrderTransaction
@employeeID int,
@customerID nchar(5),
@shipVia int,
@keranjang tabelkeranjang readonly
as
begin

	declare @productVariable as productscan

	insert into Orders(CustomerID, EmployeeID, ShipVia, OrderDate, RequiredDate, ShippedDate, Freight)
	select @customerID, @employeeID, @shipVia, GETDATE(), GETDATE(), GETDATE(), SUM(Quantity) * 10
	from @keranjang

	insert into @productVariable(productID, quantity)
	select ProductID, Quantity
	from @keranjang

	declare @OrderID as int = (
	select OrderID	
	from Orders
	where OrderID = 
	(select max(OrderID)from Orders)
	)

	insert into [Order Details](OrderID,ProductID, Quantity, UnitPrice)
	select @OrderID, k.ProductID, k.Quantity, p.UnitPrice
	from @keranjang k 
	join Products p on k.ProductID = p.ProductID

	declare @TotalScan as int = (
		select count(1)[Total Barang yang dibeli]
		from @keranjang
	)
	declare @scannedProduct as int = 1
	declare @stockReduction as int = 0
	declare @currentProductScan as int = 0
	declare @UnitInStockProduct as int 
	declare @UnitIsDiscontinue as int

	while(@scannedProduct <= @TotalScan)
		begin
			set @currentProductScan = (
				select productID
				from @productVariable
				where scanID = @scannedProduct
			)

			set @stockReduction = (
				select quantity
				from @productVariable
				where scanID = @scannedProduct
			)

			set @UnitInStockProduct = (
				select UnitsInStock
				from #duplikatProduct
				where ProductID = @currentProductScan
			)

			set @UnitIsDiscontinue = (
				select Discontinued
				from #duplikatProduct
				where ProductID = @currentProductScan
			)

			IF @UnitIsDiscontinue = 1
			BEGIN
				print('Product Belum Ready')
				set @scannedProduct += 1
				continue
			END

			ELSE IF @stockReduction <= @UnitInStockProduct
			Begin
				update #duplikatProduct
					set UnitsInStock -= @stockReduction
					where ProductID = @currentProductScan
					print ('Transaksi berhasil')
			END

			ELSE
			BEGIN
				print(concat('Transaksi productID ', @currentproductscan, ' kehabisan stok'))
			END
			set @scannedProduct += 1
		end

	select * from Orders
	select * from [Order Details]
	select * from #duplikatProduct
end

declare @cart as TabelKeranjang
declare @customer as nchar(5) = 'CACTU'
declare @employee as int = 8
declare @jasaPengiriman as int = 1

insert into @cart
values (6, 14), (2, 16), (1, 12)

execute dbo.OrderTransaction
	@employeeID = @employee,
	@customerID = @customer,
	@shipVia = @jasaPengiriman,
	@keranjang = @cart

delete from [Order Details] where OrderID > 11077
DBCC CHECKIDENT ([Order Details], RESEED, 11077)


delete from Orders where OrderID > 11077
DBCC CHECKIDENT (Orders, RESEED, 11077)

select *
from Products

select *
from Categories

-- Procedure untuk membuat user dapat menambahkan produk dan kategori baru jika kategori tersebut baru
-- jika kategori yang diinput sudah ada, maka tambahkan produk kedalam kategori yang sudah ada

alter procedure dbo.ProductInput
@productName nvarchar(40),
@supplierID int,
@categoryName nvarchar(45),
@quantityPerUnit nvarchar(20),
@unitPrice money,
@unitInStock smallint,
@unitsOnOrder smallint,
@reorderLevel smallint
as
begin

	declare @maxCategoryID int = (
	select max(CategoryID)
	from Categories
	)
	declare @index int = 1
	declare @currentCategoryName nvarchar(45)

	while(@index <= @maxCategoryID)
	begin
		set @currentCategoryName =(
		select CategoryName
		from Categories
		where CategoryID = @index
		)
		if @categoryName = @currentCategoryName break
		else set @index += 1 
	end

	if @index > @maxCategoryID
	begin
		print('masuk baru')
		insert into Categories(CategoryName)
		values (@categoryName)
	end

	declare @currentCategoryID int = @index

	insert into Products(
	ProductName, 
	SupplierID, 
	CategoryID, 
	QuantityPerUnit, 
	UnitPrice, 
	UnitsInStock,
	UnitsOnOrder,
	ReorderLevel
	)
	values (
	@productName,
	@supplierID,
	@currentCategoryID,
	@quantityPerUnit,
	@unitPrice,
	@unitInStock,
	@unitsOnOrder,
	@reorderLevel
	)
end

-- Alternatif Procedure untuk pengecekan kategori
--alter procedure dbo.CheckCategoryName
--@categoryName nvarchar(45)
--as
--begin

--	declare @maxCategoryID int = (
--	select max(CategoryID)
--	from Categories
--	)
--	declare @index int = 1
--	declare @currentCategoryName nvarchar(45)

--	while(@index <= @maxCategoryID)
--	begin
--		set @currentCategoryName =(
--		select CategoryName
--		from Categories
--		where CategoryID = @index
--		)
--		if(@categoryName = @currentCategoryName) break
--		else set @index += 1 
--	end
--	print(@index)
--	if @index > @maxCategoryID
--	begin
--		print('masuk baru')
--		insert into Categories(CategoryName)
--		values (@categoryName)
--	end
--end

--declare @namaKategori nvarchar(45) = 'Sate'
--execute dbo.CheckCategoryName @namaKategori

select * from Categories
select * from Products

delete from Categories where CategoryID > 8
DBCC CHECKIDENT (Categories, RESEED, 8)

declare @newProductName nvarchar(40) = 'Kerang Ijo'
declare @newSupplierID int = 1
declare @newCategoryName nvarchar(45) = 'Seafood'
declare @newQuantity nvarchar(20) = '1 cangkang besar'
declare @newPrice money = 13.00
declare @newStock smallint = 10
declare @newOrder smallint = 2
declare @newReorder smallint = 4

execute dbo.ProductInput
@newProductName,
@newSupplierID,
@newCategoryName,
@newQuantity,
@newPrice,
@newStock,
@newOrder,
@newReorder

select * from Products
select * from Shippers

